using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{
    private void Awake()
    {
        AwakeElements();
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
        }
    }
}
