using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    private Coroutine waitBarrelSpawn;
    [SerializeField] private int barrelSpawnTimer = 3;
    [SerializeField] private GameObject prefabBarrel;
    public List<GameObject> barrelList;

    // Start is called before the first frame update
    void Start()
    {
        
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
        GameObject spawnedBarrel = Instantiate(prefabBarrel, transform.position, transform.rotation);
        Barrel spawnedBarrelScript = spawnedBarrel.GetComponent<Barrel>();
        spawnedBarrelScript.MyBarrelSpawner = this;
        barrelList.Add(spawnedBarrel);
        StopCoroutine(waitBarrelSpawn);
        waitBarrelSpawn = null;
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
}
