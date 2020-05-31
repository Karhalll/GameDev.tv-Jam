using UnityEngine;
using UnityEngine.Playables;

using GameDevJam.Controls;

namespace GameDevJam.Core
{
  public class FinallAnimation : MonoBehaviour
  {
	[SerializeField] Transform finalCameraPos = null;
	[SerializeField] float transitSpeed = 0.02f;

    private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
			playerController.DisableControl();

			Camera.main.gameObject.GetComponent<FollowCamera>().SlowTransitionTo(finalCameraPos, transitSpeed);
      }
    }
  }

}