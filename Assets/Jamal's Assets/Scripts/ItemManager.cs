﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private ItemTypes type;
    private PropellantTypes pType;
    private float oxygenReplinishAmount;


    public ItemTypes Type
    {
        get { return type; }
        set { type = value; }
    }

    public PropellantTypes PType
    {
        get { return pType; }
        set { pType = value; }
    }

    public float OxygenReplinishAmount
    {
        get { return oxygenReplinishAmount; }
        set { oxygenReplinishAmount = value; }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>())
        {
            switch (type)
            {
                case ItemTypes.Propellant:
                    collision.gameObject.GetComponent<PlayerManager>().SetPropellant(pType);
                    break;

                case ItemTypes.Oxygen:
                    collision.gameObject.GetComponent<PlayerManager>().oxygenAmount += oxygenReplinishAmount;
                    if(collision.gameObject.GetComponent<PlayerManager>().oxygenAmount > 100)
                    {
                        collision.gameObject.GetComponent<PlayerManager>().oxygenAmount = 100;
                    }
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}