using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private BarrelSpawner myBarrelSpawner;

    public BarrelSpawner MyBarrelSpawner
    {
        get { return myBarrelSpawner; }
        set { myBarrelSpawner = value; }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Abyss")
        {
            myBarrelSpawner.DestroyBarrel(gameObject);
        }
    }
}
