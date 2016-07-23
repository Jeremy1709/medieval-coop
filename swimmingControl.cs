using UnityEngine;
using System.Collections;

public class swimmingControl : MonoBehaviour
{

    Transform theDoor; //I think this means it can be a value remembered by unity. if not private
    private bool drawGUI = false;
    private bool doorIsClosed = true;

    void Update()
    {
        if (drawGUI == true && Input.GetKeyDown(KeyCode.E))
        {
            if (doorIsClosed == true)
            {
                openDoor();
            }
            else
            {
                closeDoor();
            }
        }
    }

    void OnTriggerEnter(Collider theCollider )
    {
        if (theCollider.tag == "Player")
        {
            PlayerController controller = theCollider.GetComponent<PlayerController>();
            controller.isSwimming();
        }
    }

    void OnTriggerExit(Collider theCollider)
    {
        if (theCollider.tag == "Player")
        {
            PlayerController controller = theCollider.GetComponent<PlayerController>();
            controller.notSwimming();
        }
    }

    void OnGUI()
    {
        if (drawGUI == true)
        {
            Debug.Log(" ");

        }
    }

    void openDoor()
    {
        if (doorIsClosed == true)
        {
            Debug.Log(" ");
        }
    }

    void closeDoor()
    {
        if (doorIsClosed == false)
        {
            Debug.Log(" ");
        }
    }

}