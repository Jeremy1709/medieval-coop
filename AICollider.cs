using UnityEngine;
using System.Collections;

public class AICollider : MonoBehaviour {

    [SerializeField] private GameObject playerObject;
    [SerializeField] private AI aiController;
	// Use this for initialization
	void Start () {

        #region Find player object
        if (gameObject.transform.parent.parent.parent.parent.parent.tag.Equals("NPC"))// && gameObject.transform.parent.parent.parent.parent.parent.gameObject.layer == 8)
        {
            playerObject = gameObject.transform.parent.parent.parent.parent.parent.gameObject;
            //Debug.Log("found 5");
        }
        else if (gameObject.transform.parent.parent.parent.parent.parent.parent.parent.tag.Equals("NPC"))// && gameObject.transform.parent.parent.parent.parent.parent.parent.parent.gameObject.layer == 8)
        {
            playerObject = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.gameObject;
            //Debug.Log("found 7");
        }
        else if (gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.tag.Equals("NPC"))// && gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.gameObject.layer == 8)
        {
            playerObject = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            //Debug.Log("found 8");
        }
        else if (gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag.Equals("NPC"))// && gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject.layer == 8)
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

        aiController = playerObject.GetComponent<AI>();
    }

    // Update is called once per frame
    void Update () {

	}
    public void takeDamage(float dmg, GameObject thisAttacker)
    {
        Debug.Log("Hit for " +dmg+" to the "+gameObject.transform.name);
        aiController.RpcTakeDamage(dmg, thisAttacker);
    }
    /*
    void OnCollisionEnter(Collision col)
    {
        switch (col.transform.tag)
        {
            case "1HMelee": col.GetComponent<>; break;
            case "2HSword": Debug.Log(" "); break;
            case "2HPole": Debug.Log(" "); break;
            case "Ranged": Debug.Log(" "); break;
            default: break;
        }
    }
    */
}
