
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

        protected static int nbInputNeurons = 1;
        protected static int nbOutputNeurons = 2;
        protected static int nbHiddenLayers = 5; // must be > 0
        protected static int nbHiddenNeuronsByLayer = 12;

        protected float subjectRotationModifier = 45.0f;
        protected float subjectSpeedModifier = 20.0f;

        protected float energy = 75.0f;

        public float score = 0.0f;

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
            this.gameObject.AddComponent<SubjectSample1Script>();

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

        protected override void ApplyInput()
        {
            if (this.gameObject != null)
            {
                Vector3 forward = this.gameObject.transform.TransformDirection(Vector3.forward);
                Vector3 position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f, this.gameObject.transform.position.z);
                Ray ray = new Ray(position, forward) ;
                float input1 = -1.0f;
                if (Physics.Raycast(ray, 50.0f))
                {
                    input1 = 1.0f;
                    Debug.DrawRay(position, forward * 50.0f, Color.red);
                }
                else
                {
                    Debug.DrawRay(position, forward * 50.0f, Color.green);
                }
                List<float> values = new List<float>();
                values.Add(input1);
                
                this.neuralNetwork.ApplyInputValues(values);
            }
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
