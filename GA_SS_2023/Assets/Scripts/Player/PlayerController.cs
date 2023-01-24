using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Serialized private values
    // [SerializeField] private Vector3 movement;    Inspectable value while uncommented

    // Private values
    private PlayerCollider playerCollider;
    private Vector3 playerOriginalPosition, movement, targetPosition, startPointDistanceToTrackStartPoint;
    private float movementSpeed, trackAngle, movementSpeedSlopeSubtraction;
    private bool waitForRelease = false;

    public bool WaitForRelease { get { return waitForRelease; } set { waitForRelease = value; } }

    private enum Path
    {
        Start, // 0
        Track // 1
    }

    private Path path;

    private void Awake()
    {
        playerCollider = GetComponent<PlayerCollider>();
        if (playerCollider == null)
        {
            Debug.LogWarning("Can't get player object's child object Collider's player collider component for player controller component!");
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        playerOriginalPosition = transform.localPosition;

        path = Path.Start;
    }

    // Update is called once per frame
    private void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    public void Setup(Vector3 startPointRightTransformPosition, float startPointMiddleTransformPositionX, float trackStartRightTargetPositionZ, float trackPlatformGroupLocalEulerAngleX)
    {
        // Movement speed units per second is equals to distance between points in X-axis.
        movementSpeed = startPointRightTransformPosition.x - startPointMiddleTransformPositionX;

        // Distance between start path point and respective track path start point.
        startPointDistanceToTrackStartPoint = new Vector3(0f, 0f, trackStartRightTargetPositionZ - startPointRightTransformPosition.z);

        // Save track platform group Z rotation for when player gets on a track platform.
        trackAngle = trackPlatformGroupLocalEulerAngleX;

        // Subtract the effect of the slope on the track from movement speed and save it to the following variable.
        movementSpeedSlopeSubtraction = movementSpeed - ((360 - Mathf.Abs(trackAngle)) * movementSpeed / 360);
    }

    private void MoveCharacter()
    {
        Vector2 playerPositionXZ = new Vector2(transform.localPosition.x, transform.localPosition.z);
        Vector2 targetPositionXZ = new Vector2(targetPosition.x, targetPosition.z);
        float distance = Vector2.Distance(playerPositionXZ, targetPositionXZ);

        if (Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.z)) < distance)
        {
            transform.Translate(movement);
        }
        else
        {
            transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

            if(targetPosition.z > 0)
            {
                path = Path.Track;
                transform.localEulerAngles = new Vector3(trackAngle * (int)path, transform.rotation.y, transform.rotation.z);
            }

            targetPosition = Vector2.zero;
        }
    }

    public void MovementSwitch(Vector2 moveInput)
    {
        switch (path)
        {
            case Path.Start:
                if (targetPosition == Vector3.zero)
                {
                    MovementTargetStart(moveInput);
                }
                break;
            case Path.Track:
                MovementTargetTrack(moveInput);
                break;
        }
    }

    private void MovementTargetStart(Vector2 moveInput)
    {
        if (moveInput.x != 0) // Prioritize horizontal movement on start platform.
        {
            Vector3 direction = new Vector3(moveInput.x, 0f, 0f) * movementSpeed;
            movement = direction * Time.deltaTime;
            targetPosition = direction + transform.position;
        }
        else
        {
            // TODO: Jumping from start platform point to track platform point.
            Vector3 direction = new Vector3(0f, 0f, moveInput.y) * movementSpeed;
            movement = direction * Time.deltaTime;
            targetPosition = startPointDistanceToTrackStartPoint + transform.position;
        }
    }

    private void MovementTargetTrack(Vector2 moveInput)
    {
        /*if (moveInput.x != 0) // Prioritize horizontal movement on start platform.
        {
            
        }
        else
        {*/
            Vector3 direction = new Vector3(0f, 0f, moveInput.y) * movementSpeed;
            movement = direction * Time.deltaTime;
            targetPosition = movement + transform.position;
        //}
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "End")
        {
            transform.localPosition = playerOriginalPosition;
            targetPosition = Vector3.zero;
            path = Path.Start;
            waitForRelease = true;
        }
    }
}
