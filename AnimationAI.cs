using UnityEngine;
using System.Collections;

public class AnimationAI : MonoBehaviour {
	
	public Transform target;
	public float lookAtDistance = 50f;
	public float chaseRange = 30f;
	
	public float Damping = 6f;
	public float TheDamage = 10f;
	public float attackRepeatTime = 1.25f;
	public float attackRepeatHit = .833333f;

    public float attackRange = 2f;
    public float stopRange = 2f;
    //Navmesh stuff===========================
    public float acceleration = 2f;
    public float deceleration = 60f;
    public float closeEnoughMeters = 2f;
    private NavMeshAgent agent; //The nav mesh controller for movement.
    //=========================================
	private float attackTime; //Time for the attack animation
	private float attackHit; //Time the weapon hits in the animation, I should make this work for when it collides maybe
	
	public AudioClip takeDamageSound;
	public AudioClip hitWallClip;
	public AudioClip hitEnemyClip;
	public AudioClip swingWeaponClip;

	public GameObject weapon01;
	public GameObject axeCollider;
    private GameObject sightRaycast;
    private bool attacking;

	private Animator anim;

	private float damage;

	private bool swingStart;

	public bool swingOverride;  //SHOULD BE PRIVATE
	private bool chase;
	private bool dontPlay;
    private double currentTime;

    private string attackerBehaviour;
    private double behaviourReset;



    void Start () {
		agent = GetComponent<NavMeshAgent>();//Initialize the navMeshAgent
		anim = gameObject.GetComponentInChildren<Animator>();
        anim.SetBool ("Swing", false);
		anim.SetFloat ("Walk", 0f);
		target = GameObject.FindGameObjectWithTag("Player").transform;//Initalize the target
        behaviourReset = Time.time + 1;
        sightRaycast = GameObject.Find("SightRaycast");
        attacking = false;
        swingOverride = false;
    }
	
	void Update()
	{//UPDATE start=============================================================

        //if axe collider is disabled. attacking should equal false.
        //if (axeCollider.active == false)
        //{
        //    attacking = false;
        //}

        //TO PREVENT SLIDING WHEN STOPPING===========================
        if (agent)
        {

            // speed up slowly, but stop quickly
            if (agent.hasPath)
            {
                agent.acceleration = (agent.remainingDistance < closeEnoughMeters) ? deceleration : acceleration;
            }
        }
        //To reset attacker behaviour==========================================
        if (Time.time > behaviourReset)
        {
            attackerBehaviour = "idle";
            behaviourReset = Time.time + 1;

        }
        //CHASING/ATTACKING METHODS===================================================
        float Distance = Vector3.Distance(target.position, transform.position);

        //DELETE, FOR DEBUGGING VV
        anim.SetFloat("Distance", Distance);

        if (Distance < attackRange) // ATTACK
		{
			lookAt ();
			anim.SetFloat ("Walk", 0f);
			
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("SwingingAxe") == false && attacking == false)
            {
                //Debug.Log("attacking runner runs " + attacking);
                if (attacking == false)
                {
                    attack();
                }
                attacking = true;
            }
            else
            {
                anim.SetBool("Swing", false);
            }
        }
		else if (Distance < chaseRange) // CHASE
		{
            //Debug.Log(Distance + " time is "+Time.time);
			chase = true;
			agent.destination = target.position;
			anim.SetFloat ("Walk", 1.5f);
		}
		else if(Distance < lookAtDistance) // LOOK AT
		{
			agent.ResetPath();
			lookAt ();
			anim.SetFloat ("Walk", 0f);
		}
		else if (Distance > lookAtDistance) // OUT OF RANGE
		{	
			agent.ResetPath();
			anim.SetFloat ("Walk", 0f);
		}


	} // UPDATE END ==============================================================================
    //ATTACK CONTROLS===========================================================================
    void attack()
    {
        //Debug.Log("Attack runs");
        anim.SetBool("Swing", true);
        Invoke("swingCollider", 0.54f);//This number should be different for each weapon 
        Invoke("swingColliderEnd", 0.83f);
        attacking = true;
    }
    void swingCollider()
    {
        axeCollider.SetActive(true);     //RE ENABLE THIS ONCE THE RAYCAST WORKS
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(swingWeaponClip);
        chase = false;
    }
    void swingColliderEnd()
    {
        //Debug.Log(swingOverride + "< the swing override");
        if (attacking == true)
        {
            target.SendMessage("ApplyDamage", 10f);
        }
        attacking = false;
        //sightRaycast.SendMessage("axeStrike", 10f);
        axeCollider.SetActive(false);
        GetComponent<AudioSource>().Stop();

    }
    void swingColliderStop()
    {
        axeCollider.SetActive(false);
    }
    void lookAt ()
	{	
		var rotation = Quaternion.LookRotation(target.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
	}
    //==========================================================================================
	public void hitWall(){
		Debug.Log ("HITWILL BEING PLAYED FROM PLAYER HIT");
		anim.CrossFade("RunningArms", 0.1f, 1); 
		GetComponent<AudioSource>().Stop();
        attacking = false;

    }
	public float hitEnemy(){
		anim.CrossFade("RunningArms", 0.1f, 1); 
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().PlayOneShot(hitEnemyClip);
		weapon01.SendMessage ("getDamage");
		swingOverride = true;
        attacking = false;
        return damage;
	}
	

	public void returnDamage(float inputDamage){
		damage = inputDamage;
	}

	public void DeathAnimation(){
		Debug.Log ("Death animation plays here");
	}

    	
	void FindDamage ()
	{
		TheDamage = 10;
	}

    void opponentBehaviour(string message)
    {
        attackerBehaviour = message;
        
    }
}