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
        /*Vector3 forward = this.transform.TransformDirection(Vector3.forward) * 75.0f;
        Vector3 forwardLeft = Quaternion.AngleAxis(-0.8f, Vector3.up) * forward;
        Vector3 forwardLeft2 = Quaternion.AngleAxis(-1.6f, Vector3.up) * forward;
        Vector3 forwardRight = Quaternion.AngleAxis(0.8f, Vector3.up) * forward;
        Vector3 forwardRight2 = Quaternion.AngleAxis(1.6f, Vector3.up) * forward;
        Vector3 leftEye = Quaternion.AngleAxis(-45.0f, Vector3.up) * forward;
        Vector3 rightEye = Quaternion.AngleAxis(45.0f, Vector3.up) * forward;
        Debug.DrawRay(this.transform.position, forward, Color.red);
        Debug.DrawRay(this.transform.position, forwardLeft, Color.green);
        Debug.DrawRay(this.transform.position, forwardLeft2, Color.green);
        Debug.DrawRay(this.transform.position, forwardRight, Color.green);
        Debug.DrawRay(this.transform.position, forwardRight2, Color.green);
        Debug.DrawRay(this.transform.position, leftEye, Color.green);
        Debug.DrawRay(this.transform.position, rightEye, Color.green);*/
    }

    public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (this.foodEnteredInCollision == null)
        {
            this.foodEnteredInCollision = collision.gameObject;
        }
    }
}
