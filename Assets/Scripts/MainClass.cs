// using System;
using BayatGames.SaveGameFree;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class MainClass : MonoBehaviour
{
    Camera cam;
    RaycastHit BlockRay, GridRay;
    public GameObject hologram;

    public GameObject lastGrid;

    Base b;

    [HideInInspector] public bool shouldPlace;
    Material hologrammaterial;

    GameObject nextCubeToPlace;

    Rigidbody nextCubeToPlaceRB;

    int GridBoxes;

    int currentGridSize;

    int GridNumber;
    public bool placing_Grid;

    public LayerMask layermask;


    [SerializeField]

    float NextGridHeight;

    Vector3 NextGridPos;


    GameObject NextGrid;

    GridLimit NextGridLimit;


    Vector3 far = new Vector3(300, 300, 300);

    bool gridFalling;

    Rigidbody GridRb;

    GameObject CameraTarget;
    Camera MainCam;

    bool ConfirmHologram;


    // Bool 2_2 condition
    bool Grid2_2, HasInstantiated;

    // GameObject[] Blocks2_2 = new GameObject[4];

    List<GameObject> Blocks2_2 = new List<GameObject>();
    int gridIndex;

    public Button NextGridButton;


    LineRenderer[] Rays;


    GameObject SessionObjects;

    private int score, HighScore;
    private int sessionID;
    public int LatestScore;
    public static MainClass instance;

    int blocksPlaced, floorAmmount;

    GameObject TopArrowCurrent, BotArrowCurrent, LeftArrowCurrent, RightArrowCurrent;

    [Header("DOTWEEN")]
    public float blockScaleDuration;
    public float blockScaleAmmount, blockScaleOvershoot;
    private bool isDead;




    //[SerializeField] Vector3 cameraTargetDeathOffset;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        hologrammaterial = hologram.GetComponent<MeshRenderer>().sharedMaterial;
        ResetCube();
        GridNumber = 0;
        currentGridSize = 16;
        GridBoxes = 0;
        CameraTarget = GameObject.Find("CameraTarget");
        MainCam = Camera.main;
        SessionObjects = new GameObject("Session_objs");


        //HighScore = PlayerPrefs.GetInt( StaticVars.HighScorePref, 0 );

        HighScore = SaveGame.Load<int>("HighScore");
        sessionID = PlayerPrefs.GetInt("sessionID", 0);


        // Rays = new LineRenderer[9];
        // for (int i = 0; i < 9; i++)
        // {
        //     Rays[i] = (Instantiate(Resources.Load("Line"), new Vector3(-3000, -3000, 3000), Quaternion.identity, SessionObjects.transform) as GameObject).GetComponent<LineRenderer>();
        // }
        // NextGridPos = new Vector3(6.5f, 5, 5.5f);

    }

    // Update is called once per frame
    void Update()
    {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // block placement 
        if (!placing_Grid)
        {

            // if (Input.GetMouseButtonDown(0))
            // {

            if (Physics.Raycast(ray, out BlockRay, Mathf.Infinity, layermask))
            {
                if (BlockRay.collider)
                {
                    shouldPlace = true;
                    // Debug.Log(hit.transform.name);
                    if (BlockRay.transform.tag == "Base")
                    {
                        shouldPlace = true;
                        b = BlockRay.transform.GetComponent<Base>();
                    }
                    else
                    {
                        shouldPlace = false;
                        // BlockRay = null;
                    }
                }
                else
                {
                    shouldPlace = false;
                }
            }
            else
            {
                shouldPlace = false;
            }

            // Debug.Log(shouldplace);
            // Debug.Log(BlockRay.collider);

            // }

            // if raycast hit on block plcement
            if (shouldPlace)
            {
                if (!b.placedBuilding)
                {

                    if (Input.GetMouseButtonDown(0))
                    {

                        // if (!ConfirmHologram)
                        // {
                        //     hologram.transform.position = b.transform.position + new Vector3(0, 2);
                        //     ConfirmHologram = true;
                        // }
                        // else
                        // {
                        // Debug.Log(hologram.transform.position + " " + b.transform.position);
                        // if(b == )

                        if ((int)hologram.transform.position.x == (int)b.transform.position.x && (int)hologram.transform.position.z == (int)b.transform.position.z)
                        {

                            b.placedBuilding = true;
                            // Instantiate(Resources.Load("Cube_L (1)"), b.postion, Quaternion.identity);
                            nextCubeToPlace.transform.position = b.transform.position + new Vector3(0, .1f);
                            nextCubeToPlaceRB.useGravity = true;
                            nextCubeToPlaceRB.isKinematic = false;
                            nextCubeToPlace.GetComponentInChildren<DotweenTest>().Scale
                                (blockScaleAmmount, blockScaleDuration, blockScaleOvershoot);

                            ////gravity increased
                            //Physics.gravity = new Vector3( 0, -10, 0 );

                            SoundManagerScript.PlaySound("block_impact");

                            score++;
                            blocksPlaced++;
                            UIScript.instance.ScoreTextsUpdate(score);


                            hologram.transform.position = new Vector3(300, 300, 300);
                            ResetCube();
                            GridBoxes++;


                            if (!NextGridButton.interactable)
                            {
                                if ((currentGridSize == 4 && GridBoxes >= 2) || (currentGridSize == 9 && GridBoxes >= 3))
                                {
                                    NextGridButton.interactable = true;
                                }
                            }
                            if (GridBoxes >= currentGridSize)
                            {

                                NewGrid();
                            }
                            ConfirmHologram = false;
                        }
                        else
                        {
                            hologram.transform.position = b.transform.position + new Vector3(0, 1.6f);
                        }
                    }
                }
            }
        }
        else
        {
            if (!gridFalling)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out GridRay, Mathf.Infinity))
                    {
                        if (GridRay.transform)
                        {
                            if (GridRay.transform.CompareTag("Arrow"))
                            {
                                if (GridRay.transform.gameObject == TopArrowCurrent)
                                {
                                    TopArrow();
                                }
                                else if (GridRay.transform.gameObject == BotArrowCurrent)
                                {
                                    BotArrow();
                                }
                                else if (GridRay.transform.gameObject == LeftArrowCurrent)
                                {
                                    LeftArrow();
                                }
                                else if (GridRay.transform.gameObject == RightArrowCurrent)
                                {
                                    RightArrow();
                                }
                            }
                            else if (GridRay.transform.CompareTag("Grid"))
                            {
                                DropGrid();
                            }

                        }
                    }
                }

