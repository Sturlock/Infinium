using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Dialogue
{
    public class AiForcedConversation : MonoBehaviour
    {
        public AIConversant ThatGuy;
        private float radius = 3f;
        [SerializeField] private LayerMask playerLayer;
        private bool isActive;

        // Start is called before the first frame update
        void Start()
        {
            ThatGuy = GetComponent<AIConversant>();
            isActive = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (isActive)
            {
                bool isNear = Physics.CheckSphere(transform.position, radius, playerLayer);
                if (isNear)
                {
                    ThatGuy.TriggerDialogue();
                    isActive = false;
                }
            }
        }
    }

}