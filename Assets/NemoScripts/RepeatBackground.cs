using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector2 startPos;
    private float repeatPoint;

    // Start is called before the first frame update

    void Start()
    {
        startPos = transform.position;
        repeatPoint = GetComponent<BoxCollider2D>().size.y * 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < startPos.y - repeatPoint)
        {
            transform.position = startPos;
        }
    }

}
