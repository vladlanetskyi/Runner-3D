using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().CollectCoin();
            Destroy(gameObject);
        }

    }

}
