﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public bool hasKeys;

    [SerializeField]
    public float oxygenAmount;

    [SerializeField]
    public float oxygenTickRate;
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
        oxygenAmount -= Time.deltaTime * oxygenTickRate;
        if(propellantFuel <= 0)
        {
            propellantFuel = 1;
            currentPropellant = PropellantTypes.Suit;
        }
    }

    public void SetPropellant(PropellantTypes p)
    {
        propellantFuel = 100;
        currentPropellant = p;
    }
}
