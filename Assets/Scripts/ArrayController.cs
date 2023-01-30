using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
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
                return false;
            }
        }

        return true;
    }

    public void OnSortClick() {
        string sliderText = sliderTextUI.text;
        sliderText = sliderText.Replace("%", string.Empty);
        float sliderValue = (float)Convert.ToDouble(sliderText);

        if (dropdown.SortSelector() == 0) {
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            //for (int i = 0; i < readSpeedInput.GetInput(); i++) {
            //StartCoroutine(BubbleSort(this.array, 0.01f / (float)readSpeedInput.GetInput()));
            StartCoroutine(BubbleSort(this.array, 0.01f / sliderValue));
            //}
        } else if (dropdown.SortSelector() == 1) {
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            //for (int i = 0; i < readSpeedInput.GetInput(); i++) {
            //StartCoroutine(SelectionSort(this.array, 0.01f / (float)readSpeedInput.GetInput()));
            StartCoroutine(SelectionSort(this.array, 0.01f / sliderValue));
            //}
        } else if (dropdown.SortSelector() == 2) {
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            //StartCoroutine(InsertionSort(this.array, 0.01f / (float)readSpeedInput.GetInput()));
            StartCoroutine(InsertionSort(this.array, 0.01f / sliderValue));
        } else if (dropdown.SortSelector() == 3) {
            sortButton.interactable = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorting...";
            StartCoroutine(BottomUpMergeSort(this.array, 0.01f / sliderValue));
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
                        //yield return new WaitForSeconds(time);
                        int temp = arr[in_var];

                        //yield return new WaitForSeconds(time);
                        arr[in_var] = arr[in_var + 1];

                        //yield return new WaitForSeconds(time);
                        arr[in_var + 1] = temp;

                        pillars[in_var + 1].GetComponent<Pillar>().Color = tempColor;
                        pillars[in_var].GetComponent<Pillar>().Color = whiteColor;
                        //yield return new WaitForSeconds(time);

                    } else {
                        if (in_var - 1 >= 0)
                            pillars[in_var - 1].GetComponent<Pillar>().Color = whiteColor;
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

                //yield return new WaitForSeconds(time);
                for (in_var = out_var + 1; in_var < arr.Length; in_var++) {
                    yield return new WaitForSeconds(time);

                    pillars[out_var].GetComponent<Pillar>().Color = nextColor;
                    if (out_var - 1 >= 0) {
                        pillars[out_var - 1].GetComponent<Pillar>().Color = checkColor;
                    }

                    pillars[in_var].GetComponent<Pillar>().Color = tempColor;
                    if (in_var - 1 >= 0 && pillars[in_var - 1].GetComponent<Pillar>().Color != swapColor) {
                        pillars[in_var - 1].GetComponent<Pillar>().Color = whiteColor;
                    }

                    //yield return new WaitForSeconds(time);
                    if (arr[in_var] < arr[min]) {
                        pillars[min].GetComponent<Pillar>().Color = whiteColor;
                        pillars[in_var].GetComponent<Pillar>().Color = swapColor;
                        //yield return new WaitForSeconds(time);
                        min = in_var;
                    }

                }

                for (int i = out_var; i < arr.Length; i++) {
                    pillars[i].GetComponent<Pillar>().Color = whiteColor;
                }

                int temp = arr[out_var];
                //yield return new WaitForSeconds(time);
                arr[out_var] = arr[min];
                //yield return new WaitForSeconds(time);
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

                //yield return new WaitForSeconds(time);
                in_var = out_var;
                //yield return new WaitForSeconds(time);

                while (in_var > 0 && arr[in_var - 1] >= temp) {
                    yield return new WaitForSeconds(time);
                    arr[in_var] = arr[in_var - 1];

                    pillars[in_var].GetComponent<Pillar>().Color = tempColor;
                    //yield return new WaitForSeconds(time);

                    if (in_var + 1 < arr.Length) {
                        pillars[in_var + 1].GetComponent<Pillar>().Color = checkColor;
                        //yield return new WaitForSeconds(time);
                    }

                    if (out_var < arr.Length - 1) {
                        pillars[out_var + 1].GetComponent<Pillar>().Color = nextColor;
                        //yield return new WaitForSeconds(time);
                    }

                    --in_var;
                }
                //yield return new WaitForSeconds(time);
                arr[in_var] = temp;

                for (int i = 0; i < out_var; i++) 
                    pillars[i].GetComponent<Pillar>().Color = checkColor;
            
            }

            if (IsSorted(this.array)) {
                running = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            }
        }
    }

    IEnumerator Wait(float time) { yield return new WaitForSeconds(time); }

    IEnumerator BottomUpMergeSort(int[] a, float time) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int[] temp = new int[a.Length];
        for (int runWidth = 1; runWidth < a.Length; runWidth = 2 * runWidth) {
            yield return new WaitForSeconds(time);
            for (int i = 0; i < pillars.Count; i++) {
                pillars[i].GetComponent<Pillar>().Color = whiteColor;
            }
            for (int eachRunStart = 0; eachRunStart < a.Length; eachRunStart = eachRunStart + 2 * runWidth) {
                yield return new WaitForSeconds(time);
                int start = eachRunStart;
                int mid = eachRunStart + (runWidth - 1);

                pillars[start].GetComponent<Pillar>().Color = nextColor;


                if (mid >= a.Length) {
                    mid = a.Length - 1;
                }
                int end = eachRunStart + ((2 * runWidth) - 1);
                if (end >= a.Length) {
                    end = a.Length - 1;
                }

                pillars[mid].GetComponent<Pillar>().Color = tempColor;

                this.Merge(a, start, mid, end, temp, time);
            }
            for (int i = 0; i < a.Length; i++) {
                yield return new WaitForSeconds(time);
                a[i] = temp[i];
                if (pillars[i].GetComponent<Pillar>().Color == tempColor) {
                    for (int j = 0; j < i; j++) {
                        pillars[j].GetComponent<Pillar>().Color = whiteColor;
                    }
                }
                pillars[i].GetComponent<Pillar>().Color = swapColor;
            }
        }

        if (IsSorted(this.array)) {
            running = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorted!";
        }
    }

    void Merge(int[] a, int start, int mid, int end, int[] temp, float time) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int i = start, j = mid + 1, k = start;
        while ((i <= mid) && (j <= end)) {
            //StartCoroutine(Wait(time));
            if (a[i] <= a[j]) {
                temp[k] = a[i];
                i++;
            } else {
                temp[k] = a[j];
                j++;
            }

            k++;

        }
        while (i <= mid) {
            //StartCoroutine(Wait(time));
            temp[k] = a[i];
            i++;
            k++;
        }
        while (j <= end) {
            //StartCoroutine(Wait(time));
            temp[k] = a[j];
            j++;
            k++;
        }

        if (k >= 0 && k < pillars.Count) {
            pillars[k].GetComponent<Pillar>().Color = swapColor;
        }


        Assert.IsTrue(k == end + 1);
        Assert.IsTrue(i == mid + 1);
        Assert.IsTrue(j == end + 1);
    }

} 
