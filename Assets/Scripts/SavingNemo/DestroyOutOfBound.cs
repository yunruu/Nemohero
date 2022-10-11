using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private float lowerBound = -5.34f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }
    }
}
