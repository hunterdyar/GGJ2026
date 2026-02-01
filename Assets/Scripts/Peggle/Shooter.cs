using Peggle;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [Header("Asset References")]
    public RoundData RoundData;
    public GameObject BulletPrefab;
    [Header("Prefab Config")] public Transform ShotTransform; 
    public void TryShoot()
    {
        if (RoundData.ShotsLeft > 0)
        {
            var bullet = Instantiate(BulletPrefab, ShotTransform.position, ShotTransform.rotation);
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(ShotTransform.up*RoundData.ShootForce, ForceMode2D.Impulse);
            RoundData.ShotsLeft--;
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
