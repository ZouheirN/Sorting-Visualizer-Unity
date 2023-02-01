using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _sliderText;

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            if (v < 10)
                _sliderText.text = "x" + (v / 10).ToString("0.0");
            else
                _sliderText.text = "x" + (v % 10 + 1).ToString("0.0");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
