using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public static TimeScale Singltone { get; private set; }

    private Coroutine _normalizeCoroutine;

    private void Awake()
    {
        Singltone = this;
    }

    public void ChangeScale(float newScale)
    {
        Time.timeScale = newScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if(_normalizeCoroutine != null) StopCoroutine(_normalizeCoroutine);
    }

    public void NormalizeScale()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (_normalizeCoroutine != null) StopCoroutine(_normalizeCoroutine);
    }

    public void ChangeScaleOnTime(float newScale, float time)
    {
        Time.timeScale = newScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        _normalizeCoroutine = StartCoroutine(NormalizeScaleOnTime(time));
    }

    private IEnumerator NormalizeScaleOnTime(float time)
    {
        float startTimeScale = Time.timeScale;
        for (float i = 0; i < 1; i += Time.unscaledDeltaTime / time)
        {
            Time.timeScale = Mathf.Lerp(startTimeScale, 1, i);
            yield return null;
        }
        Time.timeScale = 1;
    }
}
