using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllComponent : MonoBehaviour
{
    [SerializeField] private List<Component> components;
    public Component[] GetComponentsArray { get => components.ToArray(); }

    public bool SerchComponentByType(Type type, out Component component)
    {
        component = null;
        foreach (var item in components)
        {
            if (item.GetType() == type)
            {
                component = item;
                return true;
            }
        }
        return false;
    }
}
