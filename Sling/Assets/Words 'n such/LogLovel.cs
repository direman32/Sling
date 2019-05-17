using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogLovel : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, bool> DebugsEnabled = new Dictionary<string, bool>();
    private bool debugWorking = true;
    private string[] names;

    public enum LogCategory
    {
        ALL,
        PHYSICS,
        ANIMATION,
        OUTERCLASS,
        MOVEMENT,
        WEAPONS,
        TEST,
    };
    private void Awake()
    {
        DebugsEnabled.Add("All", false);
        DebugsEnabled.Add("PHYSICS", false);
        DebugsEnabled.Add("ANIMATION", false);
        DebugsEnabled.Add("OUTERCLASS", false);
        DebugsEnabled.Add("MOVEMENT", false);
        DebugsEnabled.Add("WEAPONS", false);
        DebugsEnabled.Add("TEST", false);
        if (debugWorking)
        {
            names = new string[]{"All","PHYSICS","ANIMATION","OUTERCLASS","MOVEMENT","WEAPONS","TEST"};
        }
    }

    public void Log(string message, LogCategory logCategory)
    {
        //names count
        Debug.Log(DebugsEnabled.Count);
        for (int i = 0; i < DebugsEnabled.Count; i++)
        {
                Debug.Log(logCategory + " : " + message);
        }
    }
}
