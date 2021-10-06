using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LearningWindow : MonoBehaviour
{
    [SerializeField] private FullScreneMessege[] _fullScreneMesseges;

    public UnityEvent EndMessegesEvent;

    private void Start()
    {
        foreach (var item in _fullScreneMesseges)
        {
            item.EndMessageEvent.AddListener(() => { EndMessegesEvent.Invoke(); });
        }
    }
}
