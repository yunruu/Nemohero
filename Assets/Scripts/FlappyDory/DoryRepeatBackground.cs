using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoryRepeatBackground : MonoBehaviour
{
    private MeshRenderer meshR;
    private float speed = 0.05f;
    private int yVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshR.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, yVal);
    }
}
