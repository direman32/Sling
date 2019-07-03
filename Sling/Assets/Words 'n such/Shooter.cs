using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Weapons
{
    public GameObject bullet;
    private GameObject player;
    Vector3 lookPos;
    private bool isPlayer;

    private void Awake()
    {
        lookPos = new Vector3();
        player = GameObject.FindGameObjectWithTag(ConstantValues.PLAYER_TAG);
        isPlayer = (gameObject.name == "Player");
        //transform.rotation = Quaternion.AngleAxis(calculateAngle(), Vector3.forward);
    }

    void Update()
    {
        rotate();
        if (Input.GetButtonDown("Fire1") && Ammo > 0 && isPlayer)
        {
            playerShooting();
        }
    }

    private float calculateAngle()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        return Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
    }

    private void rotate()
    {
        //Use for hand
        //transform.rotation = Quaternion.AngleAxis(calculateAngle(), Vector3.forward);
    }

    private void playerShooting()
    {
        calculateAngle();
        Instantiate(bullet, transform.position, transform.rotation);
        Ammo--;
    }
}
