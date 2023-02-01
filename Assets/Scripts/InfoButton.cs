using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour
{
    public GameObject panel;

    private void Awake() {
        panel.SetActive(false);
    }

    public void btnClick() {
        panel.SetActive(!panel.activeSelf);
    }
}
