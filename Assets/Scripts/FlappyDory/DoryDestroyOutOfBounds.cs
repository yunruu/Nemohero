using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoryDestroyOutOfBounds : MonoBehaviour
{
    private float leftBound = -8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
