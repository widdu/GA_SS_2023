using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // Private variables
    private PlayerController playerController;
    private BoxCollider boxCollider;
    private int hazardLayer = 7;

    public int collisionCount = 0; // Make this private

    // Properties
    public bool IsActive { get { return collisionCount > 0; } }

    private void Awake()
    {
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
            playerController.ResetLevel();
        }

        playerController.SwitchingTrack = false;
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
        }

        if (collider.gameObject.name == "Abyss")
        {
            playerController.ResetLevel();
        }
    }
}
