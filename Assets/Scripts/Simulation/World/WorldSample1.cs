
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CreatureSim
{
    public class WorldSample1 : AbstractWorld, IWorld
    {
        public WorldSample1() : base()
        {
        }

        public override void Load()
        {
            SubjectSample1 subject = new SubjectSample1();
            this.AddSubject(subject);

            Vector3 initialPosition = new Vector3(1.0f, 1.1f, 5.0f);
            FoodSample1 food = new FoodSample1(initialPosition);
            this.AddElement(food);

            base.Load();
        }

        protected override bool CheckWorldEnd()
        {
            return true;
        }
    }
}
