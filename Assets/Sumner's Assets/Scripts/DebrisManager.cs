using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisManager : MonoBehaviour
{
    private Rigidbody2D rb;
    public float colResistance = 3;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.AddTorque(Random.Range(-1, 1));
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>())
        {
            print(collision.relativeVelocity.magnitude);

            if(collision.relativeVelocity.magnitude >= colResistance)
            {
                collision.gameObject.GetComponent<PlayerManager>().OxygenAmount -= collision.relativeVelocity.magnitude;
            }
        }
    }
}
