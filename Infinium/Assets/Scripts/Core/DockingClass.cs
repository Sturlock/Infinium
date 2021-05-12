using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingClass : MonoBehaviour
{
    SphereCollider sc;
    // Start is called before the first frame update
    void Awake()
    {
        sc = gameObject.AddComponent<SphereCollider>();
    }

    void Start()
    {
        sc.isTrigger = true;
        sc.radius = 0.0001f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
