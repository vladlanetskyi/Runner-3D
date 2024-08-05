using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Spawner spawner;
    private bool spawnNextChunk = false;
    private GameController gameController;

    private void Awake()
    {
        spawner = transform.parent.GetComponent<Spawner>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        GameObject coinsContainer = transform.Find("Coins").gameObject;

        if (Random.Range(0, 100) < gameController.coinsRandomChance)
        {
            coinsContainer.SetActive(true);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && spawnNextChunk == false)
        {
            spawner.ChunckSpawn(transform.position + Vector3.forward * 100);
            spawnNextChunk = true;
        }
    }
}
