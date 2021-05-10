using UnityEngine;

namespace Infinium.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] private string AIName;
        [SerializeField] private Dialogue dialogue = null;
        [SerializeField] private bool near = false;
        [SerializeField] private float radius = 3f;
        private LayerMask playerLayer;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        private void Start()
        {
            playerLayer = LayerMask.GetMask("Player");
        }

        private void Update()
        {
            near = Physics.CheckSphere(transform.position, radius, playerLayer);
            if (near && Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
            }
        }

        public void TriggerDialogue()
        {
            GameObject.FindGameObjectWithTag("Player")
                .GetComponent<PlayerConversant>()
                .StartDialogue(this, dialogue);
        }

        public string GetName()
        {
            return AIName;
        }
    }
}