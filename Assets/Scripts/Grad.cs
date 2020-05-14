using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]a6
public class Grad : MonoBehaviour
{
    public Color topColor, botColor;

  
    void Start()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        var uv = mesh.uv;
        var colors = new Color[uv.Length];

        // Instead if vertex.y we use uv.x
        for (var i = 0; i < uv.Length; i++)
            colors[i] = Color.Lerp(topColor, botColor, uv[i].x);

        mesh.colors = colors;
    }
}
