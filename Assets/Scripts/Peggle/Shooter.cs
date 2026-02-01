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
    private int _shotsLeft;
    

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
        if (_shotsLeft > 0)
        {
            var bullet = Instantiate(BulletPrefab, ShotTransform.position, ShotTransform.rotation);
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(ShotTransform.up*_shootForce, ForceMode2D.Impulse);
            _shotsLeft--;
        }
    }
    
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryShoot();
        }
    }
}
