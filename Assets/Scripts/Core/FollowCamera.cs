using UnityEngine;

namespace GameDevJam.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform targetToFollow = null;

        void LateUpdate()
        {
            transform.position = new Vector3(targetToFollow.position.x, targetToFollow.position.y, transform.position.z);
        }
    }

}