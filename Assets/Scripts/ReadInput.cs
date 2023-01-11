using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    private string input;
    private int output;

    public void ReadSizeInput(string s) {
        input = s;
        Debug.Log(Convert.ToInt32(input));
        output = Convert.ToInt32(s);
    }

    public int GetInput() {
        return output;
    }
}
