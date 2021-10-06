using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public GameObject pausaObject;

    private Person activePerson;

    public UnityEvent StartPausaEvent;
    public UnityEvent ClosePausaEvent;

    public void PressPausa()
    {
        TimeScale.Singltone.ChangeScale(0);
        pausaObject.SetActive(true);

        StartPausaEvent.Invoke();

        //activePerson = FindObjectOfType<PersonManager>().ActivePerson;
        //activePerson.DeActivate();
    }

    public void ExitPause()
    {
        pausaObject.gameObject.SetActive(false);
        TimeScale.Singltone.NormalizeScale();

        ClosePausaEvent.Invoke();

        //activePerson.Activate();
    }

    public void RestartGeame(bool withExcurtion)
    {
        int i = withExcurtion.GetHashCode();   
    }
}
