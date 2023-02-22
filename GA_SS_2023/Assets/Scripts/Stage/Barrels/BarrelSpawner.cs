using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    // Serialized variables
    [SerializeField] private int barrelSpawnTimer = 3;
    [SerializeField] private GameObject[] spawnArray = new GameObject[3];

    // Private variables
    private Coroutine waitBarrelSpawn;
    private GoalScore goalScore;

    // Public variables
    public List<GameObject> barrelList; // Not sure yet why this doesn't work as private with public property.

    private void Awake()
    {
        goalScore = transform.parent.GetComponent<GoalScore>();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        waitBarrelSpawn = null;
    }

    // Update is called once per frame
    private void Update()
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
        int i = UnityEngine.Random.Range(0, spawnArray.Length);
        GameObject prefabBarrel = spawnArray[i];
        GameObject spawnedBarrel = Instantiate(prefabBarrel, transform.position, transform.rotation);
        Barrel spawnedBarrelScript = spawnedBarrel.GetComponent<Barrel>();
        spawnedBarrelScript.MyBarrelSpawner = this;
        barrelList.Add(spawnedBarrel);

        if(i == 2)
        {
            Rigidbody rigidbody = spawnedBarrel.GetComponent<Rigidbody>();
            rigidbody.drag += rigidbody.drag * goalScore.Score;
            rigidbody.angularDrag += rigidbody.angularDrag * goalScore.Score;
        }
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
