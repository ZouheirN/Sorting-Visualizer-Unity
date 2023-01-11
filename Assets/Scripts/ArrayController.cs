using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ArrayController : MonoBehaviour
{
    public int[] array;
    public GameObject pillarPrefab;

    private int i;
    private int j;
    bool running;

    [Header("Colors")]
    public Material tempColor;
    public Material nextColor;
    public Material checkColor;

    Dropdown dropdown;
    ReadInput readInput;

    private void Awake() {
        readInput = GetComponent<ReadInput>();
        dropdown = GameObject.Find("List").GetComponent<Dropdown>();
    }

    public void GenerateArray() {
        running = false;
        i = 0;
        j = 1;

        this.array = new int[readInput.GetInput()];

        DestroyWithTag("pillar");


        for (int i = 0; i < this.array.Length; i++) {
            float denom = (float)i/this.array.Length;
            this.array[i] = UnityEngine.Random.Range(0, 60);
            var newObj = GameObject.Instantiate(pillarPrefab);
            newObj.transform.parent = GameObject.Find("Bar").transform;
            /*newObj.transform.localPosition = new Vector3(-0.5f + denom, 0.5f,0);
            newObj.transform.localScale = new Vector3((float)1/this.array.Length, this.array[i] % 60, 1);*/
            newObj.GetComponent<Pillar>().value = array[i];
            newObj.GetComponent<Pillar>().offset = denom;
            newObj.GetComponent<Pillar>().size = array.Length;
        }

        
    }

    public bool IsSorted(int[] a) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        for (int i = 0; i < a.Length - 1; i++) {
            pillars[i].GetComponent<Pillar>().Color = checkColor;
            pillars[i+1].GetComponent<Pillar>().Color = checkColor;
            if (a[i] > a[i + 1]) {
                return false; // It is proven that the array is not sorted.
            }
        }

        return true; // If this part has been reached, the array must be sorted.
    }

    public void OnSortClick() {
        if (dropdown.SortSelector() == 0 && !IsSorted(this.array)) {
            StartCoroutine(BubbleSort(0.001f));
        }
    }

    void DestroyWithTag(string destroyTag) {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }

    IEnumerator BubbleSort(float time) {

        // Set the function as running
        running = true;

        // Do the job until running is set to false
        while (running) {
            // Do your code
            OneBubbleSort(this.array);

            if (IsSorted(this.array))
                running = false;

            // wait for seconds
            yield return new WaitForSeconds(time);
        }
    }



    public int[] OneBubbleSort(int[] array) {

        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        //int length = pillars[0].GetComponentInChildren<Pillar>().size;
        int length = array.Length;

        int temp = array[0];
        //int t = pillars[0].GetComponentInChildren<Pillar>().value;

        if (i < length) {
           if (j < length) {
                if (array[i] > array[j]) {
                //if (pillars[i].GetComponentInChildren<Pillar>().value > pillars[j].GetComponentInChildren<Pillar>().value) { 

                    temp = array[i];
                    //t = pillars[i].GetComponentInChildren<Pillar>().value;
                    pillars[i].GetComponentInChildren<MeshRenderer>().material = tempColor;

                    array[i] = array[j];
                    //pillars[i].GetComponentInChildren<Pillar>().value = pillars[j].GetComponentInChildren<Pillar>().value;
                    pillars[j].GetComponentInChildren<MeshRenderer>().material = nextColor;
                    DisplayArray(array);

                    array[j] = temp;
                    //pillars[j].GetComponentInChildren<Pillar>().value = t;
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

    public void DisplayArray(int[] array) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        for (int i = 0; i < array.Length; i++) {
            pillars[i].GetComponent<Pillar>().value = array[i];
            //pillars[i].transform.localScale = new Vector3(pillars[i].transform.localScale.x, array[i]%60, 1);
        }

    }
}
