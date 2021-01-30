﻿using System.Collections;
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
}
