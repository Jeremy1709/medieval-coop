using UnityEngine;
using System.Collections;

public class weaponCollision : MonoBehaviour {

    private BoxCollider boxCollider;
    private weaponStats weaponStatsScript; 

    private PlayerController playerController;
    //private Player_Equipment playerEquipment;
    public GameObject Effect;//to store particles when bullet hits. like blood or sparks
    public GameObject BloodEffect;//to store particles when bullet hits. like blood or sparks
    private bool isColliding;
    Transform colTransform;

    private Transform target;

    private float startTime;
    private float nextCheck;
    private float timeChange;
    private float lastLocation;

    [SerializeField]
    private Vector3 lockedPosition;
    private bool attacking;
    private Transform transform; //DELETE THIS AND LOOK AT THE ABOVE

    public float[] positionValues;
    [SerializeField]
    private Transform thisTransform;

    // Use this for initialization
    void Start () {

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        //Debug.Log(gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject);


        playerController = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<PlayerController>();
        //playerEquipment = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<Player_Equipment>();
        //Debug.Log(playerController);
        colTransform = GetComponent<Transform>(); //DELETE OR CHANGE NAME
        thisTransform = GetComponent<Transform>();

        startTime = Time.time;
        nextCheck = startTime + 0.2f;
        lastLocation = colTransform.position.x;

        //transform = GetComponent<Transform>();
        //attacking = false;
        //lockedPosition = transform.position;

        thisTransform.localPosition = new Vector3(positionValues[0], positionValues[1], positionValues[2]);
        thisTransform.localEulerAngles = new Vector3(positionValues[3], positionValues[4], positionValues[5]);
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (attacking)
        {
            thisTransform.localPosition = new Vector3(positionValues[0], positionValues[1], positionValues[2]);
            thisTransform.localEulerAngles = new Vector3(positionValues[3], positionValues[4], positionValues[5]);
        }
        */
        isColliding = false;

        /*
        if (Time.time > nextCheck)
        {
            nextCheck += 0.1f;
            timeChange = lastLocation - colTransform.position.x;
            lastLocation = colTransform.position.x;
            Debug.Log("change in x is " + timeChange);
        }
        */
    }

    /*
    void OnTriggerEnter(Collision col)
    {
        Debug.Log("Trigger HAPPENING");
    }
    */

    public float[] returnPostion()
    {
        return positionValues;
    }

    void OnCollisionStay(Collision col)
    {
        thisTransform.localPosition = new Vector3(positionValues[0], positionValues[1], positionValues[2]);
        thisTransform.localEulerAngles = new Vector3(positionValues[3], positionValues[4], positionValues[5]);
        Debug.Log("LOCAL POSITION AND ROTATION RESET");
    }

    //void OnTriggerEnter(Collision col)
   // {
    //    Debug.Log("Trigger has been entered");
    //}


//OLD ON COLLISION ENTER VV, im working on this july 21
    
    void OnCollisionEnter(Collision col)
    //void OnTriggerEnter(Collision col)
    {
        Debug.Log("A COLLISION IS HAPPENING NAME: " +col.transform.name+ "  TAG: "+ col.transform.tag);
        Debug.Log("collider is "+col.collider);
        if (col.transform.tag == "Surface")
        {
            if (isColliding) return;
            isColliding = true;

            Debug.Log("Surface was hit, name is  " + col.gameObject.name);

            ContactPoint contact = col.contacts[0];
            Vector3 pos = contact.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);

            playerController.hitWall();
            GameObject particle = (GameObject)Instantiate(Effect, pos, rot);
            //			particle.DestroyMe();
            //			Destroy(particle, .2f); //2 is for 2 seconds
        }
        else if (col.transform.tag == "HitBox")     //gameObject.name == "Fireball(Clone)")
        {
            Debug.Log("Player was hit in "+col.gameObject.transform.name); 
            if (isColliding) return;
            isColliding = true;


            //Debug.Log("hit box entered");
            //FIRST CALL ON WHATEVER SCRIPT FINDS DAMAGE OF WEAPON. THE WEAPON SHOULD CONTAIN THIS SCRIPT.
            //SET TARGET TO THE HITBOX. SEND A DAMAGE VALUE TO THE HITBOX JUST LIKE WITH THE APPLYDAMAGE THING
            //THEN THE HITBOX WILL SEND THE DAMAGE TO THE HEALTH OF THE ENEMY. DONE.
            float dmg = playerController.hitEnemy();
            GameObject thisPlayer = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            col.gameObject.GetComponent<bodyCollider>().takeDamage(dmg);//, thisPlayer);
            //col.gameObject.SendMessage("sendDamage", dmg);

            ContactPoint contact = col.contacts[0];
            Vector3 pos = contact.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            GameObject particle = (GameObject)Instantiate(BloodEffect, pos, rot);
        }
        else if (col.transform.tag == "AIHitbox")     //gameObject.name == "Fireball(Clone)")
        {
            boxCollider.enabled = false;
            Debug.Log("Player was hit in " + col.gameObject.transform.name);
            if (isColliding) return;
            isColliding = true;


            //Debug.Log("hit box entered");
            //FIRST CALL ON WHATEVER SCRIPT FINDS DAMAGE OF WEAPON. THE WEAPON SHOULD CONTAIN THIS SCRIPT.
            //SET TARGET TO THE HITBOX. SEND A DAMAGE VALUE TO THE HITBOX JUST LIKE WITH THE APPLYDAMAGE THING
            //THEN THE HITBOX WILL SEND THE DAMAGE TO THE HEALTH OF THE ENEMY. DONE.
            float dmg = playerController.hitEnemy();
            GameObject thisPlayer = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            col.gameObject.GetComponent<AICollider>().takeDamage(dmg, thisPlayer);
            //col.gameObject.SendMessage("sendDamage", dmg);

            ContactPoint contact = col.contacts[0];
            Vector3 pos = contact.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            GameObject particle = (GameObject)Instantiate(BloodEffect, pos, rot);
        }
    }
    

