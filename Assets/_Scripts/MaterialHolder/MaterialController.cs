using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public Material defaultMaterial;
    public List<MaterialBro> materials = new();
    

    public Material GetMaterialByEnum(MaterialEnum targetEnum)
    {
        foreach (MaterialBro matBro in materials)
        {
            if (matBro.materialEnum == targetEnum)
                return matBro.Material;
        }
        return defaultMaterial;
    }
}

[Serializable]
public class MaterialBro
{
    public MaterialEnum materialEnum;
    public Material Material;
}

public enum MaterialEnum
{
    Green,
    Red,
    Blue,
    Yellow,
    Purple,
    White,
    Black
}