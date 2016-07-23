using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    GameObject[] bodyComponentsToDisable; //change name to gameobjects

    [SerializeField]
    Collider characterController;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";

    //[SerializeField]
    //GameObject playerGraphics;  // FOR DISABLING GRAPHICS, NOT NEEDED

    [SerializeField]
    private GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    Camera sceneCamera;

    void Start()
    {
        //Debug.Log(LayerMask.NameToLayer("Water"));//THIS FIXES LayerMask PROBLEM

        if (!isLocalPlayer)// && !isClient) //check if we're controlling the player, if not disable all the components
        {
            //Debug.Log("Is not local player");
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null) {
                sceneCamera.gameObject.SetActive(false);
            }
            DisableBody(); //Disable Body Parts for local player

            /*
            //Disable player graphics for local player, but i already made this
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));
            */

            //Create PlayerUI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;
        }

        GetComponent<Player>().Setup();
    }

    /*
    void SetLayerRecursively (GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
    */

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        characterController.enabled = false;
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void DisableBody()
    {
        for (int i = 0; i < bodyComponentsToDisable.Length; i++)
        {
            bodyComponentsToDisable[i].active = false;
        }
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);

    }


}
