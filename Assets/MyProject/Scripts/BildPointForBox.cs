using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BildPointForBox : Person
{
    [SerializeField] List<BildObj> _personsCanBild;
    [SerializeField] List<int> _prices;
    [Space]
    [SerializeField] GameObject _interfaceObjectInActiveMode;

    private BildObj _activeBildObj = null;
    private GameObject instantiateGO;
    private int _activePersonIndex;

    public override void Activate()
    {
        base.Activate();

        _interfaceObjectInActiveMode.SetActive(true);
    }

    public override void DeActivate()
    {
        base.DeActivate();
        _interfaceObjectInActiveMode.SetActive(false);
    }

    public void CreatePerson(int index)
    {
        if (instantiateGO != null)
        {
            if (index == _activePersonIndex) return;

            Destroy(instantiateGO);
        }
        instantiateGO = Instantiate(_personsCanBild[index].gameObject, transform.position, Quaternion.identity);
        instantiateGO.GetComponentInChildren<Person>().DeActivate();
        _activePersonIndex = index;
        //_activeBildObj.gameObject.SetActive(true);
    }

    protected override void SetCameraParametrs()
    {
        _cameraMove.SetPosition(transform.position, transform.rotation);
    }

    public void BuyActivePerson()
    {
        int prise = _prices[_activePersonIndex];
        if (Economica.SingleTone.PlayerMoney > prise)
        {
            Economica.SingleTone.SubtractMoney(prise);
            _personManager.AddPerson(instantiateGO.GetComponentInChildren<Person>());

            DeActivate();
            _personManager.RemovePerson(this);

            if (Game.SingleTone.IsFight)
            {
                _personManager.ChangePersonOnThis(instantiateGO.GetComponentInChildren<Person>());
            }
            else
            {
                _personManager.ToNavigation(true);
            }
            _interfaceObjectInNavigationMode = new GameObject();
        }        
    }  
    
    [System.Serializable]
    private class BildObj
    {
        public GameObject gameObject;
        public Person person;
    }
}
