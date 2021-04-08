using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class TestingMovement : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField]private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.velocity.magnitude < speed)
        {
            float value = Input.GetAxis("Vertical");
            if(value != 0)
            {
                rb.AddForce(0, 0, value * Time.fixedDeltaTime * 1000f);
            }
        }
    }
}
