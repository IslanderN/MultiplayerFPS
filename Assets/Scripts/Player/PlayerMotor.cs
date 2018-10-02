using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCamreaRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;
    public float CameraRotationLimit
    {
        get { return cameraRotationLimit; }
        set
        {
            if (value > 85f || value < 0f)
            {
                cameraRotationLimit = 85f;
            }
            else
            {
                cameraRotationLimit = value;
            }
        }
    }

    //private float camera_X_RotationLimit = 0f;
    //public float Camer_X_RotationLimit
    //{
    //    get { return camera_X_RotationLimit; }
    //    set
    //    {
    //        if (value > 360f || value < 0f)
    //        {
    //            camera_X_RotationLimit = 0f;
    //        }
    //        else
    //        {
    //            camera_X_RotationLimit = value;
    //        }

    //    }
    //}


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Gets a rotational vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // Gets a rotational vector for the camera
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    //Get a force vector for our thrusters
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    // Perform movement based velocity  
    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    private void PerformRotation()
    {

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            currentCamreaRotationX -= cameraRotationX;
            currentCamreaRotationX = Mathf.Clamp(currentCamreaRotationX, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCamreaRotationX, 0f, 0f);
        }
    }



    public void SetActive(bool active)
    {
        this.enabled = active;
    }

}
