
using System;
using System.Collections.Generic;
using UnityEngine;

class FoodSample1 : AbstractElement, IElement
{
    protected static GameObject prefab;
    protected static bool prefabIsLoaded = false;

    public FoodSample1()
    {
    }

    public static void addPrefab(GameObject prefab)
    {
        FoodSample1.prefab = prefab;
        FoodSample1.prefabIsLoaded = true;
    }

    public override void Load()
    {
        if (!FoodSample1.prefabIsLoaded)
        {
            throw new Exception("Food.addPrefab must be called before calling Load");
        }

        //  Unity game object
        this.gameObject = UnityEngine.Object.Instantiate(FoodSample1.prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
