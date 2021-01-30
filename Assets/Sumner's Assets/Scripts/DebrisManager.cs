using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisManager : MonoBehaviour
{
    private Rigidbody2D rb;

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

            if (collision.relativeVelocity.magnitude >= collision.gameObject.GetComponent<PlayerMovement>().colResistance)
            {
                collision.gameObject.GetComponent<PlayerManager>().oxygenAmount -= collision.relativeVelocity.magnitude;

                if(collision.gameObject.GetComponent<PlayerManager>().currentPropellant == PropellantTypes.SolarSail)
                {
                    collision.gameObject.GetComponent<PlayerManager>().propellantFuel -= 25;
                }
            }
        }
    }
}
