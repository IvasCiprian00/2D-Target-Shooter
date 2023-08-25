using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HitDetection : MonoBehaviour
{
    public float fadeDuration = 1f;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Color originalColor;

    [SerializeField]
    private float upThrust = 20f;

    [SerializeField]
    private float rotationAmount;

    private bool isHit = false;

    public UIManager manager;

    PolygonCollider2D polygonCollider;

    Rigidbody2D rigidbody;

    private void Start()
    {
        rotationAmount = Random.Range(1f, 2f);

        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        originalColor = spriteRenderer.color;

        GenerateThrust();
    }

    private void Update()
    {
        if ((!isHit))
        {
            transform.Rotate(0, 0, rotationAmount);
        }
        CheckBoundry();
    }

    private void CheckBoundry()
    {
        if (transform.position.y < -7f || transform.position.x < -10f || transform.position.y > 10f)
        {
            if (manager != null)
            {
                manager.RemoveLife(1);
            }
            Object.Destroy(gameObject);
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        isHit = true;
        if (manager != null)
        {
            manager.AddScore(10);
        }
        polygonCollider.enabled = false;
        rigidbody.isKinematic = false;
        rigidbody.simulated = false;
        while (elapsedTime < 1f)
        {
            elapsedTime = Time.time - startTime;
            float fadeAmount = Mathf.Lerp(1f, 0f, elapsedTime); // Linear interpolation between 1 and 0

            Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, fadeAmount);
            spriteRenderer.color = fadedColor;

            yield return null; // Wait for the next frame
        }

        Object.Destroy(gameObject);
    }


    private void GenerateThrust()
    {
        float upThrust = Random.Range(450f, 800f);
        float sideThrust = Random.Range(0f, 400f);
        Vector2 thrust;

        if(transform.position.x <= 0)
        {
            thrust = new Vector2(sideThrust, upThrust);
        }
        else
        {
            thrust = new Vector2(-sideThrust, upThrust);
        }

        if (gameObject != null && rigidbody != null)
        {
            rigidbody.AddForce(thrust);
        }
    }

    private void OnMouseDown()
    {
        StartCoroutine(FadeOutCoroutine());
    }

}
