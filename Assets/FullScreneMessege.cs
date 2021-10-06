using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FullScreneMessege : MonoBehaviour
{
    [SerializeField] private float _waitTime = 0;
    [SerializeField] private bool _destroyAfterWork = true;
    [Space]
    [SerializeField] private GameObject _messageObj;
    [SerializeField] private Text[] _texts;
    [SerializeField] private GameObject[] _objToHide;
    [Space]
    public UnityEvent EndMessageEvent;

    private int _activeTextIndex = 0;

    private void Start()
    {
        foreach (var item in _texts)
        {
            item.enabled = false;
        }
    }

    [ContextMenu("Show")]
    public void ShowMessege()
    {
        StartCoroutine(Wait());
    }

    public void MoveNext()
    {
        _texts[_activeTextIndex].enabled = false;
        _activeTextIndex++;
        if (_activeTextIndex == _texts.Length)
        {
            if(_destroyAfterWork) Destroy(gameObject);

            _activeTextIndex = 0;
            _messageObj.SetActive(false);
            TimeScale.Singltone.NormalizeScale();
            FindObjectOfType<JoystickManager>().ShowJoystick();
            foreach (var item in _objToHide)
            {
                item.SetActive(true);
            }

            EndMessageEvent.Invoke();
            return;
        }

        _texts[_activeTextIndex].enabled = true;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_waitTime);

        TimeScale.Singltone.ChangeScale(0);
        foreach (var item in _objToHide)
        {
            item.SetActive(false);
        }
        _messageObj.SetActive(true);
        _texts[0].enabled = true;

        FindObjectOfType<JoystickManager>().HideJoystick();
    }
}
