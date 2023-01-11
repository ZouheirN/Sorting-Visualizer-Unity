using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    //public Text output;
    public TMPro.TMP_Dropdown myDrop;

    /*public void SortingSelector() {
        if (myDrop.value == 0) output.text = "b";
        else if (myDrop.value == 1) output.text = "s";
        else if (myDrop.value == 2) output.text = "i";
    }*/

    public int SortSelector() {
        return myDrop.value;
    }
}
