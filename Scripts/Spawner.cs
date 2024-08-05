using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] chunks;
    [Range(0, 100)] public int clearChunkChance;

    private void Start()
    {
        Vector3 spawnPosition = transform.position;
        for (int i = 0; i < 10; i++)
        {
            GameObject SpawnChunk;

            if (clearChunkChance < Random.Range(0, 100))
            {
                int randomChunk = Random.Range(1, chunks.Length);
                SpawnChunk = chunks[randomChunk];
            }
            else
            {
                SpawnChunk = chunks[0];
            }

            if (i == 0)
            {
                SpawnChunk = chunks[0];
            }

            Instantiate(SpawnChunk, spawnPosition, Quaternion.identity, transform);
            spawnPosition.z += 10;
        }
    }
    public void ChunckSpawn(Vector3 position)
    {

        Instantiate(chunks[Random.Range(0, chunks.Length)], position, Quaternion.identity, transform);

    }
}


   

