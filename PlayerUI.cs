using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {

    private Canvas UI;

	// Use this for initialization
	void Start () {
        UI = GetComponentInChildren<Canvas>();
        //UI.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
