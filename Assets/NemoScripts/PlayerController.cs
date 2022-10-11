using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private float laneSize = 3.4f;
    private bool hasBoo = false;
    private bool hasShield = false;
    //private bool hasBoots = false;
    public TextMeshProUGUI ghostText;
    public TextMeshProUGUI shieldText;
    //public TextMeshProUGUI bootsText;

    // Start is called before the first frame update
    void Start()
    {
        ghostText.gameObject.SetActive(false);
        shieldText.gameObject.SetActive(false);
        //bootsText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

   /** private void FixedUpdate()
    {
        move();
    }*/

    private void move()
    {
        /**if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos.x > 0 && transform.position.x < 2)
            {
                transform.position = new Vector2(transform.position.x + laneSize, transform.position.y);
            } else if (mousePos.x < 0 && transform.position.x > -2)
            {
                transform.position = new Vector2(transform.position.x - laneSize, transform.position.y);
            }
        }*/
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y == -laneSize)
        {
            transform.position = new Vector2(transform.position.x, 0);
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y == 0)
        {
            transform.position = new Vector2(transform.position.x, laneSize);
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y == laneSize)
        { 
            transform.position = new Vector2(transform.position.x, 0);
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y == 0)
        {
            transform.position = new Vector2(transform.position.x, -laneSize);
        } else
        {
            transform.position = transform.position;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && hasShield)
        {
            Destroy(collision);
            hasShield = false;
            shieldText.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Obstacle") && hasBoo)
        {
            Destroy(collision);
        }
        /**else if (collision.gameObject.CompareTag("Boots"))
        {
            Destroy(collision.gameObject);
            hasBoots = true;
            StartCoroutine(BootsCountdownRoutine());
        }*/
        else if (collision.gameObject.CompareTag("Boo"))
        {
            Destroy(collision.gameObject);
            hasBoo = true;
            ghostText.gameObject.SetActive(true);
            StartCoroutine(BooCountdownRoutine());
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
            hasShield = true;
            shieldText.gameObject.SetActive(true);
            //StartCoroutine(ShieldCountdownRoutine());
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
    
    IEnumerator BooCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasBoo = false;
        ghostText.gameObject.SetActive(false);
    }

    /**IEnumerator BootsCountdownRoutine()
    {
        MoveLeft[] objs = FindObjectsOfType<MoveLeft>();
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].speed = 2.5f;
        }
        FindObjectOfType<MoveBackground>().speed = 0.1f;

        yield return new WaitForSeconds(5);
        hasBoots = false;
        bootsText.gameObject.SetActive(false);
    }*/

    //IEnumerator ShieldCountdownRoutine()
    //{
    //    yield return new WaitForSeconds(5);
    //    hasShield = false;
    //    shieldText.gameObject.SetActive(false);
    //}

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        movement = Vector3.zero;
    }
}
