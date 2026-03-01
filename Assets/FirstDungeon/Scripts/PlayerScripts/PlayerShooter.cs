using System;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 7;
    public float ProjectileSpeed => projectileSpeed;
    float spawnOffset = 0.6f;

    public bool CanShoot;
    public event Action<bool> OnCanShootChanged;

    PlayerMovement movement;
    public GameObject projectilePrefab;


    public void UnlockFrogStaff()
    {
        if (CanShoot) return; 

        CanShoot = true;
        OnCanShootChanged?.Invoke(CanShoot);
    }

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(GameKeys.Shoot))
            return;
        if (CanShoot == false)
            return;
        Shoot();
    }
    public void Shoot()
    {
        Vector2 dir = movement.Facing;
        Vector2 playerPos = movement.rb.position;
        Vector2 spawnPos = playerPos + dir * spawnOffset;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        FrogProjectile FrogScript = projectile.GetComponent<FrogProjectile>();
        FrogScript.Launch(dir, projectileSpeed);

    }
}
