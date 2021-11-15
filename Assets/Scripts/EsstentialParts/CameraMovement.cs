using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;
     public Vector2 maxPosition;
     public Vector2 minPosition;

    [SerializeField] public VectorValue targetStartPosition = null;

    // Start is called before the first frame update
    void Start()
    {
        if (targetStartPosition != null)
        {
            float startX = Mathf.Clamp(targetStartPosition.initalValue.x, minPosition.x, maxPosition.x);
            float startY = Mathf.Clamp(targetStartPosition.initalValue.y, minPosition.y, maxPosition.y);
            Vector3 targetStartPos = new Vector3(startX, startY, transform.position.z);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            // to avoid the camera from being underneath

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
