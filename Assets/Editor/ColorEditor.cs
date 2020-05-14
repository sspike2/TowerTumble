using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ColorAssign))]
public class ColorEditor : Editor
{

    ColorAssign mytarget;
    void OnEnable()
    {
        mytarget = (ColorAssign)target;

    }
    public override void OnInspectorGUI()
    {



        base.OnInspectorGUI();
        if (GUILayout.Button("setColor value"))
        {
            mytarget.setColor(mytarget.id);
        }
        if (GUILayout.Button("setColor Random"))
        {
            mytarget.setColor();
        }


        //id = EditorGUILayout.IntField("Value to set on Button", id);
    }





    void OnValidate()
    {
        mytarget.setColor(mytarget.id);
    }
}
