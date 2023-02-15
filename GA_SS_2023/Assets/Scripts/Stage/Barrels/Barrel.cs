using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // Private variables
    private BarrelSpawner myBarrelSpawner;
    private new Rigidbody rigidbody;
    private int trackLayer = 10;

    public BarrelSpawner MyBarrelSpawner
    {
        get { return myBarrelSpawner; }
        set { myBarrelSpawner = value; }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == trackLayer)
        {
            TrackController trackController = collision.gameObject.GetComponent<TrackController>();
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z - trackController.TrackSpeedF); // Multipliers?
            rigidbody.angularVelocity = new Vector3(rigidbody.angularVelocity.x - trackController.TrackSpeedF, rigidbody.angularVelocity.y, rigidbody.angularVelocity.z);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Abyss")
        {
            myBarrelSpawner.DestroyBarrel(gameObject);
        }
    }
}
