using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{

    //public float Yammount;

    //public float duration;

    //public float overShootAmmount;
    //public Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        //Scale();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Scale(float ammount, float duration, float overShootAmmount)
    {
        Tween t = transform.DOScaleZ( ammount, duration );
        t.SetEase( Ease.OutElastic );
        t.easeOvershootOrAmplitude = overShootAmmount;
        //t.
    }
}

#if UNITY_EDITOR
[CustomEditor( typeof( DotweenTest ) )]
public class DotweenTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var cass = FindObjectOfType<MainClass>();
        DotweenTest t = (DotweenTest)target;
        if (GUILayout.Button( "Scale" )) t.Scale( cass.blockScaleAmmount, cass.blockScaleDuration, cass.blockScaleOvershoot );
    }
}
#endif
