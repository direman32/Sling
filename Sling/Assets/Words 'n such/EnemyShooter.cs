using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    private float shootTime;
    private float timeSinceShot;
    private Shooter shooterScript;
    private bool canShoot;
    [SerializeField]
    private GameObject bullet;

    private void Awake()
    {
        AwakeElements();
        notFollow();
        shootTime = 2f;
        timeSinceShot = 0f;
        shooterScript = GetComponent<Shooter>();
        canShoot = false;
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<manager>();
    }

    private void Update()
    {
        if (!manager.gameOver)
        {
            timeSpawnnedIn += getTime();
            if (timeSpawnnedIn < 1)
            {
                setBodyVelocity(Vector3.zero);
            }

            if (allowedToMove)
            {
                MovementUpdates();
            }
            
            timeSinceShot += Time.deltaTime;
            if (timeSinceShot >= shootTime)
            {
                timeSinceShot = 0f;
                canShoot = true;
            }

            if (canShoot && seenPlayer)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                canShoot = false;
            }
        }
    }
}
