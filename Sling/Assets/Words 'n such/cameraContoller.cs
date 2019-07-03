using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraContoller : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public float cameraDistance = 10f;
    public manager manager;

    private void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<manager>();
        player = GameObject.FindGameObjectWithTag(ConstantValues.PLAYER_TAG).GetComponent<Player>();
}

    void FixedUpdate()
    {
        if (!manager.gameOver)
        {
            Vector3 playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
    }
}
