using UnityEngine;
using System.Collections;

public class camerascript : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    [SerializeField]
    Transform Parent;
    // Rigidbody rb;
    [SerializeField]
    float rotateSpeed;

    float xDelta;
    float lastPos;
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        // Parent = GameObject.Find("Grid 4_4").transform;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            var delta = Input.mousePosition.x - lastPos;


            // Do Stuff here
            Debug.Log("delta  : " + delta);

            if (delta != 0)
            {
                Parent.transform.RotateAround(Parent.transform.position, Parent.transform.up, rotateSpeed * -delta * Time.deltaTime);
            }

            // Debug.Log("delta Y : " + delta.y);
            // End do stuff


            lastPos = Input.mousePosition.x;
        }
    }
}