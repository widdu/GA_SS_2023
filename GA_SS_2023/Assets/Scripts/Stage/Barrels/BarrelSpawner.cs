using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    // Serialized variables
    [SerializeField] private int barrelSpawnTimer = 3;
    [SerializeField] private GameObject prefabBarrel;

    // Private variables
    private Coroutine waitBarrelSpawn;

    // Public variables
    public List<GameObject> barrelList; // Not sure yet why this doesn't work as private with public property.

    // Start is called before the first frame update
    void OnEnable()
    {
        waitBarrelSpawn = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitBarrelSpawn == null)
        {
            waitBarrelSpawn = StartCoroutine(WaitBarrelSpawn());
        }
    }

    private IEnumerator WaitBarrelSpawn()
    {
        yield return new WaitForSeconds(barrelSpawnTimer);
        SpawnBarrel();
        StopCoroutine(waitBarrelSpawn);
        waitBarrelSpawn = null;
    }

    public void SpawnBarrel()
    {
        GameObject spawnedBarrel = Instantiate(prefabBarrel, transform.position, transform.rotation);
        Barrel spawnedBarrelScript = spawnedBarrel.GetComponent<Barrel>();
        spawnedBarrelScript.MyBarrelSpawner = this;
        barrelList.Add(spawnedBarrel);
    }

    public void DestroyBarrel(GameObject barrel)
    {
        barrelList.Remove(barrel);
        Destroy(barrel);
    }

    public void DestroyAllBarrels()
    {
        for(int i = barrelList.Count - 1; i >= 0; i--)
        {
            Destroy(barrelList[i]);
        }
        barrelList.Clear();
    }

    public void DeactivateMe()
    {
        gameObject.SetActive(false);
    }
}
