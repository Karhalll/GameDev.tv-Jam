using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform folloTarget = null;
    [SerializeField] float horizontalDumping = 5f;

    private void Update() 
    {
        Vector3 newPos = new Vector3(
            folloTarget.position.x,
            folloTarget.position.y,
            transform.position.z);

        //transform.position = Vector3.Lerp(transform.position, newPos, horizontalDumping); 
        transform.position = newPos;
    }
}
