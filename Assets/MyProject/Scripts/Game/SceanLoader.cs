using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public enum Sceans
{
    MainMenu = 0,
    GameWithLearning = 1,
    GameNoLearning = 2
}

public class SceanLoader : MonoBehaviour
{
    [SerializeField] private float _speedDarkening = 1;
    [SerializeField] private float _speedTransperansy = 1;
    [SerializeField] private Color _darkeningColor = Color.black;
    [SerializeField] private Color _transperansyColor = Color.clear;
    [Space]
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Image _blackOutImage;

    public void LoadSceanByIndex(int index)
    {
        StartCoroutine(LerpImage(_speedDarkening, _transperansyColor, _darkeningColor, () => { SceneManager.LoadScene(index); }));
    }

    public void LoadSceanByName(Sceans sceans)
    {
        StartCoroutine(LerpImage(_speedDarkening, _transperansyColor, _darkeningColor, () => { SceneManager.LoadScene(sceans.GetHashCode()); }));
    }

    public void StartLavl()
    {
        StartCoroutine(LerpImage(_speedTransperansy, _darkeningColor, _transperansyColor, () => { _canvas.SetActive(false); }));
    }

    private IEnumerator LerpImage(float speed, Color color1, Color color2, Action OnEndAction)
    {
        Debug.LogError("Start unscale time: " + Time.unscaledTime);
        Debug.LogError("Time scale: " + Time.timeScale);
        Debug.LogError("Start time: " + Time.time);
        //Debug.Break();
        _canvas.SetActive(true);
        for (float i = 0; i < 1; i += Time.deltaTime * Time.timeScale * speed) //Mathf.Min(Time.unscaledDeltaTime * speed, (float)1 / 30)
        {
            _blackOutImage.color = Color.Lerp(color1, color2, i);
            yield return null;
            print("UnscaleTime: " + Time.unscaledDeltaTime + "I: " + i);
            print("DeltaTime: " + Time.deltaTime + "I: " + i);
        }
        _blackOutImage.color = color2;

        OnEndAction.Invoke();

        Debug.LogError("End unscale time: " + Time.unscaledTime);
        Debug.LogError("Time scale: " + Time.timeScale);
        Debug.LogError("End time: " + Time.time);
        //Debug.Break();
    }
}
