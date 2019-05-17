using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraContoller : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public float cameraDistance = 10f;

    private void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        
    }
}
