using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Economica : MonoBehaviour
{
    [SerializeField] private int _startMoney = 400;
    [SerializeField] private float koifHilPointMoney = 2;
    [Space]
    [SerializeField] private Text _moneyText;

    public int PlayerMoney { get; private set; }
    public static Economica SingleTone { get; private set; }

    public UnityEvent<int> MoneyChangedEvent;

    private void Start()
    {
        SingleTone = this;
        PlayerMoney = _startMoney;

        _moneyText.text = PlayerMoney.ToString();
    }

    public void AddMoney(int addedMoney)
    {
        PlayerMoney += addedMoney;
        ChangeInfo();
    }

    public void SubtractMoney(int subtractMoney)
    {
        PlayerMoney -= subtractMoney;
        ChangeInfo();
    }

    public void TakeAllMoney()
    {
        PlayerMoney = 0;
        ChangeInfo();
    }

    public void AddMoneyAfterEnemyDie(Health enemyHealth)
    {
        int addedMoney = Mathf.RoundToInt((enemyHealth.startHealth * koifHilPointMoney));
        AddMoney(addedMoney);
    }

    public void SubscribeOnCreatedEnemy(GameObject enemy)
    {
        enemy.GetComponent<AllComponent>().SerchComponentByType(new EnemyHealth().GetType(), out Component component);
        if (component == null)
        {
            Debug.LogError("No component EnemyHealth on created enemy");
            return;
        }

        EnemyHealth enemyHealth = component as EnemyHealth;
        int addedMoney = Mathf.RoundToInt((enemyHealth.startHealth * koifHilPointMoney));
        enemyHealth.OnDieEvent.AddListener(AddMoneyAfterEnemyDie);
    }

    private void ChangeInfo()
    {
        _moneyText.text = PlayerMoney.ToString();
        MoneyChangedEvent.Invoke(PlayerMoney);
    }
}
