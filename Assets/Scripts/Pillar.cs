using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    ArrayController arrayController;
    public GameObject pillar;

    // Start is called before the first frame update
    void Awake()
    {
        arrayController = FindObjectOfType<ArrayController>();
    }

    // Update is called once per frame
    void Update()
    {
        //pillar.transform.localScale = new Vector3(arrayController.array[0], 0, 0);
    }
}
