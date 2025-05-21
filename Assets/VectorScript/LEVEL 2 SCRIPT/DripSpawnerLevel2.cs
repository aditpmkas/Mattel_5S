using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripSpawnerLevel2 : MonoBehaviour
{
    [Header("Drip Settings")]
    public GameObject waterDropPrefab;
    public float spawnInterval = 1f;
    public Vector3 spawnOffset = Vector3.zero;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnDrop();
            timer = 0f;
        }
    }

    void SpawnDrop()
    {
        if (waterDropPrefab != null)
        {
            GameObject drop = Instantiate(waterDropPrefab, transform.position + spawnOffset, Quaternion.identity);
            Destroy(drop, 3f); // Destroy the spawned drop after 3 seconds
        }
        else
        {
            Debug.LogWarning("Water Drop Prefab is not assigned!");
        }
    }
}
