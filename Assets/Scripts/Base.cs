using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    // Use this for initialization

    public bool placedBuilding;

    public Vector3 postion;
    void Start()
    {
        postion = transform.position + new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }



}
