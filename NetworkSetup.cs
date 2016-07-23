using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkSetup : NetworkBehaviour {

	public override void OnStartLocalPlayer () {
        //Renderer[] rens = GetComponentsInChildren<Renderer>();
        //foreach (Renderer ren in rens)
        //{
        //    ren.enabled = false;
        //}

        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
	}

    // Update is called once per frame
    void PreStartClient () {
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }
}
