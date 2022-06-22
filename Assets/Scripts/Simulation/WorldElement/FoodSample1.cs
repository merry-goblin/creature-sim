
using System;
using System.Collections.Generic;
using UnityEngine;

class FoodSample1 : AbstractElement, IElement
{
    protected static GameObject prefab;
    protected static bool prefabIsLoaded = false;

    protected Vector3 initialPosition;

    public FoodSample1(Vector3 position)
    {
        this.initialPosition = position;
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
        this.gameObject = UnityEngine.Object.Instantiate(FoodSample1.prefab, this.initialPosition, Quaternion.identity);
    }
}
