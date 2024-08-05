using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinSystem : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Material[] skins;

    private void Start()
    {
        skinnedMeshRenderer.material = skins[PlayerPrefs.GetInt("CurrentSkin")];
    }
}
