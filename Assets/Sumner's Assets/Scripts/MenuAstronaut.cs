using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAstronaut : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.AddTorque(Random.Range(-10, 10));
        rb.AddForce(new Vector2(25, Random.Range(-25, 25)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
