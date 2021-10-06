using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMessageSuport : MonoBehaviour
{
    [SerializeField] private FullScreneMessege _massage;
    [SerializeField] private GameObject _backButton;

    public void TernOnMessage()
    {
        _massage.ShowMessege();
        PersonManager.SingleTone.SetActiveAllNavigationsInterface(false);
        _backButton.SetActive(false);
    }

    public void ShowNavigationInterfase()
    {
        PersonManager.SingleTone.SetActiveAllNavigationsInterface(true);
        _backButton.SetActive(true);
        Destroy(gameObject);
    }
}
