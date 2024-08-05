using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer Skin;

    private void Start()
    {
        Skin.material = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkinSystem>().skinnedMeshRenderer.material;
    }
}
