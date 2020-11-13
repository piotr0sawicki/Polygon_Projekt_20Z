﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class WaveManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab = null;
    [SerializeField] private BoxCollider[] spawnAreas;
    [SerializeField] private Transform enemyMovementDestination = null;
    [SerializeField, Range(0.0f, 1000.0f)] private int numberOfEnemiesToSpawn = 0;
    private const float SPAWN_HEIGHT = 1.0f;


    private void Awake()
    {
        Assert.IsNotNull(enemyPrefab); 
        Assert.IsTrue(spawnAreas.Length > 0);
        var spawnAreasSet = new HashSet<BoxCollider>();
        foreach(var spawnArea in spawnAreas)
        {
            Assert.IsNotNull(spawnArea);
            Assert.IsFalse(spawnAreasSet.Contains(spawnArea));
            spawnAreasSet.Add(spawnArea);
        }
        //TODO: sprawdzanie duplikatów
        Assert.IsNotNull(enemyMovementDestination);
    }

    private void Start()
    {
        var spawnPosition = new Vector3(0.0f, SPAWN_HEIGHT, 0.0f);

        while (numberOfEnemiesToSpawn > 0)
        {
            var spawningArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
            spawnPosition.x = spawningArea.bounds.extents.x * Random.Range(-1.0f, 1.0f) + spawningArea.transform.position.x;
            spawnPosition.z = spawningArea.bounds.extents.z * Random.Range(-1.0f, 1.0f) + spawningArea.transform.position.z;
            var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            --numberOfEnemiesToSpawn;
        }
    }
}