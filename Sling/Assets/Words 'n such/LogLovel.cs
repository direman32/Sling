using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogLovel
{
    [SerializeField]
    private Dictionary<LogCategory, bool> DebugsEnabled = new Dictionary<LogCategory, bool>();
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
        DebugsEnabled.Add(LogCategory.ALL, false);
        DebugsEnabled.Add(LogCategory.PHYSICS, false);
        DebugsEnabled.Add(LogCategory.ANIMATION, false);
        DebugsEnabled.Add(LogCategory.OUTERCLASS, false);
        DebugsEnabled.Add(LogCategory.MOVEMENT, false);
        DebugsEnabled.Add(LogCategory.WEAPONS, false);
        DebugsEnabled.Add(LogCategory.TEST, false);
    }

    public void Log(string message, LogCategory logCategory)
    {
        //if (DebugsEnabled[logCategory] || DebugsEnabled[LogCategory.ALL])
            //Debug.Log(logCategory + " : " + message);
    }
}
