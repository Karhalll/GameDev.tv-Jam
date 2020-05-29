using UnityEngine;

namespace GameDevJam.Core
{
    [ExecuteInEditMode]
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform targetToFollow = null;

        void Update()
        {
            transform.position = new Vector3(targetToFollow.position.x, targetToFollow.position.y, transform.position.z);
        }
    }

}