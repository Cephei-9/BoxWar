using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageTakingHandler 
{
    void Handle(float damage, Health otherHealth);
}

public class DamageHandlerEmisionChange : MonoBehaviour, DamageTakingHandler
{
    [SerializeField] private float _colorIntensity;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _finalColor;
    [Range(0,1)]
    [SerializeField] private float _colorLerp;
    [Range(0,1)]
    [SerializeField] private float _colorLerpSqrt;
    [Space]
    [SerializeField] private Health _selfHealth;
    [SerializeField] private Renderer selfRenderer;

    private void Start()
    {
        _startColor = selfRenderer.material.GetColor("_EmissionColor");
    }

    private void Update()
    {
        //selfRenderer.material.SetColor("_EmissionColor", Color.Lerp(_finalColor, _startColor, _colorLerp) * _colorIntensity);
        //_colorLerpSqrt = Mathf.Sqrt(_colorLerp);
    }

    public void Handle(float damage, Health otherHealth)
    {
        float t = _selfHealth.CurentHealth / _selfHealth.startHealth;
        selfRenderer.material.SetColor("_EmissionColor", Color.Lerp(_finalColor, _startColor, t) * _colorIntensity);
    }
}
