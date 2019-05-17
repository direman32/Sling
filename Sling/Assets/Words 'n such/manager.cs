using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    [SerializeField]
    private int numberOfEnemiesInPlay = 3;
    [SerializeField]
    private int totalNumberOfEnemies = 10;
    [SerializeField]
    private GameObject[] spawns;
    public GameObject enemy;
    private float timeToSpawn = 2f;
    private float timeSinceSpawn = 0f;

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length < numberOfEnemiesInPlay)
        {
            timeToSpawn += Time.deltaTime;
            Vector3 position = spawns[Random.Range(0,3)].transform.position;
            Quaternion rotation = spawns[Random.Range(0, 3)].transform.rotation;
            if (timeToSpawn > timeSinceSpawn)
            {
                timeSinceSpawn = 0f;
                Instantiate(enemy, position, rotation);
            }
        }
    }

}
