using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float raduis = 40f;
    [SerializeField] LayerMask dockMask;
    public bool inRange;
    // Start is called before the first frame update
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raduis);
    }
    void Start()
    {
        dockMask = LayerMask.GetMask("Dock");
    }
    // Update is called once per frame
    void Update()
    {
        inRange = Physics.CheckSphere(transform.position, raduis, dockMask);
    }

    public bool GetInRange()
    {
        return inRange;
    }
}
