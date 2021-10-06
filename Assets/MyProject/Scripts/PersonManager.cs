
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersonManager : MonoBehaviour
{
    public List<Person> Persons;
    public void AddPerson(Person person) => Persons.Add(person);
    public void RemovePerson(Person person) => Persons.Remove(person);

    [SerializeField] private Person _activePersone;
    [SerializeField] private Navigation _navigation;

    [HideInInspector] public UnityEvent<Person> OnPersonChanged;

    public static PersonManager SingleTone { get; private set; }
    public Person ActivePerson { get => _activePersone; }

    private void Start()
    {
        SingleTone = this;

        foreach (var item in Persons)
        {
            item.DeActivate();
        }

        if (_activePersone is Navigation) ToNavigation(false);
        _activePersone.Activate();
        OnPersonChanged.Invoke(_activePersone);
    }

    public void ChangePersonByIndex(int personIndex)
    {
        if (_activePersone == Persons[personIndex]) return;

        _activePersone.DeActivate();
        Persons[personIndex].Activate();

        OnPersonChanged.Invoke(_activePersone);
    }

    public void ChangePersonOnThis(Person nextPerson)
    {
        if (_activePersone == nextPerson) return;

        _activePersone.DeActivate();
        nextPerson.Activate();
        _activePersone = nextPerson;

        OnPersonChanged.Invoke(_activePersone);
    }

    public void ToNavigation(bool inLastPoint)
    {
        _activePersone.DeActivate();
        if (inLastPoint) _navigation.ReturnInLastPosition();

        SetActiveAllNavigationsInterface(true);
        _navigation.Activate(_activePersone);

        _activePersone = _navigation;
        OnPersonChanged.Invoke(_activePersone);
    }

    public void SetActiveAllNavigationsInterface(bool active)
    {
        foreach (var item in Persons)
        {
            if ((item is Navigation) == false)
            {
                item.NavigationInterfaceSetActive(active);
            }
        }
    }
}
