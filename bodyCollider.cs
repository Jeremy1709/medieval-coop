using UnityEngine;
using System.Collections;

public class bodyCollider : MonoBehaviour {

    [SerializeField] private GameObject playerObject;
    [SerializeField] private Player playerController;

	// Use this for initialization
	void Start () {

        #region Find player object
        if (gameObject.transform.parent.parent.parent.parent.parent.name.Contains("Player "))// && gameObject.transform.parent.parent.parent.parent.parent.gameObject.layer == 8)
        {
            playerObject = gameObject.transform.parent.parent.parent.parent.parent.gameObject;
            //Debug.Log("found 5");
        }
        else if (gameObject.transform.parent.parent.parent.parent.parent.parent.parent.name.Contains("Player "))// && gameObject.transform.parent.parent.parent.parent.parent.parent.parent.gameObject.layer == 8)
        {
            playerObject = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.gameObject;
            //Debug.Log("found 7");
        }
        else if (gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.name.Contains("Player "))// && gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.gameObject.layer == 8)
        {
            playerObject = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            //Debug.Log("found 8");
        }
        else if (gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.name.Contains("Player "))// && gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject.layer == 8)
        {
            playerObject = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            //Debug.Log("found 9");
        }
        else
        {
            Debug.Log("NOT FOUND");
            Debug.Log(gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject);
        }
        #endregion

        playerController = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {
	
	}
    public void takeDamage(float dmg)
    {
        Debug.Log("Hit for " +dmg+" to the "+gameObject.transform.name);
        playerController.RpcTakeDamage(dmg);
    }
}
