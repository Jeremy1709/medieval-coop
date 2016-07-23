using UnityEngine;
using System.Collections;

public class attackMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocityY = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    //Get a y movement vector
    public void MoveY(Vector3 _velocityY)
    {
        velocityY = _velocityY;
    }
    // Gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    // Gets rotational vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // Gets rotational vector for CAMERA
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    //Gets a force vector for our thrusters.
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    // Run every physics iteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Performs a jump
    public void Jump()
    {
        rb.AddForce(0, 450, 0);
        Debug.Log("JUMP");
    }
    //Perform movement based on velocity veriable
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);//Takes position we want to move to, easier then AddForce method
        }
        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
    //Perform rotation
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            // Set our rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Applt our rotation to the transform of our camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

    public void hitWall()
    {
        Debug.Log("attack motor hitwall runs");
    }
}


