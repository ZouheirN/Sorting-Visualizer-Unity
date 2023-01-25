using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSpeedInput : MonoBehaviour
{
    private string input;
    private double output;

    GameObject warningText;

    private void Awake() {
        warningText = GameObject.FindGameObjectWithTag("warningtext").gameObject;
        warningText.SetActive(false);
    }

    public void ReadSpeedInputFunc(string s) {
        input = s;
        Debug.Log(Convert.ToDouble(input));
        output = Convert.ToDouble(s);
        if (output > 10) {
            warningText.SetActive(true);
        } else {
            warningText.SetActive(false);
        }
    }

    public double GetInput() {
        return output;
    }
}
