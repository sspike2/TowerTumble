using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLimit : MonoBehaviour
{

    // [SerializeField]
    public float TopLimit, BotLimit, LeftLimit, RightLimit;

    public GameObject TopArrow, BotArrow, LeftArrow, RightArrow;

    public Transform CameraTarget;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //if (transform.eulerAngles.y < -45 || transform.eulerAngles.y > 45)
        //{
        //    UIScript.instance.GameOver();
        //}
    }
}