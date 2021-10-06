using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnStart : MonoBehaviour
{
    public UnityEvent StartEvent;

    void Start()
    {
        Debug.LogError("StartEvent");
        StartEvent.Invoke();
    }
}
