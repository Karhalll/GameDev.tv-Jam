using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using GameDevJam.Core.Variables;

namespace GameDevJam.Pickups
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] UnityEvent pickUp = null;

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.tag == "Player")
            {
                pickUp.Invoke();
                Destroy(gameObject);
            }
        }



    }
}
