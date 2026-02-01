using System;
using Peggle;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [Header("Asset References")]
    public GameObject BulletPrefab;
    [Header("Config")] public Transform ShotTransform;
    public float _shootForce = 40;
    public float _shotDelay = 0.2f;
    private int _shotsLeft;
    private MoveByPiece _moveByPiece;
    private float _realShotCharge;
    private float _animShotCharge;
    public float ChargeTime = 1;
    private ChargeAnimation _chargeAnimation;
    private void Awake()
    {
        _chargeAnimation = GetComponent<ChargeAnimation>();
        _moveByPiece = GetComponent<MoveByPiece>();
    }

    private void OnEnable()
    {
        GameManager.OnNewRound += OnNewRound;
    }

    private void OnDisable()
    {
        GameManager.OnNewRound -= OnNewRound;
    }

    private void OnNewRound(GameManager manager, int obj)
    {
        _shotsLeft = manager.gameSettings.ShotsPerRound;
    }

    public void TryShoot()
    {
        if (_shotsLeft > 0 && _realShotCharge >= 1)
        {
            var bullet = Instantiate(BulletPrefab, ShotTransform.position, ShotTransform.rotation);
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(ShotTransform.up*_shootForce, ForceMode2D.Impulse);
            _shotsLeft--;
        }

        _realShotCharge = 0;
    }
    
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryShoot();
        }

        //because we're reading _moveByPiece, we might be behind by an update tick.
        
        if (_moveByPiece.TouchPhase == TouchPhase.Release)
        {
            if (_realShotCharge >= 1)
            {
                TryShoot();
            }

            _realShotCharge -= 2 * (Time.deltaTime / ChargeTime);

        }else if (_moveByPiece.TouchPhase == TouchPhase.None)
        {
            //if we are not touching it, it should not charge up.
            _realShotCharge -= 2*(Time.deltaTime / ChargeTime);
        }
        else //stay or press...
        {
            //if we are touching it, it should charge up.
            _realShotCharge += Time.deltaTime / ChargeTime;
        }

       
        _realShotCharge = Mathf.Clamp01(_realShotCharge);
        _chargeAnimation.SetChargeAmount(_realShotCharge);
    }

}
