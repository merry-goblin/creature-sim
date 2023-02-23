
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

            Vector3 initialPosition = new Vector3(1.0f, 1.1f, 5.0f);
            FoodSample1 food = new FoodSample1(initialPosition);
            this.AddElement(food);

            base.Load();

            this.brainExchanger.BuildSimSubjectBrain(ref subject);
        }

        protected override bool CheckWorldEnd()
        {
            return true;
        }
    }
}
