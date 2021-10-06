using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandlerColorChange : MonoBehaviour, DamageTakingHandler
{
    [SerializeField] private float _colorIntensity;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _finalColor;
    [Space]
    [SerializeField] private Health _selfHealth;
    [SerializeField] private Renderer selfRenderer;

    private void Start()
    {
        _startColor = selfRenderer.material.color;   
    }

    public void Handle(float damage, Health otherHealth)
    {
        float t = _selfHealth.CurentHealth / _selfHealth.startHealth;
        selfRenderer.material.color = Color.Lerp(_finalColor, _startColor, t) * _colorIntensity;
    }
}

