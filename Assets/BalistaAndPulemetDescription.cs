using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalistaAndPulemetDescription : MonoBehaviour
{
    [SerializeField] private FullScreneMessege _balistaDiscription;
    [SerializeField] private FullScreneMessege _pulemetDiscription;
    [Space]
    [SerializeField] private BildingPoint[] _bildingPoints;

    private bool _showBalista;
    private bool _showPulemet;

    private Action HideJoystic = () => { };

    private void Start()
    {
        foreach (var item in _bildingPoints)
        {
            item.Events.OnBuyPerson.AddListener(PrintDescription);
        }
    }

    public void PrintDescription(Person person)
    { 
        if (person is Balista && _showBalista == false)
        {
            StartCoroutine(ChangeOnThisPeron(person));
            _showBalista = true;
            _balistaDiscription.ShowMessege();
        }
        else if (person is Pulimet && _showPulemet == false)
        {
            StartCoroutine(ChangeOnThisPeron(person));
            _showPulemet = true;
            _pulemetDiscription.ShowMessege();
        }
    }

    private IEnumerator ChangeOnThisPeron(Person person)
    {
        HideJoystic = () => {
            PersonManager.SingleTone.ChangePersonOnThis(person);
            FindObjectOfType<JoystickManager>().HideJoystick();
        };
        yield return new WaitForEndOfFrame();

        if (_showPulemet && _showBalista) Destroy(gameObject);
    }

    private void LateUpdate()
    {
        HideJoystic.Invoke();
        HideJoystic = () => { };
    }
}
