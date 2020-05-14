using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class Screenshot : MonoBehaviour
{
    //public RawImage image;
    public RenderTexture rt;
    Color transparent = new Color( 0, 0, 0, 0 );

    public Material mat;
    public Texture2D tex;

    Camera screenshotCam, mainCam;
    void Start()
    {
        screenshotCam = GetComponent<Camera>();
        mainCam = Camera.main;
        //ScreenCapture.CaptureScreenshotAsTexture();
        //rt = new RenderTexture( 512, 256, 32 );
        //rt = GetComponent<Camera>().targetTexture;
        //image.texture = rt;
        //RenderPipeline.BeginCameraRendering( GetComponent<Camera>() );
        //RenderPipeline.beginCameraRendering += OnBeginCameraRender;
    }
    void OnBeginCameraRender(Camera cam)
    {
        Graphics.SetRenderTarget( rt );
        RenderTexture.active = rt;
        GL.Clear( true, true, transparent );

        Graphics.SetRenderTarget( null );
        RenderTexture.active = null;
    }

    public void TakeScreenShot()
    {
        //StartCoroutine( testc() );
        IEnumerator testc()
        {


            var tex1 = new Texture2D( 2, 2 );
            yield return new WaitForEndOfFrame();

            mainCam.enabled = false;
            screenshotCam.enabled = true;
            tex1 = ScreenCapture.CaptureScreenshotAsTexture();
            mainCam.enabled = true;
            screenshotCam.enabled = false;

            //tex.Apply();
            //mat.SetTexture( "_MainTex", tex1 );
            //Graphics.CopyTexture( tex1, 0, 0, 1, 1, tex.width, tex.height, tex, 0, 0, 512, 512 );

            yield return new WaitForSeconds( .1f );
            var b = tex1.EncodeToPNG();
            yield return new WaitForSeconds( 1f );
            File.WriteAllBytes( Application.persistentDataPath + "/" + "LastProgress.png", b );

        }
    }
}



#if UNITY_EDITOR
[CustomEditor( typeof( Screenshot ) )]
public class ScreenShotEditor : Editor
{
    Screenshot myTarget;

    void OnEnable()
    {
        myTarget = (Screenshot)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button( "Capture" ))
        {
            myTarget.TakeScreenShot();
        }
    }
}
#endif
