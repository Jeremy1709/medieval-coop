using UnityEngine;
using System.Collections;

public class AiSiteControl : MonoBehaviour {
    private Ray ray;
    private GameObject siteRaycast;
    // Use this for initialization
    void Start () {
        siteRaycast = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        //RaycastHit hit;
        //Ray ray = new Ray(siteRaycast.transform.position, Vector3.forward);

        //if (Physics.Raycast(ray, out hit, 100))//100 is the range of the raycast
        //{
        //    //Debug.Log("raycast hit");
        //    if (hit.transform.gameObject.tag == "Player")
        //    {
        //        Debug.Log("Enemy Sees you");
        //        //hit.transform.gameObject.SendMessage("playerBehaviour", "test message");
        //    }
        //}
        
    }
    void axeStrike(double damage)
    {
       

        RaycastHit hit;
        Ray ray = new Ray(siteRaycast.transform.position, Vector3.forward);

        if (Physics.Raycast(ray, out hit, 100))//100 is the range of the raycast
        {
            Debug.Log("attack");
            //Debug.Log(hit.collider);
            if (hit.transform.gameObject.tag == "Player")
            {
                Debug.Log("hit player");
                hit.collider.SendMessage("ApplyDamage", damage);
                //hit.transform.gameObject.SendMessage("playerBehaviour", "test message");
            }
        }
    }
}







	
	
	

