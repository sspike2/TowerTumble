using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderManager : MonoBehaviour
{

    [SerializeField] TouchTest mouseControl;

    [SerializeField] TextMeshProUGUI text;

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        // slider.onValueChanged += OnValueChanged;
        slider.onValueChanged.AddListener(delegate { OnValueChanged(); });

    }


    public void OnValueChanged()
    {
        mouseControl.xSpeed = slider.value;
        text.text = slider.value + "";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
