using UnityEngine;
using UnityEngine.Playables;

using GameDevJam.Controls;

namespace GameDevJam.Core
{
  public class FinallAnimation : MonoBehaviour
  {
	[SerializeField] PlayableDirector directour = null;

    private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
			playerController.DisableControl();

			// directour.Play();
		}
    }
  }

}