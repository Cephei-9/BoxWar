using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class SerchTransform
{
    public static Transform SerchNearest(Transform baseTransform, Transform[] arrayTransform, out int indexReturnTransform)
    {
        indexReturnTransform = -1;
        float distanse = Mathf.Infinity;
        int i = 0;
        foreach (var item in arrayTransform)
        {
            float distanceToItem = (item.position - baseTransform.position).magnitude;
            if (distanceToItem < distanse)
            {
                distanse = distanceToItem;
                indexReturnTransform = i;
            }
            i++;
        }
        return arrayTransform[indexReturnTransform];
    }

    public static Transform SerchFarthest(Transform baseTransform, Transform[] arrayTransform, out int indexReturnTransform)
    {
        indexReturnTransform = -1;
        float distanse = -1;
        int i = 0;
        foreach (var item in arrayTransform)
        {
            float distanceToItem = (item.position - baseTransform.position).magnitude;
            if (distanceToItem > distanse)
            {
                distanse = distanceToItem;
                indexReturnTransform = i;
            }
            i++;
        }
        return arrayTransform[indexReturnTransform];
    }

    public static List<T> CleinIEnumerableAtNull<T>(IEnumerable<T> iEnumerable)
    {
        List<T> list = new List<T>();
        foreach (var item in iEnumerable)
        {
            if (item != null) list.Add(item);
        }
        return list;
    }
}
