using UnityEngine;

namespace CodeNameIW.EnvironmentSystem
{
    public class Chest : MonoBehaviour, IInteractable
    {
        public int InteractionID { get; private set; }

        private void Start()
        {
           InteractionID = gameObject.GetInstanceID();
        }

        public void Interact()
        {
            Debug.Log("Interacted with " + InteractionID);
            
        }

        public void Disconnect()
        {
            Debug.Log("Disconnected from " + InteractionID);
        }
    }
}