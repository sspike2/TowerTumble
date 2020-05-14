using UnityEngine;

public class FillScreen : MonoBehaviour
{

    Camera cam;
    [SerializeField] float dist;
    void Start()
    {
        cam = Camera.main;

    }
    void Update()
    {

        //float pos = (cam.nearClipPlane + dist);

        //transform.position = cam.transform.position + cam.transform.forward * pos;
        //transform.LookAt(cam.transform);
        //float h = (Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2f) * cam.aspect / 10.0f;
        //float w = h * Screen.height / Screen.width;
        //transform.localScale = new Vector3(h, w, 1); ;


        float distance = Vector3.Distance(cam.transform.position, transform.position);
        float height = 2.0f * Mathf.Tan(0.5f * cam.fieldOfView * Mathf.Deg2Rad) * distance;
        float width = height * Screen.width / Screen.height;
        transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        transform.localScale = new Vector3(width / 7f, 1.0f, height / 7f);


    }
}