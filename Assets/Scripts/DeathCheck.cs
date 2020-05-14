using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DeathCheck : MonoBehaviour
{

    //UIScript ui;

    // Start is called before the first frame update
    void Start()
    {
        //ui = FindObjectOfType<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        //if (!ui.DeathScreen.isVisible)
        //ui.GameOver();

        MainClass.instance.GameOver();
    }


    
}
