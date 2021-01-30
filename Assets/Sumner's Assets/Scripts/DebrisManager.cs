using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        rb.AddTorque(Random.Range(-1, 1));
        rb.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), ForceMode2D.Impulse);
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
            print(collision.relativeVelocity.magnitude);
            collision.gameObject.GetComponent<PlayerManager>().numberOfCollisions++;
            if (collision.relativeVelocity.magnitude >= collision.gameObject.GetComponent<PlayerMovement>().colResistance)
            {
                collision.gameObject.GetComponent<PlayerManager>().oxygenAmount -= collision.relativeVelocity.magnitude * 5;

                if(collision.gameObject.GetComponent<PlayerManager>().currentPropellant == PropellantTypes.SolarSail)
                {
                    collision.gameObject.GetComponent<PlayerManager>().propellantFuel -= collision.relativeVelocity.magnitude * 7.5f; 
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
