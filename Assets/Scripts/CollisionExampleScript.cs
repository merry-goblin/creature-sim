
using UnityEngine;

public class CollisionExampleScript : MonoBehaviour
{
    GameObject[] mainController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Debug.Log("Detection: " + collision.gameObject.name);
        mainController = GameObject.FindGameObjectsWithTag("GameController");
        if (mainController.Length == 1)
        {
            var mainScript = mainController[0].GetComponent<MoveCreatureScript>();
            mainScript.DebugCollision();
        }
        /*if (other.name == "Detection Zone")
        {
            inSight = true;
        }*/
    }
}
