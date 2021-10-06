using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMassege : MonoBehaviour
{
    public List<float> timeLiveTexts;
    public List<Text> texts;
    public GameObject Fon;

    public GameObject activeInterfase;

    private void Start()
    {
        foreach (var item in texts)
        {
            item.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ShowMessageByIndex(0);
    }

    public void ShowMessageByIndex(int index)
    {
        Fon.SetActive(true);
        texts[index].enabled = true;
        activeInterfase.SetActive(false);

        Time.timeScale = 0.001f;
        Time.fixedDeltaTime = 0.02f;

        Invoke(nameof(HideFon), timeLiveTexts[index] * Time.timeScale);

        FindObjectOfType<JoystickManager>().HideJoystick();
    }

    private void HideFon()
    {
        Fon.SetActive(false);
        activeInterfase.SetActive(true);

        foreach (var item in texts)
        {
            item.enabled = false;
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f / Time.timeScale;

        FindObjectOfType<JoystickManager>().ShowJoystick();
    }

    public void FerstMessege()
    {
        ShowMessageByIndex(0);
    }

    public void SecondMessege()
    {
        ShowMessageByIndex(1);
    }

    public void BrichMessege()
    {
        ShowMessageByIndex(2);
    }

    public void FinalyMessege()
    {
        ShowMessageByIndex(3);
    }

    public void EndMessege()
    {
        ShowMessageByIndex(4);
    }
}

