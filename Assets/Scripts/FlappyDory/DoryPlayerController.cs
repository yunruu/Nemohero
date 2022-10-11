using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoryPlayerController : MonoBehaviour
{
    private Vector3 movement;
    private float speed = 1.8f;
    private float gravity = -5f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement = Vector3.up * speed;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                movement = Vector3.up * speed;
            }
        }

        movement.y += gravity * Time.deltaTime;
        transform.position += movement * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Ground")
        {
            FindObjectOfType<DoryGameManager>().GameOver();
        } else if (collision.gameObject.tag == "Gap")
        {
            FindObjectOfType<DoryGameManager>().IncreaseScore();
        }
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        movement = Vector3.zero;
    }
}
