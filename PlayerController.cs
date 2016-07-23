using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

//handles input
//[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

    //public Behaviour fpsController;
    public FirstPersonController fpsController;
    [SerializeField] private GameObject weaponParent;
    [SerializeField] private BoxCollider weaponCollider;
    public Camera mainCam;
    private attackStart thisAttack;
    [SerializeField]
    private float weaponAttackSpeed;
    /*
    private GameObject headCollider;
    private GameObject upperBodyCollider;
    private GameObject lowerbodyCollider;
    private GameObject upperRightArmCollider;
    private GameObject lowerRightArmCollider;
    private GameObject upperLeftArmCollider;
    private GameObject lowerLeftArmCollider;
    private GameObject upperRightLegCollider;
    private GameObject lowerRightLegCollider;
    private GameObject upperLeftLegCollider;
    private GameObject lowerLeftLegCollider;
    */
    private CapsuleCollider headCollider;
    private CapsuleCollider upperBodyCollider;
    private CapsuleCollider lowerbodyCollider;
    private CapsuleCollider upperRightArmCollider;
    private CapsuleCollider lowerRightArmCollider;
    private CapsuleCollider upperLeftArmCollider;
    private CapsuleCollider lowerLeftArmCollider;
    private CapsuleCollider upperRightLegCollider;
    private CapsuleCollider lowerRightLegCollider;
    private CapsuleCollider upperLeftLegCollider;
    private CapsuleCollider lowerLeftLegCollider;



    [SerializeField]
    private bool swimming = false;

    //public Collider collider;
    [SerializeField]
    private bool isGrounded = true;
    [SerializeField]
    private bool sprinting = false;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float runSpeed = 40f;
    [SerializeField]
    private float walkSpeed = 1.5f;

    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    private bool walkF = false;
    private bool walkB = false;
    private bool turnR = false;
    private bool turnL = false;


    [Header("Spring settings")]
    /*
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;
    */

    private PlayerMotor motor;
    //private ConfigurableJoint joint;
    private Animator animator;
    private CharacterController controller;
    private Transform thisTransform;

    private UnityStandardAssets.Characters.FirstPerson.MouseLook[] mous;

    [SerializeField] private GameObject localPlayerUI;
    [SerializeField] private UiController uiController;

    [SerializeField]
    private attackMotor attackMot;



    //[SerializeField] private FirstPersonController fpsController;

    //private MouseLook playerLookCamera;

    float testTime = 0f;
    float testInterval = .2f;



    void Start()
    {

        weaponParent = GameObject.Find(gameObject.transform.name + 
            "/Graphics/PlayerModel/Armature/spine2/clavicle_R/upArm_R/lowArm_R/hand_R/Weapon");
        if (weaponParent.transform.childCount > 0)
        {
            equipUnarmed();
        }
        

        controller = GetComponent<CharacterController>();
        motor = GetComponent<PlayerMotor>();
        //joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();
        thisTransform = GetComponent<Transform>();
        attackMot = GetComponent<attackMotor>();

        if (weaponParent.transform.childCount > 0)
        {
            weaponCollider = weaponParent.GetComponentInChildren<BoxCollider>();
            weaponCollider.enabled = false;
        }
        else
        {
            Debug.Log("instantiate hands prefab on the weapon slot. also change other stuff for that");
        }
        //FINDING BODY PART COLLIDERS

        headCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/spine2/neck/head/headCollider").GetComponent<CapsuleCollider>();
        upperBodyCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/spine2/upperBodyCollider").GetComponent<CapsuleCollider>();
        lowerbodyCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/spine1/lowerBodyCollider").GetComponent<CapsuleCollider>();
        upperRightArmCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/spine2/clavicle_R/upArm_R/upperRightArmCollider").GetComponent<CapsuleCollider>();
        lowerRightArmCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/spine2/clavicle_R/upArm_R/lowArm_R/lowerRightArmCollider").GetComponent<CapsuleCollider>();
        upperLeftArmCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/spine2/clavicle_L/upArm_L/upperLeftArmCollider").GetComponent<CapsuleCollider>();
        lowerLeftArmCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/spine2/clavicle_L/upArm_L/lowArm_L/lowerLeftArmCollider").GetComponent<CapsuleCollider>();
        upperRightLegCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/masterControl/hipsCtrl/root/upLeg_R/upperLegRightCollider").GetComponent<CapsuleCollider>();
        lowerRightLegCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/masterControl/hipsCtrl/root/upLeg_R/lowLeg_R/lowerLegRightCollider").GetComponent<CapsuleCollider>();
        upperLeftLegCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/masterControl/hipsCtrl/root/upLeg_L/upperLegLeftCollider").GetComponent<CapsuleCollider>();
        lowerLeftLegCollider = GameObject.Find(gameObject.transform.name + "Graphics/PlayerModel/Armature/masterControl/hipsCtrl/root/upLeg_L/lowLeg_L/lowerLegLeftCollider").GetComponent<CapsuleCollider>();

        headCollider.enabled = false;
        upperBodyCollider.enabled = false;
        lowerbodyCollider.enabled = false;
        upperRightArmCollider.enabled = false;
        lowerRightArmCollider.enabled = false;
        upperLeftArmCollider.enabled = false;
        lowerLeftArmCollider.enabled = false;
        upperRightLegCollider.enabled = false;
        lowerRightLegCollider.enabled = false;
        upperLeftLegCollider.enabled = false;
        lowerLeftLegCollider.enabled = false;


    }

    void Update()
    {
        if (speed == 0)
        {
            setSpeed();
        }
        /*
        if (Time.time > testTime)
        {
            testTime += testInterval;
            Debug.Log(animator.GetCurrentAnimatorClipInfo(0));
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0));
        }
        */

        //Debug.Log(isGrounded);
        
        //MOVEMENT
        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxis("Horizontal");//between -1 and  1
        float _zMov = Input.GetAxis("Vertical");
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;
        // Final movement vector
        if (sprinting)
            speed = runSpeed;
        else
            speed = walkSpeed;
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Apply movement
        if (swimming)
        {
            motor.Move(_velocity);
        }

        #region Attack Animator output

        if (Input.GetKeyDown("q"))//overhead swing
        {
            animator.SetBool("Overhead", true);
        }
        if (Input.GetKeyUp("q"))//overhead swing
        {
            animator.SetBool("Overhead", false);
        }
        if (Input.GetKeyDown("e"))//overhead swing
        {
            animator.SetBool("Pierce", true);
        }
        if (Input.GetKeyUp("e"))//overhead swing
        {
            animator.SetBool("Pierce", false);
        }
        //ATTACK control======================
        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("Swing", true);
        }
        else
            animator.SetBool("Swing", false);
        #endregion

        #region Movement Animator output
        animator.SetBool("Grounded", isGrounded);
        //WALK control======================
        /*    WHAT WAS HERE BEFORE
         animator.SetFloat("Walk", _zMov);
         animator.SetFloat("Turn", _xMov);
        */

        //Walk Forward
        if (_zMov > 0 && walkF == false)
        {
            animator.SetBool("WalkF", true);
            walkF = true;
        }
        else if (!(_zMov > 0) && walkF == true)
        {
            animator.SetBool("WalkF", false);
            walkF = false;
        }
        //Walk Backward
        if (_zMov < 0 && walkB == false)
        {
            animator.SetBool("WalkB", true);
            walkB = true;
        }
        else if (!(_zMov < 0) && walkB == true)
        {
            animator.SetBool("WalkB", false);
            walkB = false;
        }
        //Turn Right
        if (_xMov > 0 && turnR == false)
        {
            animator.SetBool("TurnR", true);
            turnR = true;
        }
        else if (!(_xMov > 0) && turnR == true)
        {
            animator.SetBool("TurnR", false);
            turnR = false;
        }
        //Turn Left
        if (_xMov < 0 && turnL == false)
        {
            animator.SetBool("TurnL", true);
            turnL = true;
        }
        else if (!(_xMov < 0) && turnL == true)
        {
            animator.SetBool("TurnL", false);
            turnL = false;
        }


        //SPRINT control==================================================

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("Sprint", true);
            sprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Sprint", false);
            sprinting = false;
        }

        /*
        if (Input.GetKeyDown(KeyCode.LeftShift) && (_xMov == 0) && (_zMov > 0))
        {
            animator.SetBool("Sprint", true);
            sprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Sprint", false);
            sprinting = false;
        }
        */
        //BLOCK control==================================================
        if (Input.GetMouseButtonDown(1))
            animator.SetBool("Block", true); 
        else if (Input.GetMouseButtonUp(1))
            animator.SetBool("Block", false);
        #endregion

        #region Character movement
        //ROTATION LEFT AND RIGHT
        //Calculate rotation as a 3d vector (turning around)===========================
        float _yRot = Input.GetAxisRaw("Mouse X"); //moving mouse left to right
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
        //Apply rotation
        if (swimming)
        {
            motor.Rotate(_rotation);
        }


        //ROTATION UP AND DOWN
        //Calculate CAMERA rotation as a 3d vector (turning around)====================
        float _xRot = Input.GetAxisRaw("Mouse Y"); //moving mouse up and down?
        float _cameraRotationX = _xRot * lookSensitivity;
        //Apply camera rotation
        if (swimming)
        {
            motor.RotateCamera(_cameraRotationX);
        }

        //THRUSTER FORCE
        //Calculate the thrustforce based on player input
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButtonDown("Jump") && (controller.isGrounded || swimming))
        {
            motor.Jump();
            animator.SetBool("Jump", true);
        }
        else if (controller.isGrounded)
            animator.SetBool("Jump", false);

        if (controller.isGrounded)
            animator.SetBool("Grounded", true);
        else
            animator.SetBool("Grounded", false);

        #endregion
    }


    public void mouseOn()
    {
        fpsController.turnMouseOn();
    }
    public void mouseOff()
    {
        Debug.Log("2. fpscontroller is " + fpsController);
        fpsController.turnMouseOff();
    }

    /*
    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce };
    }
    */

    public void isSwimming()
    {
        swimming = true;
        //fpsController.enabled = false;

    }
    public void notSwimming()
    {
        swimming = false;
        //fpsController.enabled = true;
    }

    public void findUI()
    {
        
        /*=======================================================================
                           FINDING THE UI TO SEND INFO=============
        =======================================================================*/
        GameObject[] objectArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> objectList = new List<GameObject>(); for (int i = 0; i < objectArray.Length; i++)
        {
            if (objectArray[i].layer == 5) { objectList.Add(objectArray[i]); }
        }
        objectArray = objectList.ToArray();
        localPlayerUI = objectArray[1];
        for (int i = 1; i < objectArray.Length; i++)
        {
            //Debug.Log(objectArray[i].transform.name);
            if (objectArray[i].transform.name.Contains("Player"))
            {
                localPlayerUI = objectArray[i];
            }
        }
        //Debug.Log("UI is " + localPlayerUI);
        uiController = localPlayerUI.GetComponent<UiController>();
        /*
        =========================================================================
        */
    }

    public void sendVitals(float inputBlood, float inputStamina, float inputMana, float inputSanity)
    {
        uiController.sendVitalStatus(inputBlood, inputStamina, inputMana, inputSanity);
    }

    public void setSpeed()//float newSpeed)
    {
        weaponAttackSpeed = weaponParent.GetComponentInChildren<weaponStats>().getSpeed();
    }

    public void sendAttackInfo(attackStart currentAttack, float swingTime, string attackType)
    {
        Debug.Log(weaponAttackSpeed);
        Debug.Log(currentAttack);
        thisAttack = currentAttack;
        thisAttack.setSpeed(weaponAttackSpeed); //THIS SHOULD BE GOTTEN FROM THE WEAPON OBJECT IF POSSIBLE
        // the UIController must send setSpeed() to this, to make a new speed
    }

    public void swingStart( )
    {
        Debug.Log("swing start");
        weaponCollider.enabled = true;
        
    }

    public void swingEnd()
    {
        Debug.Log("swing end");
        weaponCollider.enabled = false;
    }

    public void hitWall()
    {
        animator.CrossFade("S Idle", 0.1f, 1);
        //thisAttack.stopAttack();
        Debug.Log("HITWALL, animation should crossfade to idle in 0.1 seconds");
        //fpsController.hitWall();
        attackMot.hitWall();
    }
    public float hitEnemy()
    {
        Debug.Log(" ");
        //THE DAMAGE SHOULD BE FOUND HERE AND RETURNED TO BE SENT
    
        return 10f;
    }

    public void equipUnarmed()
    {
        Debug.Log("FIX THIS. instantiate unarmed object if nothing is equipped at runtime.");
    }

    public void debugTest()
    {
        Debug.Log("Debug test runs");
    }

    public Camera findCamera()
    {
        return mainCam;
    }
}
 