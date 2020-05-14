using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tempDoozyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // transform.DOScale(1.5, 0.5).SetEase<Ease.OutElastic>();
       //Tweener t =  transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .4f);

        //t.SetEase<Ease.OutBack>();


        //Tweener.setEase<t>(Ease.OutElastic);

        //.SetEase<Ease.OutElastic>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOScale(2.0f, 2.0f).SetDelay(1.5f).SetEase(Ease.OutElastic);
        //transform.DOShakeScale(2.0f, 2.0f).SetDelay(1.5f).SetEase(Ease.OutElastic);
    }

    //.OnComplete(HandleTweenCallback)
    //void HandleTweenCallback()
    //{
    //    Debug.Log("scaled");
    //}

}
