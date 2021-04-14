using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    WeaponScriptable[] contence;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in contence)
        {
            Debug.Log($"Has Item: {item.GetName()}");
        }
    }

    internal void AddToFirstEmptySlot(object item, int number)
    {
        throw new NotImplementedException();
    }
}
