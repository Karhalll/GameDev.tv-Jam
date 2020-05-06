using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevJam.Interactable
{
    public class PlayerSpotter : MonoBehaviour
    {
        bool isPlayerInRange = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlayerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlayerInRange = false;
            }
        }

        public bool IsPlayerInRange()
        {
            return isPlayerInRange;
        }
    }
}
