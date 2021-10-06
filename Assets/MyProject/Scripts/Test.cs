using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Tank[] tanks = new Tank[]
        {
            new Tank(),
            null,
            new Tank()
        };
        List<Tank> newArr = SerchTransform.CleinIEnumerableAtNull(tanks);
        print(newArr);
    }
}
