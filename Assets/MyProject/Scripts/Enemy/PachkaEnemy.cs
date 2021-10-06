using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PachkaEnemy 
{
    public GameObject prefab;
    public int count;
    [Space]
    public float timeToNextPachka;
    public int spawnNumber;

    public UnityEvent OnPachkaCreateEvent;
}


