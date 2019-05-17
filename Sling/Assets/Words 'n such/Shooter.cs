using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bullet;
    private GameObject player;
    Vector3 lookPos;
    Vector2 offset;

    private void Awake()
    {
        lookPos = new Vector3();
        offset = new Vector2();
        player = GameObject.FindGameObjectWithTag("Player");
        //transform.rotation = Quaternion.AngleAxis(calculateAngle(), Vector3.forward);
    }

    void Update()
    {
        rotate();
        if (Input.GetButtonDown("Fire1"))
        {
            shooting();
        }
    }

    private float calculateAngle()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        return Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
    }

    private void rotate()
    {
        //Use for hand
        //transform.rotation = Quaternion.AngleAxis(calculateAngle(), Vector3.forward);
    }

    private void shooting()
    {
        calculateAngle();
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
