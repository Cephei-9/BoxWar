using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public static Game SingleTone { get; private set; }

    public bool IsFight { get; private set; }

    [Header("GameEvents")]
    public UnityEvent StartGameEvent;
    public UnityEvent EndGameEvent;
    [Header("WaveEvents")]
    public UnityEvent StartFightEvent;
    public UnityEvent EndFightEvent;

    private void Awake()
    {
        //IsFight = true;
        //StartFightEvent.Invoke();
        SingleTone = this;
    }

    private void Start()
    {
        StartGameEvent.Invoke();
    }

    public void StartFight()
    {
        IsFight = true;
        StartFightEvent.Invoke();
    }

    public void Pause()
    {
        IsFight = false;
        EndFightEvent.Invoke();
    }
}
