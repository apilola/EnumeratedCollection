using AP.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyExampleBehaviour : MonoBehaviour
{
    public EnumeratedCollection<MyEnum, GameObject> myTable = new EnumeratedCollection<MyEnum, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum MyEnum
{
    Value1,
    Value2,
    Value3
}