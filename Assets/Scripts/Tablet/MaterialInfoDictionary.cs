using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class MaterialInfoDictionary : MonoBehaviour
{
    public MaterialInfo this[Material material]
    {
        get { return MaterialInfos.FirstOrDefault(x => x.Material.mainTexture == material.mainTexture); }
    }

    public MaterialInfo[] MaterialInfos;
}

[Serializable]
public class MaterialInfo
{
    public Material Material;
    public string Name;

    [Multiline]
    public string Description;

    [Multiline]
    public string Advantages;
}