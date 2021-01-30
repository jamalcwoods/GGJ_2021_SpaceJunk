using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private ParticleSystem particles;

    // how fast can the player spin
    public float rotationRate = 1f;

    // how fast does the player get accelerated
    public float thrust = 1f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        particles = gameObject.GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        // adds current horizontal input axis value to z axis rotation
        gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.eulerAngles.z - (Input.GetAxis("Horizontal") * rotationRate));

        if (Input.GetKey(KeyCode.Space))
        {
            particles.Emit(5);
            rb.AddForce(gameObject.transform.up * thrust);
        }
    }
}
