using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform folloTarget = null;
    [SerializeField] float horizontalDumping = 5f;

    bool isTransitioning = false;

    private void Update() 
    {
        Vector3 newPos = new Vector3(
            folloTarget.position.x,
            folloTarget.position.y,
            transform.position.z);

        if (!isTransitioning)
        {
            transform.position = newPos;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, newPos, horizontalDumping);
            if (transform.position == newPos)
            {
                isTransitioning = false;
            }
        } 
    }

    public void SlowTransitionTo(Transform newTarget, float transitSpeed)
    {
        folloTarget = newTarget;
        horizontalDumping = transitSpeed;
        isTransitioning = true;
    }
}