#if UNITY_EDITOR

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    LeftArrow();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    RightArrow();
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    TopArrow();
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    BotArrow();
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {

                    DropGrid();
                }
#endif
            }
            else
            {
                if (GridRb.velocity.magnitude == 0)
                {
                    CancelInvoke("ResetBlock");
                    ResetBlock();
                }
            }

            /*
            new Grid Code
             */
        }
        // else
        // {
        //     hologram.transform.position = far;
        // }

    }

    void LeftArrow()
    {
        SoundManagerScript.PlaySound("click_sound");

        NextGrid.transform.localPosition += new Vector3(-4, 0, 0);
        UpdateRayPosition(NextGridLimit);
        if (NextGrid.transform.localPosition.x <= NextGridLimit.LeftLimit)
        {
            LeftArrowCurrent.SetActive(false);
        }
        if (!RightArrowCurrent.activeSelf)
        {
            RightArrowCurrent.SetActive(true);
        }
    }
    void RightArrow()
    {
        SoundManagerScript.PlaySound("click_sound");

        NextGrid.transform.localPosition += new Vector3(4, 0, 0);
        UpdateRayPosition(NextGridLimit);
        if (NextGrid.transform.localPosition.x >= NextGridLimit.RightLimit)
        {
            RightArrowCurrent.SetActive(false);
        }
        if (!LeftArrowCurrent.activeSelf)
        {
            LeftArrowCurrent.SetActive(true);
        }
    }
    void TopArrow()
    {

        SoundManagerScript.PlaySound("click_sound");

        NextGrid.transform.localPosition += new Vector3(0, 0, 4);
        UpdateRayPosition(NextGridLimit);
        if (NextGrid.transform.localPosition.z >= NextGridLimit.TopLimit)
        {
            TopArrowCurrent.SetActive(false);
        }
        if (!BotArrowCurrent.activeSelf)
        {
            BotArrowCurrent.SetActive(true);
        }
    }

    void BotArrow()
    {
        SoundManagerScript.PlaySound("click_sound");

        NextGrid.transform.localPosition += new Vector3(0, 0, -4);
        UpdateRayPosition(NextGridLimit);
        if (NextGrid.transform.localPosition.z <= NextGridLimit.BotLimit)
        {
            BotArrowCurrent.SetActive(false);
        }

        if (!TopArrowCurrent.activeSelf)
        {
            TopArrowCurrent.SetActive(true);
        }
    }
    void DropGrid()
    {
        GridRb = NextGrid.GetComponentInChildren<Rigidbody>();
        GridRb.isKinematic = false;
        GridRb.useGravity = true;
        gridFalling = true;

        LeftArrowCurrent.SetActive(false);
        RightArrowCurrent.SetActive(false);
        TopArrowCurrent.SetActive(false);
        BotArrowCurrent.SetActive(false);


        score += 10;
        floorAmmount++;
        UIScript.instance.ScoreTextsUpdate(score);


        lastGrid = NextGrid;
        var t = NextGrid.GetComponentsInChildren<Transform>();

        var target = new Vector3(CameraTarget.transform.position.x, CameraTarget.transform.position.y + (4f), CameraTarget.transform.position.z);
        CameraTarget.transform.DOMove(target, 1f).SetDelay(1.5f);

        for (int i = 0; i < t.Length; i++)
        {
            if (t[i].name.StartsWith("Holo"))
            {
                t[i].gameObject.SetActive(false);
            }
        }

        Invoke("ResetBlock", 2f);
    }

    void ResetBlock()
    {
        // var a = NextGrid.GetComponentsInChildren<Base>();

        // for (int i = 0; i < a.Length; i++)
        // {
        //     a[i].transform.parent = null;
        // }


        GridBoxes = 0;
        placing_Grid = false;
        gridFalling = false;
        //CameraTarget.transform.position += new Vector3( 0, 4 );
    }


    public void NewGrid()
    {

        placing_Grid = true;
        currentGridSize = 0;
        GridNumber++;


        Grid2_2 = false;
        HasInstantiated = false;

        NextGridButton.interactable = false;

        bool is3_3;

        int levelNo = GridNumber;


        switch (levelNo)
        {
            case 1:

                if (Random.Range(1, 11) >= 3) // 30 % chace for 2 x 2 
                {
                    is3_3 = true;
                }

                else
                {
                    is3_3 = false;
                }

                break;

            case 2:
                if (Random.Range(1, 11) >= 3) // 30 % chace for 2 x 2 
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;

            case 3:
                if (Random.Range(1, 11) >= 4)// 40 % chace for 2 x 2 
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;

            case 4:
                if (Random.Range(1, 11) >= 7) // 70 % chace for 2 x 2 
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;

            case 5:
                if (Random.Range(1, 11) >= 5)// 50 % chace for 2 x 2 
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;

            case 6:
                if (Random.Range(1, 11) >= 5) // 50 % chace for 2 x 2 
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;

            case 7:
                if (Random.Range(1, 11) >= 7)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;

            case 8:
                if (Random.Range(1, 11) >= 4)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;

            case 9:
                if (Random.Range(1, 11) >= 5)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;


            case 10:
                if (Random.Range(1, 11) >= 5)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;


            case 11:
                if (Random.Range(1, 11) >= 8)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;


            case 12:
                if (Random.Range(1, 11) >= 3)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;


            case 13:
                if (Random.Range(1, 11) >= 4)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;


            case 14:
                if (Random.Range(1, 11) >= 7)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;


            case 15:
                if (Random.Range(1, 11) >= 3)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }
                break;

            default:
                if (Random.Range(1, 11) >= 5)
                {
                    is3_3 = true;

                }
                else
                {
                    is3_3 = false;
                }

                break;

        }

        if (is3_3)
        {
            //if (NextGridPos.x == 0)
            //{
            NextGridPos = new Vector3(8, lastGrid.transform.position.y + NextGridHeight, -8);
            //}
            //else
            //{
            //    NextGridPos += new Vector3( 0, NextGridHeight );
            //}
            NextGrid = Instantiate(Resources.Load("Grid 3_3"), NextGridPos, Quaternion.identity) as GameObject;
            NextGridLimit = NextGrid.GetComponent<GridLimit>();
            TopArrowCurrent = NextGridLimit.TopArrow;
            BotArrowCurrent = NextGridLimit.BotArrow;
            LeftArrowCurrent = NextGridLimit.LeftArrow;
            RightArrowCurrent = NextGridLimit.RightArrow;
            currentGridSize = 9;


            // var cubes = NextGridLimit.GetComponentsInChildren<Transform>(true);
            // var j = 0;

            // for (int i = 0; i < cubes.Length; i++)
            // {
            //     if (cubes[i].name.StartsWith("Cube"))
            //     {
            //         Rays[j].transform.position = cubes[i].position;
            //         j++;
            //     }
            // }
            UpdateRayPosition(NextGridLimit);






        }
        else
        {

            //if (NextGridPos.x == 0)
            //{

            NextGridPos = new Vector3(4f, lastGrid.transform.position.y + NextGridHeight, 4f);
            //}
            //else
            //{
            //    NextGridPos += new Vector3( 0, NextGridHeight );
            //}

            Grid2_2 = true;
            NextGrid = Instantiate(Resources.Load("Grid 2_2"), NextGridPos, Quaternion.identity) as GameObject;
            NextGridLimit = NextGrid.GetComponent<GridLimit>();
            TopArrowCurrent = NextGridLimit.TopArrow;
            BotArrowCurrent = NextGridLimit.BotArrow;
            LeftArrowCurrent = NextGridLimit.LeftArrow;
            RightArrowCurrent = NextGridLimit.RightArrow;

            currentGridSize = 4;

            UpdateRayPosition(NextGridLimit);


        }
    }

    void UpdateRayPosition(GridLimit limit)
    {
        // var cubes = limit.GetComponentsInChildren<Transform>(true);
        // var j = 0;
        // for (int i = 0; i < cubes.Length; i++)
        // {
        //     if (cubes[i].name.StartsWith("Cube"))
        //     {
        //         Rays[j].transform.position = cubes[i].position;
        //         // Rays[j].transform.parent = cubes[i];
        //         j++;
        //     }
        // }

    }



    void ResetCube()
    {


        if (Grid2_2)
        {
            if (!HasInstantiated)
            {
                Blocks2_2.Clear();
                // Blocks2_2

                Blocks2_2.Add(Instantiate(Resources.Load("Cube_L (1)"), far, Quaternion.identity) as GameObject); //=  ;
                Blocks2_2.Add(Instantiate(Resources.Load("Cube_L (1)"), far, Quaternion.identity) as GameObject);

                var a = Random.Range(0, 2);

                if (a == 0)
                {
                    Blocks2_2.Add(Instantiate(Resources.Load("Cube_L (1)"), far, Quaternion.identity) as GameObject);

                }
                else
                {
                    Blocks2_2.Add(Instantiate(Resources.Load("Cube_M (1)"), far, Quaternion.identity) as GameObject);
                }

                a = Random.Range(0, 2);

                if (a == 0)
                {
                    Blocks2_2.Add(Instantiate(Resources.Load("Cube_L (1)"), far, Quaternion.identity) as GameObject);

                }
                else
                {
                    Blocks2_2.Add(Instantiate(Resources.Load("Cube_M (1)"), far, Quaternion.identity) as GameObject);
                }


                Blocks2_2.Shuffle();


                nextCubeToPlace = Blocks2_2[GridBoxes];
                nextCubeToPlaceRB = nextCubeToPlace.GetComponentInChildren<Rigidbody>();
                if (nextCubeToPlace.name.StartsWith("Cube_M (1)"))
                {
                    hologrammaterial.color = Color.grey;
                    hologram.transform.localScale = new Vector3(hologram.transform.localScale.x, 3, hologram.transform.localScale.z);
                }
                else
                {
                    hologrammaterial.color = Color.white;
                    hologram.transform.localScale = new Vector3(hologram.transform.localScale.x, 6, hologram.transform.localScale.z);
                }


                HasInstantiated = true;
            }
            else
            {
                nextCubeToPlace = Blocks2_2[GridBoxes];
                nextCubeToPlaceRB = nextCubeToPlace.GetComponentInChildren<Rigidbody>();
                if (nextCubeToPlace.name.StartsWith("Cube_M (1)"))
                {
                    hologrammaterial.color = Color.grey;
                    hologram.transform.localScale = new Vector3(hologram.transform.localScale.x, 3, hologram.transform.localScale.z);
                }
                else
                {
                    hologrammaterial.color = Color.white;
                    hologram.transform.localScale = new Vector3(hologram.transform.localScale.x, 6, hologram.transform.localScale.z);
                }

                // gridIndex++;
            }



        }
        else
        {
            var t = Random.Range(0, 2);
            if (t == 0)
            {
                nextCubeToPlace = Instantiate(Resources.Load("Cube_M (1)"), far, Quaternion.identity) as GameObject;
                nextCubeToPlaceRB = nextCubeToPlace.GetComponentInChildren<Rigidbody>();

                hologrammaterial.color = Color.grey;
                hologram.transform.localScale = new Vector3(hologram.transform.localScale.x, 3, hologram.transform.localScale.z);
            }
            else
            {
                nextCubeToPlace = Instantiate(Resources.Load("Cube_L (1)"), far, Quaternion.identity) as GameObject;
                nextCubeToPlaceRB = nextCubeToPlace.GetComponentInChildren<Rigidbody>();

                hologrammaterial.color = Color.white;
                hologram.transform.localScale = new Vector3(hologram.transform.localScale.x, 6, hologram.transform.localScale.z);
            }
        }

    }

    public void GameOver()
    {
        if (!isDead)
        {
            isDead = true;

            sessionID++;
            PlayerPrefs.SetInt("sessionID", sessionID);

            AnalyticsEvent.Custom("SessionComplete", new Dictionary<string, object>
          {
             { "Session Ammount", sessionID },
             { "Blocks Placed", blocksPlaced },
             { "Floors Completed", floorAmmount },
             {"Score", score }
         });



            UIScript.instance.AddToLeaderBoards(score);
            UIScript.instance.GameOver();

            //CameraTarget.transform.DOLocalMove( cameraTargetDeathOffset, 1f );
            var mainCam = Camera.main;

            DOTween.To(() => mainCam.fieldOfView, x => mainCam.fieldOfView = x, 56, 1f);

            if (score > HighScore)
            {
                UIScript.instance.HighScoreTextsUpdate(score);
                UIScript.instance.NewHighScore();
                SaveGame.Save<int>("HighScore", HighScore);

                AnalyticsEvent.Custom("HighScore", new Dictionary<string, object>{
             { "HighScore", score }
         });

            }
            else
            {
                UIScript.instance.HighScoreTextsUpdate(HighScore);
            }

        }
    }


}
