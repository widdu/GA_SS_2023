using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Serialized private variables.
    [SerializeField] private Transform targetObjectTransform;

    private Vector3 originalCameraPosition;
    private float originalDistanceY;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update.
    private void Start()
    {
        originalCameraPosition = transform.position;
        originalDistanceY = transform.position.y - targetObjectTransform.position.y;
    }

    // Update is called once per frame.
    private void Update()
    {
        float cameraPositionY = (targetObjectTransform.position.y + originalDistanceY <= originalCameraPosition.y) ? originalCameraPosition.y : targetObjectTransform.position.y + originalDistanceY;
        float cameraPositionZ = originalCameraPosition.z + targetObjectTransform.position.z;
        transform.position = new Vector3(transform.position.x, cameraPositionY, cameraPositionZ);
    }
}
