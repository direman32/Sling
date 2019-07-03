using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    #region Declarations
    [SerializeField]
    private int numberOfEnemiesInPlay = 3;
    [SerializeField]
    private int totalNumberOfEnemies = 13;
    [SerializeField]
    public GameObject player;
    private GameObject[] spawns;
    [SerializeField]
    public GameObject[] enemies;
    private float timeToSpawn = 2f;
    private float timeSinceSpawn = 0f;
    private int howManySpawned = 0;
    private static int enemiesKilled = 0;
    public TextMeshProUGUI health;
    public TextMeshProUGUI gun;
    public TextMeshProUGUI enemiesLeft;
    public bool gameOver = false;
    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(ConstantValues.PLAYER_TAG);
        spawns = GameObject.FindGameObjectsWithTag("Respawn");
    }

    private void Update()
    {
        if (!gameOver)
        {
            respawnEnemies();
            updateText();
        }
    }

    private void respawnEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length < numberOfEnemiesInPlay)
        {
            timeToSpawn += Time.deltaTime;
            Vector3 position = spawns[Random.Range(0, spawns.Length)].transform.position;
            Quaternion rotation = spawns[Random.Range(0, spawns.Length)].transform.rotation;
            if (timeToSpawn > timeSinceSpawn && howManySpawned < totalNumberOfEnemies)
            {
                timeSinceSpawn = 0f;
                Instantiate(getEnemy(), position, rotation);
                howManySpawned++;
            }
        }

        if(enemiesKilled == totalNumberOfEnemies)
        {
            enemiesLeft.text = "You Win!";
            gameOver = true;
        }
    }

    private GameObject getEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }

    private void updateText()
    {
        health.text = "Health: " + player.GetComponent<Player>().getHealth();
        gun.text = "Gun: " + player.GetComponent<Shooter>().Name + "\nAmmo: " + player.GetComponent<Shooter>().Ammo;
        if(totalNumberOfEnemies - enemiesKilled == 1)
            enemiesLeft.text = (totalNumberOfEnemies - enemiesKilled) + " enemy left";
        else
            enemiesLeft.text = "Enemies Left: " + (totalNumberOfEnemies - enemiesKilled);
    }

    public void enemyKilled()
    {
        enemiesKilled++;
    }

    public void gameEnded()
    {
        gameOver = true;
        health.text = "You died";
        //SceneManager.LoadScene("Playground"); 
    }

}
