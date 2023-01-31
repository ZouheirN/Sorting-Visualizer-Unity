using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    private string input;
    private int output;

    public Button generateArrayButton;

    private void Awake() {
        generateArrayButton.interactable = false;
    }

    public void ReadSizeInput(string s) {
        input = s;
        Debug.Log(Convert.ToInt32(input));
        output = Convert.ToInt32(s);
        if (output <= 0)
            generateArrayButton.interactable = false;
        else
            generateArrayButton.interactable = true;
    }

    public int GetInput() {
        return output;
    }
}
