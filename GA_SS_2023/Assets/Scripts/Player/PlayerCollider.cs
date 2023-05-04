using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // Private variables
    private Transform playerTransform;
    private PlayerController playerController;
    private BoxCollider boxCollider;
    private int hazardLayer = 7, trackLayer = 10;

    public int collisionCount = 0; // Make this private

    // Properties
    public bool IsActive { get { return collisionCount > 0; } }

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        if (playerTransform == null)
        {
            Debug.LogWarning("Can't get player transform component for player object's player collider component!");
        }

        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogWarning("Can't get player controller component for player object's player collider component!");
        }

        boxCollider = GetComponent<BoxCollider>();
        if(boxCollider == null)
        {
            Debug.LogWarning("Can't get box collider component for player object's player collider component!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;

        if(collision.gameObject.layer == hazardLayer)
        {
            FindObjectOfType<Audiomanager>().Play("Barrel");
            playerController.ResetLevel();
        }

        playerController.SwitchingTrack = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == trackLayer)
        {
            TrackController trackController = collision.gameObject.GetComponent<TrackController>();
            playerTransform.Translate(-trackController.TrackSpeedV3);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount--;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "End")
        {
            GoalScore goalScore = collider.gameObject.GetComponent<GoalScore>();
            playerController.ReachGoal(goalScore);
            goalScore.TrackController.UpdateTrackSpeed();
        }

        if (collider.gameObject.name == "Abyss")
        {
            FindObjectOfType<Audiomanager>().Play("Fall");
            playerController.ResetLevel();
            playerController.SwitchingTrack = false;
        }
    }
}
