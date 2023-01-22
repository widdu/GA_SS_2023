using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Serialized private values
    [SerializeField] private Transform startPointLeftTransform, startPointMiddleTransform, startPointRightTransform;

    // Private values
    private Vector3 startLeftTargetPosition, startMiddleTargetPosition, startRightTargetPosition, movement, targetPosition;
    private float movementSpeed;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    private void Start()
    {
        // Get start point positions.
        startLeftTargetPosition = startPointLeftTransform.position;
        startMiddleTargetPosition = startPointMiddleTransform.position;
        startRightTargetPosition = startPointRightTransform.position;

        // Movement speed units per second is equals to distance between points in X-axis.
        movementSpeed = startPointRightTransform.position.x - startPointMiddleTransform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    private void MoveCharacter()
    {
        Vector2 playerPositionXZ = new Vector2(transform.localPosition.x, transform.localPosition.z);
        Vector2 targetPositionXZ = new Vector2(targetPosition.x, targetPosition.z);
        float distance = Vector2.Distance(playerPositionXZ, targetPositionXZ);
        /*Vector2 travel = targetPosition - new Vector2(transform.position.x, transform.position.z);
        Vector2 frameMovement = travel.normalized * Time.fixedDeltaTime * movement;
        float distance = travel.magnitude;*/
        if (Mathf.Abs(movement.x) < distance)
        {
            transform.Translate(movement);
        }
        else
        {
            transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            targetPosition = Vector2.zero;
        }
    }

    public void MovementTarget(Vector2 moveInput)
    {
        if (targetPosition == Vector3.zero)
        {
            Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y) * movementSpeed;
            movement = direction * Time.deltaTime;
            targetPosition = direction + transform.position;
        }
        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, movement, movementSpeed * Time.deltaTime);
    }
}
