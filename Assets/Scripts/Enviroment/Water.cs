using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevJam.Enviroment
{
  public class Water : MonoBehaviour
  {
    [SerializeField] Transform respawnPoint = null;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.transform.position = respawnPoint.position;
        }
    }
  }
}

