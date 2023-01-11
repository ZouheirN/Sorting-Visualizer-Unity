using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ArrayController : MonoBehaviour
{
    public int[] array;
    public GameObject pillarPrefab;

    private int i;
    private int j;

    Dropdown dropdown;
    ReadInput readInput;

    private void Awake() {
        readInput = GetComponent<ReadInput>();
        dropdown = GameObject.Find("List").GetComponent<Dropdown>();
    }

    public void GenerateArray() {
        i = 0;
        j = 1;

        this.array = new int[readInput.GetInput()];

        DestroyWithTag("pillar");

        /*float[] range = { -0.5f };

        if (this.array.Length != 1) {
            range = linspace(-0.5f, 0.5f, this.array.Length);
        }*/


        for (int i = 0; i < this.array.Length; i++) {
            float denom = (float)i/this.array.Length;
            this.array[i] = UnityEngine.Random.Range(0, 60);
            var newObj = GameObject.Instantiate(pillarPrefab);
            newObj.transform.parent = GameObject.Find("Bar").transform;
            newObj.transform.localPosition = new Vector3(-0.5f + denom, 0.5f,0);
            newObj.transform.localScale = new Vector3((float)1/this.array.Length, this.array[i] % 60, 1);
        }
    }

    IEnumerator WaitForSpecificSeconds(float seconds) {
        array = OneBubbleSort(this.array);
        yield return new WaitForSeconds(seconds);
    }

    public bool IsSorted(int[] array) {
        for (int i = 1; i < array.Length; i++) {
            if (array[i-1] > array[i]) {
                return false;
            }
        }
        return true;
    }

    public void OnSortClick() {
        if (dropdown.SortSelector() == 0) {
            while(!IsSorted(this.array)) {
                StartCoroutine(WaitForSpecificSeconds(0.2f));
            }
            
        }
    }

    void DestroyWithTag(string destroyTag) {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }

    /*public static float[] linspace(float startval, float endval, int steps) {
        float interval = (endval / MathF.Abs(endval)) * MathF.Abs(endval - startval) / (steps - 1);
        return (from val in Enumerable.Range(0, steps)
                select startval + (val * interval)).ToArray();
    }*/
    public int[] OneBubbleSort(int[] array) {

        int length = array.Length;

        int temp = array[0];

        if (i < length) {
           if (j < length) {
                if (array[i] > array[j]) {
                    temp = array[i];

                    array[i] = array[j];

                    array[j] = temp;

                    DisplayArray(array);
                }
                j++;
           }
        }

        if (!(j < length)) {
            i++;
            j = i + 1;
        }


        return array;
    }

    public int[] BubbleSort(int[] array) {

        /*List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }*/

        int length = array.Length;

        int temp = array[0];
        //Vector3 pillarsTemp = pillars[0].transform.localPosition;

        for (int i = 0; i < length; i++) {
            for (int j = i + 1; j < length; j++) {
                if (array[i] > array[j]) {
                    temp = array[i];
                    //pillarsTemp = pillars[i].transform.localPosition;

                    array[i] = array[j];
                    //pillars[i].transform.localPosition = pillars[j].transform.localPosition;

                    array[j] = temp;
                    //pillars[j].transform.localPosition = pillarsTemp;

                    //another idea: display pillars after every step instead of swapping them
                    DisplayArray(array);
                }

            }
        }

        return array;
    }

    public void DisplayArray(int[] array) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        for (int i = 0; i < array.Length; i++) {
            pillars[i].transform.localScale = new Vector3(pillars[i].transform.localScale.x, array[i]%60, 1);
        }

    }



    /*    public void swap(int pos1, int pos2) {
            List<GameObject> pillars = new List<GameObject>();
            foreach (Transform tran in GameObject.Find("Bar").transform) {
                pillars.Add(tran.gameObject);
            }

            Vector3 pillarsTemp = pillars[0].transform.localPosition;
            Vector3 pillarsTempScale = pillars[0].transform.localScale;

            pillarsTemp = pillars[pos1].transform.localPosition;
            pillarsTempScale.y = pillars[pos1].transform.localScale.y;

            pillars[pos1].transform.localPosition = pillars[pos2].transform.localPosition;
            pillars[pos1].transform.localScale.y = pillars[pos2].transform.localScale.y;

            pillars[pos2].transform.localPosition = pillarsTemp;
            pillars[pos2].transform.localScale.y = pillarsTempScale.y;
        }*/
}
