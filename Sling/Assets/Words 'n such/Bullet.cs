using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 20f;

    private float angle = 0f;
    private Rigidbody2D rb;
    private GameObject player;
    private Vector2 mousePos;
    private Vector3 lookPos;
    private float timeSinceShot = 0f;
    private manager manager;
   
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag(ConstantValues.PLAYER_TAG);
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<manager>();
        mousePos = new Vector2();
        lookPos = new Vector3();
        calculateTrajectory();
        rb.rotation = angle;
    }

    private void calculateTrajectory()
    {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        rb.rotation = angle;
        rb.velocity = (rb.transform.right * bulletSpeed);
        timeSinceShot += Time.deltaTime;
        if (timeSinceShot >= 5)
            destroyBullet();
    }


    void destroyBullet()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.name.Equals("Player") && !collision.name.Equals("Senser"))
        {
            destroyBullet();
        }

        if (collision.name.Equals("EnemyCollider"))
        {
            destroyBullet();
            Destroy(collision.transform.parent.gameObject);
            manager.enemyKilled();
        }
    }
}
