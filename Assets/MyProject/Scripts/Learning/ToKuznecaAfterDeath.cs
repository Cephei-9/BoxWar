using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToKuznecaAfterDeath : MonoBehaviour
{
    [SerializeField] private float _speedForNullPerson = 0.5f;
    [SerializeField] private BildingPoint _bildingPointForBox;

    private void Start()
    {
        _bildingPointForBox.Events.OnBuyPerson.AddListener(SubscribeOnBoxDie);
        Game.SingleTone.EndFightEvent.AddListener(
            () => { _bildingPointForBox.Events.OnBuyPerson.RemoveListener(SubscribeOnBoxDie); }
            );
    }

    private void SubscribeOnBoxDie(Person person)
    {
        PlayerHealth playerHealth = person.transform.GetComponentInChildren<PlayerHealth>();
        playerHealth.OnDieEvent.AddListener(ChangePersonOnBoxBildPoint);
    }

    public void ChangePersonOnBoxBildPoint(Health health)
    {
        Action action = () => { PersonManager.SingleTone.ChangePersonOnThis(_bildingPointForBox); };
        StartCoroutine(Wait(action, 1));
    }

    public void ChangePersonOnBoxBildPoint()
    {
        Action action = () => { PersonManager.SingleTone.ChangePersonOnThis(_bildingPointForBox); };
        StartCoroutine(Wait(action, 1));
    }

    public void ChangePersonOnNullPerson()
    {
        GameObject gameObject = new GameObject();
        StaticCameraPerson person = gameObject.AddComponent<StaticCameraPerson>();
        person.SetSpeed(_speedForNullPerson);
        Action action = () => { PersonManager.SingleTone.ChangePersonOnThis(person); };
        StartCoroutine(Wait(action, 0));
    }

    private IEnumerator Wait(Action action, float updateCount)
    {
        for (int i = 0; i < updateCount; i++)
        {
            yield return null; 
        }
        action.Invoke();
    }
}


