//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//public class ColorAssign : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        Renderer rend = GetComponent<Renderer>();
//        //blockMaterial.shader = Shader.Find("Custom_Fog");
//        blockMaterial.SetColor("_Color1_F", Color.red);
//        blockMaterial.SetColor("_Color1_B", Color.blue);
//        blockMaterial.SetColor("_Color1_L", Color.yellow);
//        blockMaterial.SetColor("_Color1_R", Color.black);
//        blockMaterial.SetColor("_Color1_T", Color.cyan);
//        blockMaterial.SetColor("_Color1_D", Color.magenta);
//    }
//    // Update is called once per frame
//    void Update()
//    {
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorAssign : MonoBehaviour
{
    public int index;


    public Material blockMaterial;
    public Material smallCube;
    public Material bigCube;
    public Material Cube_base;
    public Material skyBox;


    public Color smallBoxFront, smallBoxSide, smallBoxTop;

    public Color bigBoxFront, bigBoxSide, bigBoxTop;


    [SerializeField]
    public ColorAssignData[] colorCombinations;


    public int id;
    // Start is called before the first frame update
    void Start()
    {
        //Renderer rend = GetComponent<Renderer>();
        //blockMaterial.shader = Shader.Find("Custom_Fog");
        // blockMaterial.SetColor("_Color1_F", Color.red);
        // blockMaterial.SetColor("_Color1_B", Color.blue);
        // blockMaterial.SetColor("_Color1_L", Color.yellow);
        // blockMaterial.SetColor("_Color1_R", Color.black);
        // blockMaterial.SetColor("_Color1_T", Color.cyan);
        // blockMaterial.SetColor("_Color1_D", Color.magenta);


        setColor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setColor()
    {
        index = Random.Range(0, colorCombinations.Length);

        setColor(index);

    }

    public void setColor(int index)
    {


        // block material
        blockMaterial.SetColor("_Color1_F", colorCombinations[index].base_Color[0]);
        blockMaterial.SetColor("_Color1_B", colorCombinations[index].base_Color[0]);

        blockMaterial.SetColor("_Color1_L", colorCombinations[index].base_Color[1]);
        blockMaterial.SetColor("_Color1_R", colorCombinations[index].base_Color[1]);

        blockMaterial.SetColor("_Color1_T", colorCombinations[index].base_Color[2]);
        blockMaterial.SetColor("_Color1_D", colorCombinations[index].base_Color[2]);

        //small block
        //smallCube.SetColor( "_Color1_F", colorCombinations[index].small_Block_Color[0] );
        //smallCube.SetColor( "_Color1_B", colorCombinations[index].small_Block_Color[0] );

        //smallCube.SetColor( "_Color1_L", colorCombinations[index].small_Block_Color[1] );
        //smallCube.SetColor( "_Color1_R", colorCombinations[index].small_Block_Color[1] );

        //smallCube.SetColor( "_Color1_T", colorCombinations[index].small_Block_Color[2] );
        //smallCube.SetColor( "_Color1_D", colorCombinations[index].small_Block_Color[2] );

        smallCube.SetColor("_Color1_F", smallBoxFront);
        smallCube.SetColor("_Color1_B", smallBoxFront);

        smallCube.SetColor("_Color1_L", smallBoxSide);
        smallCube.SetColor("_Color1_R", smallBoxSide);

        smallCube.SetColor("_Color1_T", smallBoxTop);
        smallCube.SetColor("_Color1_D", smallBoxTop);


        //Big Block
        //bigCube.SetColor( "_Color1_F", colorCombinations[index].Big_Block_Color[0] );
        //bigCube.SetColor( "_Color1_B", colorCombinations[index].Big_Block_Color[0] );

        //bigCube.SetColor( "_Color1_L", colorCombinations[index].Big_Block_Color[1] );
        //bigCube.SetColor( "_Color1_R", colorCombinations[index].Big_Block_Color[1] );

        //bigCube.SetColor( "_Color1_T", colorCombinations[index].Big_Block_Color[2] );
        //bigCube.SetColor( "_Color1_D", colorCombinations[index].Big_Block_Color[2] );

        bigCube.SetColor("_Color1_F", bigBoxFront);
        bigCube.SetColor("_Color1_B", bigBoxFront);

        bigCube.SetColor("_Color1_L", bigBoxSide);
        bigCube.SetColor("_Color1_R", bigBoxSide);

        bigCube.SetColor("_Color1_T", bigBoxTop);
        bigCube.SetColor("_Color1_D", bigBoxTop);






        //Skybox


        skyBox.SetColor("_Color1", colorCombinations[index].SkyBox[0]);
        skyBox.SetColor("_Color2", colorCombinations[index].SkyBox[1]);


        Cube_base.SetColor("_Color", colorCombinations[index].Cube_base);



    }

//#if UNITY_EDITOR
//    [MenuItem("MyMenu/Set Color %g")]

//    public void Color()
//    {
//        setColor(id);
//    }

//#endif





}

[System.Serializable]
public class ColorAssignData
{
    public Color[] base_Color = new Color[3];

    //public Color[] small_Block_Color = new Color[3];

    //public Color[] Big_Block_Color = new Color[3];

    public Color[] SkyBox = new Color[2];

    public Color Cube_base;
}
