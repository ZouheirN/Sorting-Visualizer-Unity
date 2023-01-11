using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSpeedInput : MonoBehaviour
{
    private string input;
    private int output;

    GameObject warningText;

    private void Awake() {
        warningText = GameObject.FindGameObjectWithTag("warningtext").gameObject;
        warningText.SetActive(false);
    }

    public void ReadSpeedInputFunc(string s) {
        input = s;
        Debug.Log(Convert.ToInt32(input));
        output = Convert.ToInt32(s);
        if (output > 200) {
            warningText.SetActive(true);
        } else {
            warningText.SetActive(false);
        }
    }

    public int GetInput() {
        return output;
    }
}
