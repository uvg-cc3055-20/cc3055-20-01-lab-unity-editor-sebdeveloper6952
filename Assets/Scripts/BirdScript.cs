using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public float jumpForce = 10f;
    public float forwardSpeed = 2f;
    public GameObject cam;
    public float posXFinal = 53f;
    public GameObject bloodEffect;
    public Sprite deadSprite;

    private Rigidbody2D rb;
    private AudioSource audio;
    private bool dead;

    private void Start()
    {
        // obtener componente rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        dead = false;
    }

    private void Update()
    {
        if(!dead)
        {
            // mover personaje en el ejex 
            transform.Translate(Vector2.right * forwardSpeed * Time.deltaTime);
            // hacer que la camara siga al bird
            cam.transform.Translate(Vector2.right * forwardSpeed * Time.deltaTime);
            // detecta cuando el usuario comienza a presionar el boton ligado al input Jump
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            // incrementar forwardSpeed para incrementar dificultad
            forwardSpeed += Time.deltaTime;
        }
        RevisarSiLlegoAlFinal();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Morir();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void RevisarSiLlegoAlFinal()
    {
        if (transform.position.x >= posXFinal)
            Morir();
    }

    private void Morir()
    {
        dead = true;
        //GetComponent<SpriteRenderer>().sprite = deadSprite;
        Instantiate(bloodEffect, transform.position, bloodEffect.transform.rotation);
        audio.Play();
    }
}
