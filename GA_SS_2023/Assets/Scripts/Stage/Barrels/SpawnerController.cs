using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    // Serialized variables
    [SerializeField] private BarrelSpawner[] spawnerArray = new BarrelSpawner[3];

    // private variables
    private Coroutine activateSpawner;
    private int activeSpawners = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BarrelSpawner spawnerObject in spawnerArray)
        {
            if (spawnerObject.isActiveAndEnabled)
            {
                activeSpawners++;
            }
        }

        if (activateSpawner == null && activeSpawners < spawnerArray.Length)
        {
            activateSpawner = StartCoroutine(ActivateSpawner());
        }

        activeSpawners = 0;
    }

    private IEnumerator ActivateSpawner()
    {
        yield return new WaitForSeconds(activeSpawners * 0.5f);
        int i = UnityEngine.Random.Range(0, spawnerArray.Length);
        while(spawnerArray[i].isActiveAndEnabled)
        {
            i = UnityEngine.Random.Range(0, spawnerArray.Length);
        }
        spawnerArray[i].gameObject.SetActive(true);
        spawnerArray[i].SpawnBarrel();
        activateSpawner = null;
    }
}
