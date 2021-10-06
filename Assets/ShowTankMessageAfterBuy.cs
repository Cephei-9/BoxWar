using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTankMessageAfterBuy : MonoBehaviour
{
    [SerializeField] private BildingPoint _bildPointForBox;
    [SerializeField] private FullScreneMessege _tankMessage;

    private void Start()
    {
        _bildPointForBox.Events.OnBuyPerson.AddListener(SubscribeOnBildPoint);
    }

    public void SubscribeOnBildPoint(Person person)
    {
        
        if (person.GetComponentInChildren<FolowBoxManager>() == false) return;

        _tankMessage.ShowMessege();
        StartCoroutine(ChangeOnThisPeron(person));
    }

    private IEnumerator ChangeOnThisPeron(Person person)
    {
        yield return new WaitForEndOfFrame();

        PersonManager.SingleTone.ChangePersonOnThis(person);
        Destroy(gameObject);
    }
}
