using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrayController : MonoBehaviour {
    public int[] array;
    public GameObject pillarPrefab;
    public Button sortButton;
    public Text sliderTextUI;

    bool running;

    [Header("Colors")]
    public Material tempColor;
    public Material nextColor;
    public Material checkColor;
    public Material swapColor;
    public Material whiteColor;

    Dropdown dropdown;
    ReadInput readSizeInput;

    private void Awake() {
        readSizeInput = GetComponent<ReadInput>();
        dropdown = GameObject.Find("List").GetComponent<Dropdown>();
        sortButton.interactable = false;
    }

    public void GenerateArray() {
        sortButton.interactable = true;
        sortButton.GetComponentInChildren<Text>().text = "Sort Array";
        
        StopAllCoroutines();
        running = false; 

        this.array = new int[readSizeInput.GetInput()];

        DestroyWithTag("pillar");


        for (int i = 0; i < this.array.Length; i++) {
            float denom = (float)i / this.array.Length;
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
            pillars[i + 1].GetComponent<Pillar>().Color = checkColor;
            if (a[i] > a[i + 1]) {
                return false; // It is proven that the array is not sorted.
            }
        }

        return true; // If this part has been reached, the array must be sorted.
    }

    public void OnSortClick() {
        string sliderText = sliderTextUI.text;
        sliderText = sliderText.Replace("%", string.Empty);
        float sliderValue = (float)Convert.ToDouble(sliderText);

        if (dropdown.SortSelector() == 0) { //&& !IsSorted(this.array)) {
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            //for (int i = 0; i < readSpeedInput.GetInput(); i++) {
            //StartCoroutine(BubbleSort(this.array, 0.01f / (float)readSpeedInput.GetInput()));
            StartCoroutine(BubbleSort(this.array, 0.01f / sliderValue));
            //}
        } else if (dropdown.SortSelector() == 1) { //&& !IsSorted(this.array)) {
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            //for (int i = 0; i < readSpeedInput.GetInput(); i++) {
            //StartCoroutine(SelectionSort(this.array, 0.01f / (float)readSpeedInput.GetInput()));
            StartCoroutine(SelectionSort(this.array, 0.01f / sliderValue));
            //}
        } else if (dropdown.SortSelector() == 2) { // && !IsSorted(this.array)) {
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            //StartCoroutine(InsertionSort(this.array, 0.01f / (float)readSpeedInput.GetInput()));
            StartCoroutine(InsertionSort(this.array, 0.01f / sliderValue));
        }
    }

    void DestroyWithTag(string destroyTag) {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }

    IEnumerator BubbleSort(int[] arr, float time) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        running = true;

        while (running) {
            int out_var, in_var;

            for (out_var = arr.Length - 1; out_var > 0; out_var--) {
                yield return new WaitForSeconds(time);
                for (in_var = 0; in_var < out_var; in_var++) {
                    yield return new WaitForSeconds(time);

                    pillars[in_var].GetComponent<Pillar>().Color = nextColor;

                    if (arr[in_var] > arr[in_var + 1]) {
                        yield return new WaitForSeconds(time);
                        int temp = arr[in_var];
                        //pillars[in_var].GetComponent<Pillar>().Color = tempColor;
                        

                        yield return new WaitForSeconds(time);
                        arr[in_var] = arr[in_var + 1];
                        pillars[in_var].GetComponent<Pillar>().Color = swapColor;
                        //pillars[in_var].GetComponent<Pillar>().Color = swapColor;


                        yield return new WaitForSeconds(time);
                        arr[in_var + 1] = temp;

                        pillars[in_var + 1].GetComponent<Pillar>().Color = tempColor;
                        pillars[in_var].GetComponent<Pillar>().Color = whiteColor;
                    }

                    if (out_var - 1 == in_var)
                        pillars[out_var].GetComponent<Pillar>().Color = checkColor;
                }

                for (int i = 0; i < out_var; i++) {
                    pillars[i].GetComponent<Pillar>().Color = whiteColor;
                }
            }

            if (IsSorted(this.array)) {
                running = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            }
        }
    }

    IEnumerator SelectionSort(int[] arr, float time) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        running = true;

        while (running) {
            int out_var, in_var, min;

            for (out_var = 0; out_var < arr.Length-1; out_var++) {
                yield return new WaitForSeconds(time);
                min = out_var;

                yield return new WaitForSeconds(time);
                for (in_var = out_var + 1; in_var < arr.Length; in_var++) {

                    pillars[out_var].GetComponent<Pillar>().Color = nextColor;
                    if (out_var - 1 >= 0) {
                        pillars[out_var - 1].GetComponent<Pillar>().Color = checkColor;
                    }

                    pillars[in_var].GetComponent<Pillar>().Color = tempColor;
                    if (in_var - 1 >= 0 && pillars[in_var - 1].GetComponent<Pillar>().Color != swapColor) {
                        pillars[in_var - 1].GetComponent<Pillar>().Color = whiteColor;
                    }

                    yield return new WaitForSeconds(time);
                    if (arr[in_var] < arr[min]) {
                        pillars[min].GetComponent<Pillar>().Color = whiteColor;
                        pillars[in_var].GetComponent<Pillar>().Color = swapColor;
                        yield return new WaitForSeconds(time);
                        min = in_var;
                    }

                }

                for (int i = out_var; i < arr.Length; i++) {
                    pillars[i].GetComponent<Pillar>().Color = whiteColor;
                }

                int temp = arr[out_var];
                yield return new WaitForSeconds(time);
                arr[out_var] = arr[min];
                yield return new WaitForSeconds(time);
                arr[min] = temp;
            }

            if (IsSorted(this.array)) {
                running = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            }
        }
    }

    IEnumerator InsertionSort(int[] arr, float time) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        running = true;

        while (running) {
            int in_var, out_var;

            for (out_var = 0; out_var < arr.Length; out_var++) {
                yield return new WaitForSeconds(time);
                int temp = arr[out_var];
                pillars[out_var].GetComponent<Pillar>().Color = nextColor;

                yield return new WaitForSeconds(time);
                in_var = out_var;
                yield return new WaitForSeconds(time);

                while (in_var > 0 && arr[in_var - 1] >= temp) {
                    yield return new WaitForSeconds(time);
                    arr[in_var] = arr[in_var - 1];

                    pillars[in_var].GetComponent<Pillar>().Color = tempColor;
                    yield return new WaitForSeconds(time);

                    if (in_var + 1 < arr.Length) {
                        pillars[in_var + 1].GetComponent<Pillar>().Color = checkColor;
                        yield return new WaitForSeconds(time);
                    }

                    if (out_var < arr.Length - 1) {
                        pillars[out_var + 1].GetComponent<Pillar>().Color = nextColor;
                        yield return new WaitForSeconds(time);
                    }

                    --in_var;
                }
                yield return new WaitForSeconds(time);
                arr[in_var] = temp;

                if (IsSorted(this.array)) {
                    running = false;
                    sortButton.GetComponentInChildren<Text>().text = "Sorted!";
                }
            }
        }
    }

} 

