using UnityEngine;
using System.Collections;

public class attackStart : StateMachineBehaviour {
    private PlayerController player;
    [SerializeField] private float swingStart;//Right when swing is started for animation
    [SerializeField]
    private float swingEnd; //Right when swinging motion ends(the closer to the end point it hits, the more damage)

    private bool startSent;
    private bool endSent;
    [SerializeField] private string attackType = "slashLeft";

    [SerializeField] attackStart thisScript;

    private AnimatorStateInfo currentInfo;
    private Animator currentAnimator;

    //always find out time between swings, to compare it to a Time.time ran when swing starts.

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {


        getSwingInfo(stateInfo);
        player = animator.GetComponentInParent<PlayerController>();
        startSent = false;
        endSent = false;
        currentInfo = stateInfo;
        currentAnimator = animator;
        float swingTime = swingEnd - swingStart;
        player.sendAttackInfo(thisScript, swingTime, attackType);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(stateInfo.normalizedTime);
        if (stateInfo.normalizedTime > swingStart && startSent == false)
        {            
            player.swingStart( );
            startSent = true;
            /*
            I need to maybe, when this starts, contact the player controller, which somehow links the current script to the playercontroller
            so it can send a message back to the script when the sword has to stop swinging.
        Maybe the playercontroller should have an 'attackStart currentAttack' script, 
        that is replaced by a new thing every time.  
    */
        }
        if(stateInfo.normalizedTime > swingEnd && endSent == false)
        {
            player.swingEnd();
            endSent = true;
        }
    }

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        currentAnimator.speed = 1;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    public void setSpeed(float weaponAttackSpeed)
    {
        currentAnimator.speed = weaponAttackSpeed;
    }

    public void stopAttack()
    {
        currentAnimator.speed = 20;
        Debug.Log("animation has been stopped");
    }

    public void getSwingInfo(AnimatorStateInfo stateInfo)
    {     
        swingEnd = 0.708f;
        switch (stateInfo.shortNameHash)
        {
            case 1213846862: swingStart = 0.5f; attackType = "slashLeft"; break;
            case -1302927315: swingStart = 0.5f; attackType = "slashRight"; break;
            case -1054926425: swingStart = 0.54f; attackType = "pierce"; break;
            case 713549666: swingStart = 0.54f; attackType = "overLeft"; break;
            case -796337663: swingStart = 0.54f; attackType = "overRight"; break;
            default: break;
        }       
    }
}
