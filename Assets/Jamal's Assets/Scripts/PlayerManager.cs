﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float oxygenAmount;
    private float oxygenTickRate;
    private float fuelAmount;
    public float propellantFuel;
    [SerializeField]
    public PropellantTypes currentPropellant;

    // Start is called before the first frame update
    void Start()
    {
        currentPropellant = PropellantTypes.Jetpack;
        propellantFuel = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPropellant(PropellantTypes p)
    {
        propellantFuel = 100;
        currentPropellant = p;
    }
}
