using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanObstacles : MonoBehaviour
{
    public float speed = 3f;
    private float leftEnd;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex = 0;

    private void Start()
    {
        leftEnd = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
        InvokeRepeating(nameof(Animate), 0.15f, 0.15f);
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEnd)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        speed += 5;
    }
}
