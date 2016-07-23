using UnityEngine;
using System.Collections;

public class CursorLock : MonoBehaviour {

    public bool inMenu = false;
    public bool isPaused = false;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                inMenu = true;
            }
            else
            {
                inMenu = false;
            }
        }

        if (inMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}




