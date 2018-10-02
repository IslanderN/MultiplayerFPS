using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensativity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Thruster Fuel configuration:")]
    [SerializeField]
    private float thrusterFuelBurnSpeed = 1f;
    [SerializeField]
    private float thrusterFuelRegenSpeed = 0.3f;
    private float thrusterFuelAmount = 1f;

    public float GetThrusterFuelAmount()
    {
        return thrusterFuelAmount;
    }

    [SerializeField]
    private LayerMask environmentMask;

    [Header("Spring settings:")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    //Component caching
    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

    private bool blockedMovement = false;

    private bool blockedCursor = true;

    private bool blockedRotation = false;


    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();

        SetJointSettings(jointSpring);
    }

    void Update()
    {
        // if (PauseMenu.isOn)
        // {
        //     if (Cursor.lockState != CursorLockMode.None)
        //     {
        //         Cursor.lockState = CursorLockMode.None;

        //     }
        //     if (!Cursor.visible)
        //     {
        //         Cursor.visible = true;
        //     }

        //     motor.Move(Vector3.zero);
        //     motor.Rotate(Vector3.zero);
        //     motor.RotateCamera(0);

        //     return;
        // }

        if (blockedCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Setting target position for spring
        // This makes the physics act right when it comes to
        // apply graviry when flying over objects
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 100f, environmentMask))
        {
            joint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
        }
        else
        {
            joint.targetPosition = new Vector3(0f, 0f, 0f);
        }

        // Calculate movement velocity as a 3D vector
        float _xMove = Input.GetAxis("Horizontal");
        float _zMove = Input.GetAxis("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMove;
        Vector3 _moveVertical = transform.forward * _zMove;

        //Final movement vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical) * speed;

        //Animation movement
        animator.SetFloat("ForwardVelocity", _zMove);

        if (blockedMovement)
        {
            motor.Move(Vector3.zero);
        }
        else
        {
            //Apply movement
            motor.Move(_velocity);
        }

        //Calculate rotation as a 3D vector (turning around)
        float _yRot = 0;
        if (!blockedRotation)
            _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensativity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector (turning around)
        float _xRot = 0;
        if (!blockedRotation)
            _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensativity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotationX);



        //Calculate the thruster force based on player input
        Vector3 _thrusterForce = Vector3.zero;


        if (Input.GetButton("Jump") && thrusterFuelAmount > 0f && !blockedMovement)
        {
            thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;

            if (thrusterFuelAmount >= 0.01f)
            {
                _thrusterForce = Vector3.up * thrusterForce;
                SetJointSettings(0f);
            }
        }
        else
        {
            thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;

            SetJointSettings(jointSpring);
        }


        thrusterFuelAmount = Mathf.Clamp(thrusterFuelAmount, 0, 1);

        //Apply the thruster force
        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }

    /// pubic funcitons for GameEvents
    public void BlockMovement(bool block)
    {
        blockedMovement = block;
    }

    public void BlockCursor(bool block)
    {
        blockedCursor = block;
    }

    public void BlockRotation(bool block)
    {
        blockedRotation = block;
    }

    public void SetActive(bool active)
    {
        this.enabled = active;
    }
}
