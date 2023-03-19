using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ArrayController : MonoBehaviour {
    public int[] array;
    public GameObject pillarPrefab;
    public Button sortButton;
    public Text sliderTextUI;

    public bool running;
    private float sliderValue;
    private float time;

    [Header("Colors")]
    public Material tempColor;
    public Material nextColor;
    public Material checkColor;
    public Material swapColor;
    public Material whiteColor;

    Dropdown dropdown;
    ReadInput readSizeInput;
    Timer timer;

    private void Awake() {
        readSizeInput = GetComponent<ReadInput>();
        dropdown = GameObject.Find("List").GetComponent<Dropdown>();
        sortButton.interactable = false;
        timer = GameObject.Find("TimerManager").GetComponent<Timer>();
    }

    private void Update() {
        string sliderText = sliderTextUI.text;
        List<char> charsToRemove = new List<char>() { 'x', '%' };
        sliderValue = (float)Convert.ToDouble(String.Concat(sliderText.Split(charsToRemove.ToArray())));
        time = 0.01f / sliderValue;
    }

    public void GenerateArray() {
        timer.timerText.color = new Color32(255, 255, 255, 255);
        timer.currentTime = 0;
        timer.timerText.text = "Time: 0.000";

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
        if (readSizeInput.GetInput() == 1) {
            List<GameObject> pillars = new List<GameObject>();
            foreach (Transform tran in GameObject.Find("Bar").transform) {
                pillars.Add(tran.gameObject);
            }
            pillars[0].GetComponent<Pillar>().Color = checkColor;
            sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            sortButton.interactable = false;
            timer.timerText.color = new Color32(27, 255, 0, 255);
            return;
        }

        timer.timerText.color = new Color32(255, 255, 255, 255);
        timer.currentTime = 0;
        running = true;

        switch (dropdown.SortSelector()) {
            case 0:
                sortButton.interactable = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorting...";
                StartCoroutine(BubbleSort(array));
                break;
            case 1:
                sortButton.interactable = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorting...";
                StartCoroutine(SelectionSort(array));
                break;
            case 2:
                sortButton.interactable = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorting...";
                StartCoroutine(InsertionSort(array));
                break;
            case 3:
                sortButton.interactable = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorting...";
                StartCoroutine(BottomUpMergeSort(array));
                break;
            case 4:
                sortButton.interactable = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorting...";
                StartCoroutine(ShellSort(array));
                break;
            case 5:
                sortButton.interactable = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorting...";
                StartCoroutine(QuickSort(array, 0, array.Length - 1));
                break;
            case 6:
                sortButton.interactable = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorting...";
                StartCoroutine(HeapSort(array));
                break;
            case 7:
                sortButton.interactable = false;
                sortButton.GetComponentInChildren<Text>().text = "Sorting...";
                StartCoroutine(RadixSort(array));
                break;
        }
    }

    void DestroyWithTag(string destroyTag) {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }

    IEnumerator BubbleSort(int[] arr) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int out_var, in_var;

        for (out_var = arr.Length - 1; out_var > 0; out_var--) {
            yield return new WaitForSeconds(time);
            for (in_var = 0; in_var < out_var; in_var++) {
                yield return new WaitForSeconds(time);

                pillars[in_var].GetComponent<Pillar>().Color = nextColor;

                if (arr[in_var] > arr[in_var + 1]) {
                    int temp = arr[in_var];

                    arr[in_var] = arr[in_var + 1];

                    arr[in_var + 1] = temp;

                    pillars[in_var + 1].GetComponent<Pillar>().Color = tempColor;
                    pillars[in_var].GetComponent<Pillar>().Color = whiteColor;

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
            timer.timerText.color = new Color32(27, 255, 0, 255);
        }

    }

    IEnumerator SelectionSort(int[] arr) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int out_var, in_var, min;

        for (out_var = 0; out_var < arr.Length - 1; out_var++) {
            yield return new WaitForSeconds(time);
            min = out_var;

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

                if (arr[in_var] < arr[min]) {
                    pillars[min].GetComponent<Pillar>().Color = whiteColor;
                    pillars[in_var].GetComponent<Pillar>().Color = swapColor;
                    min = in_var;
                }

            }

            for (int i = out_var; i < arr.Length; i++) {
                pillars[i].GetComponent<Pillar>().Color = whiteColor;
            }

            int temp = arr[out_var];
            arr[out_var] = arr[min];
            arr[min] = temp;
        }

        if (IsSorted(this.array)) {
            running = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            timer.timerText.color = new Color32(27, 255, 0, 255);
        }

    }

    IEnumerator InsertionSort(int[] arr) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int in_var, out_var;

        for (out_var = 0; out_var < arr.Length; out_var++) {
            yield return new WaitForSeconds(time);
            int temp = arr[out_var];
            pillars[out_var].GetComponent<Pillar>().Color = nextColor;

            in_var = out_var;

            while (in_var > 0 && arr[in_var - 1] >= temp) {
                yield return new WaitForSeconds(time);
                arr[in_var] = arr[in_var - 1];

                pillars[in_var].GetComponent<Pillar>().Color = tempColor;

                if (in_var + 1 < arr.Length) {
                    pillars[in_var + 1].GetComponent<Pillar>().Color = checkColor;
                }

                if (out_var < arr.Length - 1) {
                    pillars[out_var + 1].GetComponent<Pillar>().Color = nextColor;
                }

                --in_var;
            }
            arr[in_var] = temp;

            for (int i = 0; i < out_var; i++)
                pillars[i].GetComponent<Pillar>().Color = checkColor;

        }

        if (IsSorted(this.array)) {
            running = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            timer.timerText.color = new Color32(27, 255, 0, 255);
        }

    }

    IEnumerator BottomUpMergeSort(int[] a) {
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

                this.Merge(a, start, mid, end, temp);
                
            }

            for (int i = 0; i < a.Length; i++) {
                yield return new WaitForSeconds(time);
                a[i] = temp[i];

                if (runWidth > 1) {
                    if (pillars[i].GetComponent<Pillar>().Color == nextColor) {
                        for (int j = 0; j < i; j++) {
                            pillars[j].GetComponent<Pillar>().Color = whiteColor;
                        }
                    }
                } else {
                    if (pillars[i].GetComponent<Pillar>().Color == tempColor || pillars[i].GetComponent<Pillar>().Color == nextColor) {
                        for (int j = 0; j < i; j++) {
                            pillars[j].GetComponent<Pillar>().Color = whiteColor;
                        }
                    }
                }

                pillars[i].GetComponent<Pillar>().Color = swapColor;
            }
        }

        if (IsSorted(this.array)) {
            running = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            timer.timerText.color = new Color32(27, 255, 0, 255);
        }
    }

    void Merge(int[] a, int start, int mid, int end, int[] temp) {
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

    IEnumerator ShellSort(int[] arr) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int inner, outer;
        int temp;
        int h = 1;

        while (h < arr.Length / 3) {
            h = h * 3 + 1;
        }

        while (h > 0) {
            for (int i = 0; i < arr.Length; i++) {
                pillars[i].GetComponent<Pillar>().Color = whiteColor;
            }

            yield return new WaitForSeconds(time);
            for (outer = h; outer < arr.Length; outer++) {

                pillars[outer].GetComponent<Pillar>().Color = nextColor;
                if (pillars[outer - 1].GetComponent<Pillar>().Color != swapColor) {
                    pillars[outer - 1].GetComponent<Pillar>().Color = whiteColor;
                }

                yield return new WaitForSeconds(time);
                temp = arr[outer];

                inner = outer;
                while (inner > h - 1 && arr[inner - h] >= temp) {
                    yield return new WaitForSeconds(time);
                    pillars[inner - h].GetComponent<Pillar>().Color = tempColor;
                    arr[inner] = arr[inner - h];
                    pillars[inner].GetComponent<Pillar>().Color = swapColor;
                    inner -= h;
                }

                if (inner - h >= 0) {
                    pillars[inner - h].GetComponent<Pillar>().Color = swapColor;
                }

                arr[inner] = temp;
            }
            h = (h - 1) / 3;
        }

        if (IsSorted(this.array)) {
            running = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            timer.timerText.color = new Color32(27, 255, 0, 255);
        }
    }

    IEnumerator QuickSort(int[] a, int low, int high) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int[] stack = new int[high - low + 1];
        int top = -1;

        stack[++top] = low;
        stack[++top] = high;

        while (top >= 0) {
            yield return new WaitForSeconds(time);
            pillars[high].GetComponent<Pillar>().Color = whiteColor;
            high = stack[top--];
            low = stack[top--];

            //int part = partition(a, low, high, time);
            
            int pivot = a[high];
            int i = low - 1;
            int temp;

            for (int l = high; l < a.Length; l++) {
                pillars[l].GetComponent<Pillar>().Color = checkColor;
            }

            pillars[high].GetComponent<Pillar>().Color = nextColor;

            for (int j = low; j <= high - 1; j++) {
                yield return new WaitForSeconds(time);
                if (a[j] <= pivot) {
                    i++;
                    temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;

                    pillars[i].GetComponent<Pillar>().Color = tempColor;
                }
                pillars[j].GetComponent<Pillar>().Color = swapColor;
            }

            for (int k = 0; k < a.Length; k++) {
                if (pillars[k].GetComponent<Pillar>().Color != nextColor && pillars[k].GetComponent<Pillar>().Color != checkColor) {
                    pillars[k].GetComponent<Pillar>().Color = whiteColor;
                }
            }




            //pillars[i+1].GetComponent<Pillar>().Color = swapColor;

            temp = a[i + 1];
            a[i + 1] = a[high];
            a[high] = temp;

            

            int part = i + 1;

            if (part - 1 > low) {
                stack[++top] = low;
                stack[++top] = part - 1;
            }

            if (part + 1 < high) {
                stack[++top] = part + 1;
                stack[++top] = high;
            }


        }

        if (IsSorted(this.array)) {
            running = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            timer.timerText.color = new Color32(27, 255, 0, 255);
        }
    }

    IEnumerator HeapSort(int[] arr) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int N = arr.Length;

        // Build heap (rearrange array)
        for (int i = N / 2 - 1; i >= 0; i--) {
            yield return new WaitForSeconds(time);
            yield return heapify(arr, N, i);
        }

        for (int j = 0; j < arr.Length; j++) {
            pillars[j].GetComponent<Pillar>().Color = whiteColor;
        }

        // One by one extract an element from heap
        for (int i = N - 1; i > 0; i--) {
            yield return new WaitForSeconds(time);

            // Move current root to end
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            pillars[i].GetComponent<Pillar>().Color = nextColor;
            if (i+1 < arr.Length) {
                pillars[i + 1].GetComponent<Pillar>().Color = checkColor;

            }

            for (int j = 0; j < arr.Length; j++) {
                if (pillars[j].GetComponent<Pillar>().Color != checkColor && pillars[j].GetComponent<Pillar>().Color != nextColor) {
                    pillars[j].GetComponent<Pillar>().Color = whiteColor;
                }
            }

            // call max heapify on the reduced heap
            yield return heapify(arr, i, 0);
        }

        if (IsSorted(this.array)) {
            running = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            timer.timerText.color = new Color32(27, 255, 0, 255);
        }
    }

    IEnumerator heapify(int[] arr, int N, int i) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int largest = i; // Initialize largest as root
        int l = 2 * i + 1; // left = 2*i + 1
        int r = 2 * i + 2; // right = 2*i + 2

        // If left child is larger than root
        if (l < N && arr[l] > arr[largest])
            largest = l;

        // If right child is larger than largest so far
        if (r < N && arr[r] > arr[largest])
            largest = r;

        // If largest is not root
        if (largest != i) {
            pillars[i].GetComponent<Pillar>().Color = tempColor;

            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;

            pillars[largest].GetComponent<Pillar>().Color = swapColor;

            // Recursively heapify the affected sub-tree
            yield return heapify(arr, N, largest);
        }
    }

    IEnumerator RadixSort(int[] Array) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int n = Array.Length;
        int max = Array[0];

        //find largest element in the Array
        for (int i = 1; i < n; i++) {
            if (max < Array[i]) {
                max = Array[i];
            }
        }

        //Counting sort is performed based on place. 
        //like ones place, tens place and so on.
        for (int place = 1; max / place > 0; place *= 10) {
            if (place >= 0 && place < Array.Length) {
                pillars[place].GetComponent<Pillar>().Color = nextColor;
            }

            yield return new WaitForSeconds(time);
            yield return StartCoroutine(CountingSort(Array, place));
        }

        if (IsSorted(this.array)) {
            running = false;
            sortButton.GetComponentInChildren<Text>().text = "Sorted!";
            timer.timerText.color = new Color32(27, 255, 0, 255);
        }
    }

    IEnumerator CountingSort(int[] Array, int place) {
        List<GameObject> pillars = new List<GameObject>();
        foreach (Transform tran in GameObject.Find("Bar").transform) {
            pillars.Add(tran.gameObject);
        }

        int n = Array.Length;
        int[] output = new int[n];

        //range of the number is 0-9 for each place considered.
        int[] freq = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //count number of occurrences in freq array
        for (int i = 0; i < n; i++) {
            yield return new WaitForSeconds(time);
            if (pillars[i].GetComponent<Pillar>().Color != nextColor)
                pillars[i].GetComponent<Pillar>().Color = swapColor;
            freq[(Array[i] / place) % 10]++;
        }

        for (int i = 0; i < Array.Length; i++) {
            if (pillars[i].GetComponent<Pillar>().Color != nextColor)
                pillars[i].GetComponent<Pillar>().Color = whiteColor;
        }

        //Change count[i] so that count[i] now contains actual 
        //position of this digit in output[] 
        for (int i = 1; i < 10; i++) {
            //yield return new WaitForSeconds(time);
            freq[i] += freq[i - 1];
        }

        //Build the output array 
        for (int i = n - 1; i >= 0; i--) {
            yield return new WaitForSeconds(time);
            output[freq[(Array[i] / place) % 10] - 1] = Array[i];
            freq[(Array[i] / place) % 10]--;
            if (pillars[i].GetComponent<Pillar>().Color != nextColor)
                pillars[i].GetComponent<Pillar>().Color = tempColor;
        }

        for (int i = 0; i < Array.Length; i++) {
            if (pillars[i].GetComponent<Pillar>().Color != nextColor)
                pillars[i].GetComponent<Pillar>().Color = whiteColor;
        }

        //Copy the output array to the input Array, Now the Array will 
        //contain sorted array based on digit at specified place
        for (int i = 0; i < n; i++) {
            yield return new WaitForSeconds(time);
            Array[i] = output[i];
            pillars[i].GetComponent<Pillar>().Color = tempColor;
        }
    }
}