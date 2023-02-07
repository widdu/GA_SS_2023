using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private PlayerController playerController;
    private BoxCollider boxCollider;

    public int collisionCount = 0;

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

        if (collision.gameObject.name == "End")
        {
            GoalScore goalScore = collision.gameObject.GetComponent<GoalScore>();
            playerController.ReachGoal(goalScore);
        }

        if (collision.gameObject.name == "Abyss")
        {
            playerController.AbyssReset();
        }

        playerController.SwitchingTrack = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount--;
    }
}
