using System.Collections;
using UnityEngine;

public class OceanPlayer : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 pos = new Vector3(0, 0, 0);

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex = 0;

    public float speedMultiplier;
    public float health;
    public OceanGameManager gameManager;

    private RaycastHit2D hit;

    private void Start()
    {
        transform.Translate(pos);
        boxCollider = GetComponent<BoxCollider2D>();
        InvokeRepeating(nameof(Animate), 0.15f, 0.15f);
        speedMultiplier = 1.5f;
        health = 100;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        health = 100;
        // spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // resetting player position
        pos = new Vector3(x, y, 0);

        // changing player direction
        if (pos.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (pos.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // moving player after checking for blocking layer
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
            new Vector2(0, pos.y), Mathf.Abs(pos.y * Time.deltaTime * speedMultiplier),
            LayerMask.GetMask("Obs"));

        if (hit.collider == null)
        {
            transform.Translate(0, pos.y * Time.deltaTime * speedMultiplier, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
            new Vector2(pos.x, 0), Mathf.Abs(pos.x * Time.deltaTime * speedMultiplier),
            LayerMask.GetMask("Obs"));

        if (hit.collider == null)
        {
            transform.Translate(pos.x * Time.deltaTime * speedMultiplier, 0, 0);
        }
    }

    private void OnEnable()
    {
        Vector3 pos = transform.position;
        pos.y = 0f;
        pos.x = 0f;
        transform.position = pos;
    }

    IEnumerator SpeedUp()
    {
        speedMultiplier += 2f;
        yield return new WaitForSeconds(5f);
        speedMultiplier -= 2f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            this.health -= 10;
        }

        else if (other.gameObject.tag == "Block") { }

        else
        {
            if (other.gameObject.tag == "Obstacle")
            {
                this.health -= 20;
            }

            else if (other.gameObject.tag == "Devil")
            {
                this.health = 0;
            }

            else if (other.gameObject.tag == "Heal")
            {
                this.health += Random.Range(1, 20);
            }

            else if (other.gameObject.tag == "Seashell")
            {
                StartCoroutine(SpeedUp());
            }

            Destroy(other.gameObject);
        }

        if (this.health <= 0)
        {
            FindObjectOfType<OceanGameManager>().GameOver();
        }
    }

    private void Animate()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    public void IncreaseSpeed()
    {
        this.speedMultiplier += 0.25f;
    }

    public void Restart()
    {
        this.health = 100;
        this.speedMultiplier = 1.5f;
        transform.Translate(new Vector3(0, 0, 0));
    }
}
