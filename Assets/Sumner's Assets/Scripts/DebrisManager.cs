﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;
    private AudioSource aSrc;
    [SerializeField] private AudioClip[] clangs;
    [SerializeField] private AudioClip ouch;
    public PolygonCollider2D[] polygons;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        aSrc = gameObject.GetComponent<AudioSource>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        rb.AddTorque(Random.Range(-1, 1));
        rb.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), ForceMode2D.Impulse);
    }

    public void setTextureCollider(int i)
    {
        polygons[i].enabled = true;
    }

    void Update()
    {
        if((transform.position.x > 55 || transform.position.x < -55 || transform.position.y > 55 || transform.position.y < -55) && (cam.WorldToViewportPoint(transform.position).x > 1 || cam.WorldToViewportPoint(transform.position).x < 0 || cam.WorldToViewportPoint(transform.position).y > 1 || cam.WorldToViewportPoint(transform.position).y < 0))
        {
            ResetPosition();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>())
        {
            aSrc.PlayOneShot(clangs[Mathf.RoundToInt(Random.Range(0, 8))], Mathf.Clamp(collision.relativeVelocity.magnitude/10, 0, 1));
            print(collision.relativeVelocity.magnitude);
            collision.gameObject.GetComponent<PlayerManager>().numberOfCollisions++;
            if (collision.relativeVelocity.magnitude >= collision.gameObject.GetComponent<PlayerMovement>().colResistance)
            {
                collision.gameObject.GetComponent<PlayerManager>().oxygenAmount -= collision.relativeVelocity.magnitude * 5;
                aSrc.PlayOneShot(ouch);

                if(collision.gameObject.GetComponent<PlayerManager>().currentPropellant == PropellantTypes.SolarSail)
                {
                    collision.gameObject.GetComponent<PlayerManager>().propellantFuel -= collision.relativeVelocity.magnitude * 12.5f; 
                }
            }
        }
    }

    private void ResetPosition()
    {
        Vector2 newPos = new Vector2(Random.Range(-40, 40), Random.Range(-40, 40));

        while(!(cam.WorldToViewportPoint(newPos).x > 1 || cam.WorldToViewportPoint(newPos).x < 0 || cam.WorldToViewportPoint(newPos).y > 1 || cam.WorldToViewportPoint(newPos).y < 0))
        {
            newPos = new Vector2(Random.Range(-40, 40), Random.Range(-40, 40));
        }

        if(cam.WorldToViewportPoint(newPos).x > 1 || cam.WorldToViewportPoint(newPos).x < 0 || cam.WorldToViewportPoint(newPos).y > 1 || cam.WorldToViewportPoint(newPos).y < 0)
        {
            transform.position = newPos;
        }
    }
}
