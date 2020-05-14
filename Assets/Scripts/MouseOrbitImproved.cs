using UnityEngine;
using System.Collections;

[AddComponentMenu( "Camera-Control/Mouse Orbit with zoom" )]
public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rbody;

    bool test;
    float x = 0.0f;
    float y = 0.0f;

    Transform cameraTransform;

    MainClass mainClass;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        cameraTransform = Camera.main.transform;
        rbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rbody != null)
        {
            rbody.freezeRotation = true;
        }
        mainClass = FindObjectOfType<MainClass>();

    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler( y, x, 0 );
        Vector3 negDistance = new Vector3( 0.0f, 0.0f, -distance );
        Vector3 position = rotation * negDistance + target.position;

        target.transform.rotation = rotation;
        cameraTransform.transform.position = position;

        if (mainClass.shouldPlace) return;
        //#if UNITY_EDITOR
        if (Input.GetMouseButton( 0 ))
        {
            x += Input.GetAxis( "Mouse X" ) * xSpeed * distance * 0.02f;
            // y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            // y = ClampAngle(y, yMinLimit, yMaxLimit);

            rotation = Quaternion.Euler( y, x, 0 );

            distance = Mathf.Clamp( distance - Input.GetAxis( "Mouse ScrollWheel" ) * 5, distanceMin, distanceMax );

            RaycastHit hit;
            if (Physics.Linecast( target.position, cameraTransform.transform.position, out hit ))
            {
                distance -= hit.distance;
            }
            negDistance = new Vector3( 0.0f, 0.0f, -distance );
            position = rotation * negDistance + target.position;

            cameraTransform.transform.rotation = rotation;
            cameraTransform.transform.position = position;
        }
        //# else
        if (Input.touchCount >= 1)
        {

            {
                x += Input.GetTouch( 0 ).position.x * xSpeed  * 0.02f;
                // y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                // y = ClampAngle(y, yMinLimit, yMaxLimit);

                rotation = Quaternion.Euler( y, x, 0 );

                distance = Mathf.Clamp( distance - Input.GetAxis( "Mouse ScrollWheel" ) * 5, distanceMin, distanceMax );

                RaycastHit hit;
                if (Physics.Linecast( target.position, cameraTransform.transform.position, out hit ))
                {
                    distance -= hit.distance;
                }
                negDistance = new Vector3( 0.0f, 0.0f, -distance );
                position = rotation * negDistance + target.position;

                cameraTransform.transform.rotation = rotation;
                cameraTransform.transform.position = position;
            }
        }
        //#endif
        //else
        //{
        //    x = 0;
        //}


    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp( angle, min, max );
    }
}