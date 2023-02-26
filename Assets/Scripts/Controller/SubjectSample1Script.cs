using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectSample1Script : MonoBehaviour
{
    public UnityEngine.GameObject foodEnteredInCollision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = this.transform.TransformDirection(Vector3.forward) * 50.0f;
        Debug.DrawRay(this.transform.position, forward, Color.green);
    }

    public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (foodEnteredInCollision == null)
        {
            this.foodEnteredInCollision = collision.gameObject;
        }
    }
}