//I NEED TO GET THIS ONE WORKING
/*
    void OnTriggerEnter(Collider col)
    //void OnTriggerEnter(Collision col)
    {
        Debug.Log("A COLLISION IS HAPPENING NAME: " + col.transform.name + "  TAG: " + col.transform.tag);

        if (col.transform.tag == "Surface")
        {
            if (isColliding) return;
            isColliding = true;

            Debug.Log("Surface was hit, name is  " + col.gameObject.name);

            ContactPoint contact = col.contacts[0];
            Vector3 pos = contact.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);

            playerController.hitWall();
            GameObject particle = (GameObject)Instantiate(Effect, pos, rot);
            //			particle.DestroyMe();
            //			Destroy(particle, .2f); //2 is for 2 seconds
        }
        else if (col.transform.tag == "HitBox")     //gameObject.name == "Fireball(Clone)")
        {
            Debug.Log("Player was hit in " + col.gameObject.transform.name);
            if (isColliding) return;
            isColliding = true;


            //Debug.Log("hit box entered");
            //FIRST CALL ON WHATEVER SCRIPT FINDS DAMAGE OF WEAPON. THE WEAPON SHOULD CONTAIN THIS SCRIPT.
            //SET TARGET TO THE HITBOX. SEND A DAMAGE VALUE TO THE HITBOX JUST LIKE WITH THE APPLYDAMAGE THING
            //THEN THE HITBOX WILL SEND THE DAMAGE TO THE HEALTH OF THE ENEMY. DONE.
            float dmg = playerController.hitEnemy();
            GameObject thisPlayer = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            col.gameObject.GetComponent<bodyCollider>().takeDamage(dmg);//, thisPlayer);
            //col.gameObject.SendMessage("sendDamage", dmg);

            ContactPoint contact = col.contacts[0];
            Vector3 pos = contact.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            GameObject particle = (GameObject)Instantiate(BloodEffect, pos, rot);
        }
        else if (col.transform.tag == "AIHitBox")     //gameObject.name == "Fireball(Clone)")
        {
            Debug.Log("Player was hit in " + col.gameObject.transform.name);
            if (isColliding) return;
            isColliding = true;


            //Debug.Log("hit box entered");
            //FIRST CALL ON WHATEVER SCRIPT FINDS DAMAGE OF WEAPON. THE WEAPON SHOULD CONTAIN THIS SCRIPT.
            //SET TARGET TO THE HITBOX. SEND A DAMAGE VALUE TO THE HITBOX JUST LIKE WITH THE APPLYDAMAGE THING
            //THEN THE HITBOX WILL SEND THE DAMAGE TO THE HEALTH OF THE ENEMY. DONE.
            float dmg = playerController.hitEnemy();
            GameObject thisPlayer = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            col.gameObject.GetComponent<AICollider>().takeDamage(dmg, thisPlayer);
            //col.gameObject.SendMessage("sendDamage", dmg);

            ContactPoint contact = col.contacts[0];
            Vector3 pos = contact.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            GameObject particle = (GameObject)Instantiate(BloodEffect, pos, rot);
        }
    }
    */

    public void swingStart()
    {
        boxCollider.enabled = true;
        attacking = true;
        //thisTransform.localPosition = new Vector3(positionValues[0], positionValues[1], positionValues[2]);
        //thisTransform.localEulerAngles = new Vector3(positionValues[3], positionValues[4], positionValues[5]);
    }

    public void swingEnd()
    {
        boxCollider.enabled = false;
        attacking = false;
        //thisTransform.localPosition = new Vector3(positionValues[0], positionValues[1], positionValues[2]);
        //thisTransform.localEulerAngles = new Vector3(positionValues[3], positionValues[4], positionValues[5]);
    }
    

}

