﻿
using System;
using System.Collections.Generic;
using UnityEngine;
using FeedForwardNeuralNetwork;

namespace CreatureSim
{
    public class SubjectSample1 : AbstractSubject, ISubject
    {
        public bool oneUseDebugOnly = true;

        public bool initSynapsesRandomly = true;

        protected static GameObject prefab;
        protected static bool prefabIsLoaded = false;

        protected static int nbInputNeurons = 2;
        protected static int nbOutputNeurons = 2;
        protected static int nbHiddenLayers = 4; // must be > 0
        protected static int nbHiddenNeuronsByLayer = 5;

        protected float subjectRotationModifier = 45.0f;
        protected float subjectSpeedModifier = 20.0f;

        protected float energy = 10.0f;

        protected float score = 0.0f;

        protected BrainExchangerSample1 brainExchanger;

        public SubjectSample1(ref BrainExchangerSample1 brainExchanger)
        {
            this.brainExchanger = brainExchanger;
        }

        public static void addPrefab(GameObject prefab)
        {
            SubjectSample1.prefab = prefab;
            SubjectSample1.prefabIsLoaded = true;
        }

        public override void Load()
        {
            if (!SubjectSample1.prefabIsLoaded)
            {
                throw new Exception("SubjectSample1.addPrefabs must be called before calling Load");
            }

            //  Unity game object
            this.gameObject = UnityEngine.Object.Instantiate(SubjectSample1.prefab, new Vector3(0, 0, 0), Quaternion.identity);

            //  Subject neural network
            IActivation activation = new TanhActivation();
            NeuralNetwork neuralNetwork = new NeuralNetwork(
                SubjectSample1.nbInputNeurons,
                SubjectSample1.nbOutputNeurons,
                SubjectSample1.nbHiddenLayers,
                SubjectSample1.nbHiddenNeuronsByLayer,
                this.initSynapsesRandomly,
                ref activation
            );
            neuralNetwork.inputLayer.neurons[0].outputValue = 0.25f; // no real input for this subject so far
            neuralNetwork.inputLayer.neurons[1].outputValue = -0.5f; // no real input for this subject so far
            this.neuralNetwork = neuralNetwork;

            base.Load();
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
                this.CheckCollisionWithFood();
            }

            base.Update();
        }

        protected void CheckCollisionWithFood()
        {
            SubjectSample1Script subjectController = this.gameObject.GetComponent<SubjectSample1Script>();
            if (subjectController != null)
            {
                if (subjectController.foodEnteredInCollision != null)
                {
                    //  Fitness in genetic algorithms
                    this.score += 10.0f;

                    //  Destroy food (indirectly)
                    FoodSample1Script foodController = subjectController.foodEnteredInCollision.GetComponent<FoodSample1Script>();
                    if (foodController != null)
                    {
                        foodController.hasBeenEaten = true;
                    }

                    //  Remove collision information of current subject's game object
                    subjectController.foodEnteredInCollision = null;
                }
            }
        }

        public float Fitness()
        {
            return this.score;
        }

        protected override void ApplyOutput()
        {
            //Debug.Log(this.energy);

            if (this.energy > 0)
            {
                List<float> outputList = this.GetOutput();

                float subjectRotation = outputList[0];
                float subjectSpeed = outputList[1];
                subjectSpeed = this.ForceMinimumSpeed(subjectSpeed);

                this.gameObject.transform.Rotate(0.0f, subjectRotation * Time.deltaTime * this.subjectRotationModifier, 0.0f, Space.Self);
                this.gameObject.transform.Translate(Vector3.forward * subjectSpeed * Time.deltaTime * this.subjectSpeedModifier, Space.Self);

                this.gameObject.AddComponent<SubjectSample1Script>();

                float consomption = (Math.Abs(subjectSpeed) + Math.Abs(subjectRotation) / 5.0f + 1.0f) * Time.deltaTime * 5.0f;
                this.energy -= consomption;
                if (this.energy <= 0)
                {
                    this.energy = 0;
                    base.EndSimulationForSubject();
                }
            }
        }

        /**
         * Unity physics doesn't collide if subject is too slow
         */
        protected float ForceMinimumSpeed(float speed)
        {
            if (speed > 0)
            {
                if (speed < 0.3f)
                {
                    speed = 0.3f;
                }
            }
            else
            {
                if (speed > -0.3f)
                {
                    speed = -0.3f;
                }
            }

            return speed;
        }
    }
}
