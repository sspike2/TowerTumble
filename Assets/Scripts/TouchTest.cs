using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent( typeof( Image ) )]
public class TouchTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    public Transform target;


    Transform cameraTransform;
    float initpos,endpos;
    public float xSpeed;


    float x = 0.0f;
    float y = 0.0f;


    public Quaternion rotation { get; private set; }

    void Start()
    {
        cameraTransform = Camera.main.transform;

        Vector3 angles = cameraTransform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rotation = Quaternion.Euler( y, x, 0 );


    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        initpos = eventData.position.x;

    }
    public void OnDrag(PointerEventData data)
    {
        //x = ;


        // data.

        // data.position.x;
        //x = data.position.x * xSpeed * 0.02f;

        x += (initpos - data.position.x) * xSpeed * 0.02f;
        // y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        // y = ClampAngle(y, yMinLimit, yMaxLimit);

        rotation = Quaternion.Euler( y, x, 0 );


        cameraTransform.transform.rotation = rotation;

    }

    void Update()
    {


        var distance = new Vector3( 0f, 0f, -65f );
        var position = rotation * distance + target.position;
        cameraTransform.transform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {


    }

   
}


