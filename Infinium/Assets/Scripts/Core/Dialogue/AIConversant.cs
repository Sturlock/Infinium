using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] string AIName;
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] bool near = false;
        float radius = 3f;
        LayerMask playerLayer;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        void Start()
        {
            playerLayer = LayerMask.GetMask("Player");
        }
        void Update()
        {
            TriggerDialogue();
        }

        private void TriggerDialogue()
        {
            near = Physics.CheckSphere(transform.position, radius, playerLayer);
            if (near && Input.GetKeyDown(KeyCode.E))
            {
                GameObject.FindGameObjectWithTag("Player")
                    .GetComponent<PlayerConversant>()
                    .StartDialogue(this,dialogue);
            }
        }

        public string GetName()
        {
            return AIName;
        }
    }
}