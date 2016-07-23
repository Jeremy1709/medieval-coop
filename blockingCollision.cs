using UnityEngine;
using System.Collections;

public class blockingCollision : MonoBehaviour {

    private BoxCollider colliderLeft;
    private BoxCollider colliderRight;
    private BoxCollider colliderFront;

    // Use this for initialization
    void Start () {
        colliderLeft = GameObject.Find("Left").GetComponent<BoxCollider>();
        colliderRight = GameObject.Find("Right").GetComponent<BoxCollider>();
        colliderFront = GameObject.Find("Front").GetComponent<BoxCollider>();
        colliderLeft.enabled = false;
        colliderRight.enabled = false;
        colliderFront.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
