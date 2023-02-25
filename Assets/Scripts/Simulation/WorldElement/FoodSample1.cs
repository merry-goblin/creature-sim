
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CreatureSim
{
    class FoodSample1 : AbstractElement, IElement
    {
        protected static GameObject prefab;
        protected static bool prefabIsLoaded = false;

        protected Vector3 initialPosition;

        protected FoodSample1Script gameObjectController;

        public FoodSample1(Vector3 position):base()
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
            this.gameObject.name = "Food_" + FoodSample1.PeekNewId();
            this.gameObjectController = this.gameObject.GetComponent<FoodSample1Script>();
        }

        public override void Unload()
        {
            if (this.gameObject != null)
            {
                UnityEngine.Object.Destroy(this.gameObject);
            }

            base.Unload();
        }

        public override void Update()
        {
            if (this.gameObject != null)
            {
                if (this.gameObjectController != null)
                {
                    if (this.gameObjectController.hasBeenEaten)
                    {
                        this.toUnload = true;
                    }
                }
            }

            base.Update();
        }

    }
}
