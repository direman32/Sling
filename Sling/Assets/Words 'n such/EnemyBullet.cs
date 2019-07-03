using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 20f;

    private float angle = 0f;
    private Rigidbody2D rb;
    private GameObject player;
    private Vector3 lookPos;
    private float timeSinceShot = 0f;
    private manager manager;
    private Player playerScript;
   
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag(ConstantValues.PLAYER_TAG);
        playerScript = player.GetComponent<Player>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<manager>();
        lookPos = new Vector3();
        calculateTrajectory();
        rb.rotation = angle;
    }

    private void calculateTrajectory()
    {
        lookPos = player.transform.position - transform.position;
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
        if (!collision.name.Equals("EnemyCollider") && !collision.name.Equals("Senser"))
        {
            destroyBullet();
        }

        if(collision.name.Equals("Player"))
        {
            playerScript.TakeDamage(20);
        }
    }
}
