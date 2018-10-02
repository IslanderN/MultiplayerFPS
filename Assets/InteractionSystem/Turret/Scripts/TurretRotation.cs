using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    //[SerializeField]
    //private Transform pivotY;
    //[SerializeField]
    //private Transform pivotX;

    [SerializeField]
    private float xLimitRotation = 45;
    [SerializeField]
    private float yLimitRotation = 45;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject bodyOfTurret;
    [SerializeField]
    private GameObject headOfTurret;
    //SerializeField]
    //private Transform firePoint;

    [SerializeField]
    private float lookSensativity = 3;

    //[SerializeField]
    //private float speed = 5;

    private float currentHeadRotation = 0f;

    void Start()
    {
        this.enabled = false;
    }


    void Update()
    {


        var limitRotation = bodyOfTurret.transform.rotation;

        //Debug.Log(limitRotation.y * 100 + " " + limitRotation.z * 100);

        float yRot = Input.GetAxis("Mouse X");
        float xRot = Input.GetAxis("Mouse Y");


        if (limitRotation.y * 100 > yLimitRotation && yRot > 0)
        {
            yRot = 0;
        }
        else if (limitRotation.y * 100 < -yLimitRotation && yRot < 0)
        {
            yRot = 0;
        }

        //if (limitRotation.z * 100 > xLimitRotation && xRot > 0)
        //{
        //    xRot = 0;
        //}
        //else if (limitRotation.z * 100 < -xLimitRotation && xRot < 0)
        //{
        //    xRot = 0;
        //}
        float headRot = xRot * lookSensativity;

        Vector3 yVector = new Vector3(0f, yRot, 0f) * lookSensativity;
        Vector3 xVector = new Vector3(0f, 0f, xRot) * lookSensativity;

        //Vector3 pivot = pivotX.position + pivotY.position;

        //bodyOfTurret.transform.position += (transform.rotation * pivot);
        //bodyOfTurret.transform.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, yVector);

        //bodyOfTurret.transform.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, xVector);

        //bodyOfTurret.transform.position -= (transform.rotation * pivot);


        Vector3 togetherVector = new Vector3(0f, yRot, xRot) * lookSensativity;

        Rigidbody rb = bodyOfTurret.GetComponent<Rigidbody>();
        //Rigidbody headRb = headOfTurret.GetComponent<Rigidbody>();
        if (!rb)
        {
            Debug.LogError("Body of Turret doesn't have Rigidbody.");
        }
        //if (!headRb)
        //{
        //    Debug.LogError("Head of Turret doesn't have Rigidbody.");
        //}

        rb.MoveRotation(rb.rotation * Quaternion.Euler(yVector));
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(xVector));

        currentHeadRotation += headRot;
        currentHeadRotation = Mathf.Clamp(currentHeadRotation, -xLimitRotation, xLimitRotation);


        headOfTurret.transform.localEulerAngles = new Vector3(0f, 0f, currentHeadRotation);

        //headRb.MoveRotation(rb.rotation * Quaternion.Euler(xVector));



        //bodyOfTurret.transform.RotateAround(pivotY.position, yVector, Mathf.Clamp(Mathf.Abs(yRot) * speed * Time.deltaTime, -yLimitRotation, yLimitRotation));
        // bodyOfTurret.transform.RotateAround(pivotX.position, xVector, Mathf.Clamp(Mathf.Abs(xRot) * speed * Time.deltaTime, -xLimitRotation, xLimitRotation));
        // bodyOfTurret.transform.rotation = new Quaternion(0f, bodyOfTurret.transform.rotation.y, bodyOfTurret.transform.rotation.z, bodyOfTurret.transform.rotation.w);



        //bodyOfTurret.transform.RotateAround(pivotY.position, togetherVector, Mathf.Abs(yRot) * speed * Time.deltaTime);

        //Vector3 pivot = pivotX.position + pivotY.position;

        //Vector3 movePoint = RotatePointAroundPivot(firePoint.position, pivotY.position, yVector);
        //bodyOfTurret.transform.position = Vector3.Lerp(firePoint.position, movePoint, Time.deltaTime * speed);
    }

    //private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    //{
    //    Vector3 dir = point - pivot; // get point direction relative to pivot
    //    dir = Quaternion.Euler(angles) * dir; // rotate it
    //    point = dir + pivot; // calculate rotated point
    //    return point; // return it
    //}

    //public void Rot()
    //{
    //    transform.position += (transform.rotation * Pivot);

    //    if (RotateX)
    //        transform.rotation *= Quaternion.AngleAxis(45 * Time.deltaTime, Vector3.right);
    //    if (RotateY)
    //        transform.rotation *= Quaternion.AngleAxis(45 * Time.deltaTime, Vector3.up);
    //    if (RotateZ)
    //        transform.rotation *= Quaternion.AngleAxis(45 * Time.deltaTime, Vector3.forward);

    //    transform.position -= (transform.rotation * Pivot);
    //}

}
