using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGrid : MonoBehaviour
{
    MainClass mainclass;

    bool buttonsOn;
    public GameObject Grid_Buttons;

    public GameObject LeftButton, RightButton, BotButton, TopButton;

    public GameObject grid;

    public int gridLength;
    // Use this for initialization
    void Start()
    {
        mainclass = FindObjectOfType<MainClass>();
    }

    public void setGrid(GameObject gridtopass, int length)
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (mainclass.placing_Grid)
        {
            if (!buttonsOn)
            {
                buttonsOn = true;
                Grid_Buttons.SetActive(true);
            }
        }
        else
        {

            if (buttonsOn)
            {
                buttonsOn = false;
                Grid_Buttons.SetActive(false);
            }

        }
    }



    public void left()
    {
        grid.transform.position += new Vector3(0, 0, 3);
        RightButton.SetActive(true);
        Debug.Log("called");
        if (grid.transform.position.z >= 3)
        {
            LeftButton.SetActive(false);
        }

    }

    public void right()
    {
        grid.transform.position -= new Vector3(0, 0, 3);
        LeftButton.SetActive(true);
        if (grid.transform.position.z <= -9)
        {
            RightButton.SetActive(false);
        }
    }

    public void top()
    {
        grid.transform.position += new Vector3(3, 0, 0);
        BotButton.SetActive(true);
        if (grid.transform.position.x >= 3)
        {
            TopButton.SetActive(false);
        }

    }
    public void bot()
    {
        grid.transform.position -= new Vector3(3, 0, 0);
        TopButton.SetActive(true);
        if (grid.transform.position.x <= -9)
        {
            BotButton.SetActive(false);
        }
    }
}