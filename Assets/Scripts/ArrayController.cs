using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArrayController : MonoBehaviour
{
    public int[] array;
    public GameObject pillarPrefab;
    public Button sortButton;

    private int i;
    private int j;
    bool running;

    [Header("Colors")]
    public Material tempColor;
    public Material nextColor;
    public Material checkColor;

    Dropdown dropdown;
    ReadInput readSizeInput;
    ReadSpeedInput readSpeedInput;
    

    private void Awake() {
        readSizeInput = GetComponent<ReadInput>();
        readSpeedInput = GetComponent<ReadSpeedInput>();
        dropdown = GameObject.Find("List").GetComponent<Dropdown>();
        sortButton.interactable = false;

    }

    public void GenerateArray() {
        sortButton.interactable = true;
        sortButton.GetComponentInChildren<Text>().text = "Sort Array";

        running = false;

        this.array = new int[readSizeInput.GetInput()];

        DestroyWithTag("pillar");


        for (int i = 0; i < this.array.Length; i++) {
            float denom = (float)i/this.array.Length;
            this.array[i] = UnityEngine.Random.Range(0, 60);
            var newObj = GameObject.Instantiate(pillarPrefab);
            newObj.transform.parent = GameObject.Find("Bar").transform;
            /*newObj.transform.localPosition = new Vector3(-0.5f + denom, 0.5f,0);
            newObj.transform.localScale = new Vector3((float)1/this.array.Length, this.array[i] % 60, 1);*/
            //newObj.GetComponent<Pillar>().value = array[i];
            newObj.GetComponent<Pillar>().offset = denom;
            newObj.GetComponent<Pillar>().size = array.Length;
            newObj.GetComponent<Pillar>().pos = i;
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
            i = 0;
            j = 1;
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            for (int i = 0; i < readSpeedInput.GetInput(); i++) {
                StartCoroutine(BubbleSort(0.001f));
            }
        } else if (dropdown.SortSelector() == 1 && !IsSorted(this.array)) {
            i = 0;
            j = 1;
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            for (int i = 0; i < readSpeedInput.GetInput(); i++) {
                StartCoroutine(SelectionSort(0.001f));
            }
        } else if (dropdown.SortSelector() == 2 && !IsSorted(this.array)) {
            i = 1;
            j = 0;
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            for (int i = 0; i < readSpeedInput.GetInput(); i++) {
                StartCoroutine(InsertionSort(0.001f));
            }
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

            if (IsSorted(this.array)) {
                running = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            }

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
                    //DisplayArray(array);

                    array[j] = temp;
                    //pillars[j].GetComponentInChildren<Pillar>().value = t;
                    //DisplayArray(array);
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

    /*    public void DisplayArray(int[] array) {
            List<GameObject> pillars = new List<GameObject>();
            foreach (Transform tran in GameObject.Find("Bar").transform) {
                pillars.Add(tran.gameObject);
            }

            for (int i = 0; i < array.Length; i++) {
                pillars[i].GetComponent<Pillar>().value = array[i];
                //pillars[i].transform.localScale = new Vector3(pillars[i].transform.localScale.x, array[i]%60, 1);
            }

        }*/

    IEnumerator SelectionSort(float time) {
        // Set the function as running
        running = true;

        // Do the job until running is set to false
        while (running) {
            // Do your code
            OneSelectionSort(this.array);

            if (IsSorted(this.array)) {
                running = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            }

            // wait for seconds
            yield return new WaitForSeconds(time);
        }
    }

    public int[] OneSelectionSort(int[] array) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        var arrayLength = array.Length;
        //for (int i = 0; i < arrayLength - 1; i++) {
        if (i < arrayLength - 1) { 
            var smallestVal = i;
            //for (int j = i + 1; j < arrayLength; j++) {
            if (j < arrayLength) { 
                if (array[j] < array[smallestVal]) {
                    smallestVal = j;
                }
                j++;
            }
            var tempVar = array[smallestVal];
            pillars[smallestVal].GetComponentInChildren<MeshRenderer>().material = tempColor;

            array[smallestVal] = array[i];
            pillars[i].GetComponentInChildren<MeshRenderer>().material = nextColor;

            array[i] = tempVar;
        }

        if (!(j < arrayLength)) {
            i++;
            j = i + 1;
        }


        return array;
    }

    IEnumerator InsertionSort(float time) {
        // Set the function as running
        running = true;

        // Do the job until running is set to false
        while (running) {
            // Do your code
            OneInsertionSort(this.array);

            if (IsSorted(this.array)) {
                running = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            }

            // wait for seconds
            yield return new WaitForSeconds(time);
        }
    }

    int flag;
    int val;
    public int[] OneInsertionSort(int[] arr) {
        

/*        //for (i = 1; i < arr.Length; i++) {
        if (i < arr.Length) {
            val = arr[i];
            flag = 0;
            //for (j = i - 1; j >= 0 && flag != 1;) {
            if (j >= 0 && flag != 1) {
                if (val < arr[j]) {
                    arr[j + 1] = arr[j];
                    j--;
                    arr[j + 1] = val;
                } else flag = 1;
            }
        }*/

        for (i = 1; i < arr.Length; i++) {
            val = arr[i];
            flag = 0;
            for (j = i - 1; j >= 0 && flag != 1;) {
                if (val < arr[j]) {
                    arr[j + 1] = arr[j];
                    j--;
                    arr[j + 1] = val;
                } else flag = 1;
                break;
            }
            break;
        }

        if (!(j >= 0 && flag != 1)) {
            i++;
            j = i - 1;
        } else {
            
        }

        return arr;
    }

}
