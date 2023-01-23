using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private BoxCollider boxCollider;

    public int collisionCount = 0;

    public bool IsActive { get { return collisionCount > 0; } }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        if(boxCollider == null)
        {
            Debug.LogWarning("Can't get box collider component for player object's player collider component!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount--;
    }
}
