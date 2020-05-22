using UnityEngine;

namespace GameDevJam.Interactable
{
    public class PlayerSpotter : MonoBehaviour
    {
        [SerializeField] Interactable myInteractable = null;
        bool isPlayerInRange = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlayerInRange = true;

                myInteractable.currentPhaseObj.LightOn(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlayerInRange = false;

                myInteractable.currentPhaseObj.LightOn(false);
            }
        }

        public bool IsPlayerInRange()
        {
            return isPlayerInRange;
        }
    }
}
