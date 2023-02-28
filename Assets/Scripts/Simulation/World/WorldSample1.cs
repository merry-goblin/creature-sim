
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CreatureSim
{
    public class WorldSample1 : AbstractWorld, IWorld
    {
        protected BrainExchangerSample1 brainExchanger;

        public WorldSample1(ref BrainExchangerSample1 brainExchanger) : base()
        {
            this.brainExchanger = brainExchanger;
        }

        public override void Load()
        {
            SubjectSample1 subject = new SubjectSample1(ref this.brainExchanger);
            this.AddSubject(subject);

            this.LoadFoodElements(100);

            base.Load();

            this.brainExchanger.BuildSimSubjectBrain(ref subject);
        }

        protected void LoadFoodElements(int nbFoodElements)
        {
            for (int i = 0; i < nbFoodElements; i++)
            {
                float x = ToolBox.GetRandomFloat(-100.0f, 100.0f);
                float z = ToolBox.GetRandomFloat(-100.0f, 100.0f);
                FoodSample1 food = new FoodSample1(new Vector3(x, 1.1f, z));
                this.AddElement(food);
            }
        }

        protected override bool CheckWorldEnd()
        {
            return true;
        }
    }
}
