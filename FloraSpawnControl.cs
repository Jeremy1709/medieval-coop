using UnityEngine;
using System.Collections;

public class FloraSpawnControl : MonoBehaviour {
    [SerializeField] private int numberOfSpawns;
	// Use this for initialization
	void Start () {
        numberOfSpawns = transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


/*

Spawn control should give maybe a 25% chance of enabling every spawn.
Then have a range the scale is given by, which gives the size and value of the spawn. The larger, the more material from harvesting the resource.


    */