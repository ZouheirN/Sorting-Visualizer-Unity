using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public GameObject pillar;
    public GameObject pivotPillar;
    public int value;
    public float offset;
    public int size;
    public Material Color;

    // Start is called before the first frame update
    void Start()
    {
        pivotPillar.transform.localPosition = new Vector3(-0.5f + offset, 0.5f,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pivotPillar.transform.localScale = new Vector3((float)1 / size, value % 60, 1);
        pillar.GetComponent<MeshRenderer>().material = Color;
    }
}
