using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BildingPoint : Person
{
    [System.Serializable]
    public class BildPointEvents : PersonEvents 
    {
        public UnityEvent<Person> OnBuyPerson;
    }

    [Header("BildPoint")]
    [SerializeField] private bool dieAfterBuy;
    [SerializeField] GameObject _interfaceObjectInActiveMode;
    [Space]
    [SerializeField] private Transform _spawnTransform;
    [Space]
    [SerializeField] List<Person> _personsCanBild;
    [SerializeField] List<int> _prices;

    private Person _createPerson = null;
    private int _activePersonIndex;

    public new BildPointEvents Events;

    public override void Activate()
    {
        base.Activate();

        _interfaceObjectInActiveMode.SetActive(true);
    }

    public override void DeActivate()
    {
        base.DeActivate();
        _interfaceObjectInActiveMode.SetActive(false);
        if (_createPerson)
        {
            Destroy(_createPerson.gameObject);
            _createPerson = null;
        }
    }

    public void CreatePerson(int index)
    {
        if (_createPerson != null)
        {
            if (index == _personsCanBild.IndexOf(_createPerson)) return;

            Destroy(_createPerson.gameObject);
        }
        _createPerson = Instantiate(_personsCanBild[index], _spawnTransform.position, _spawnTransform.rotation);
        _activePersonIndex = index;
        _createPerson.enabled = false;
    }

    protected override void SetCameraParametrs()
    {
        _cameraMove.SetPosition(transform.position, transform.rotation);
    }

    public void BuyActivePerson()
    {
        int prise = _prices[_activePersonIndex];
        if (Economica.SingleTone.PlayerMoney >= prise && _createPerson)
        {
            Economica.SingleTone.SubtractMoney(prise);
            _personManager.AddPerson(_createPerson);
            Person buyPerson = _createPerson;
            _createPerson = null;
            Events.OnBuyPerson.Invoke(buyPerson);

            if (Game.SingleTone.IsFight)
            {
                _personManager.ChangePersonOnThis(buyPerson);
            }
            else
            {
                print("ToNavigation");
                _personManager.ToNavigation(true);
                buyPerson.DeActivate();
            }

            if (dieAfterBuy)
            {
                _personManager.RemovePerson(this);
                DeActivate();
                Destroy(this.gameObject);
                _interfaceObjectInNavigationMode = new GameObject();
                return;
            }
        }
    }
}
