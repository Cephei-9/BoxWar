using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [Header("Player")]
    [SerializeField] private bool MadeFullHpOnEndFight = true;
    [SerializeField] private float _addedHelthKoif = 1;
    [Space]
    public Person selfPerson;
    public Slider healthSlider;

    public override void Start()
    {
        base.Start();
        if(MadeFullHpOnEndFight) Game.SingleTone.EndFightEvent.AddListener(MadeFullHp);
    }

    public override void TakeDamage(float damage, Health otherHealth)
    {
        base.TakeDamage(damage, otherHealth);

        ChangeValueOnSlider();
    }

    public override void Die(Health otherHealth)
    {
        PersonManager personManager = FindObjectOfType<PersonManager>();
        personManager.RemovePerson(selfPerson);

        Game.SingleTone.EndFightEvent.RemoveListener(MadeFullHp);

        if (selfPerson.IsActive) personManager.ToNavigation(false);
        base.Die(otherHealth);
    }

    public void MadeFullHp()
    {
        AddHealth(Mathf.Infinity);
    }

    public void AddHealth(float health)
    {
        print("AddHealh: " + health);
        _health += health * _addedHelthKoif;
        _health = Mathf.Min(_health, startHealth);

        ChangeValueOnSlider();
        TakeDamage(0, this);
    }

    private void ChangeValueOnSlider()
    {
        if (healthSlider == null) return;

        float value = Mathf.InverseLerp(0, startHealth, _health);
        healthSlider.value = value;
    }
}
