using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    ArrayController arrayController;

    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;

    private void Awake() {
        arrayController = GameObject.Find("Array").GetComponent<ArrayController>();
    }

    void Update()
    {
        if (arrayController.running) {
            currentTime += Time.deltaTime;
            timerText.text = "Time: " + currentTime.ToString("0.000");
        }

    }
}
