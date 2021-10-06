using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniMassage : MonoBehaviour
{
    [SerializeField] private float _aliveTime = 1;
    [SerializeField] private float _additionalTime = 0;
    [SerializeField] private float _waitingTime = 0;
    [Space]
    [SerializeField] private GameObject _messageObj;
    [SerializeField] private GameObject[] _objToHide;

    public UnityEvent CloseMessageEvent;

    [ContextMenu("Show")]
    public void ShowMessege()
    {
        Debug.LogError("Show mini message");
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(_waitingTime);

        _messageObj.SetActive(true);

        foreach (var item in _objToHide) item.SetActive(false);

        yield return new WaitForSecondsRealtime(_aliveTime);

        CloseMessageEvent.Invoke();

        yield return new WaitForSecondsRealtime(_additionalTime);

        _messageObj.SetActive(false);

        foreach (var item in _objToHide) item.SetActive(true);

    }
}
