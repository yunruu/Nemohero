using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoryRepeatGround : MonoBehaviour
{
    //private Vector2 startPos;
    private float repeatPoint;

    // Start is called before the first frame update
    void Start()
    {
        repeatPoint = GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -repeatPoint)
        {
            transform.position = new Vector2(transform.position.x + 2 * repeatPoint, transform.position.y);
        }
    }
}
