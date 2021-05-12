using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool isFocus = false;
    [SerializeField] Transform player;

    [SerializeField] bool hasInteracted = false;

    private void Update()
    {
        if (!hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    Debug.Log("interact");
                    hasInteracted = true;
                }
                else
                {
                    hasInteracted = false;
                }
            }
        }
    }
    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;              
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);


    }
}
