using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private ItemTypes type;
    private PropellantTypes pType;
    private CollectableTypes cType;
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
            PlayerManager p = collision.gameObject.GetComponent<PlayerManager>();
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            switch (type)
            {
                case ItemTypes.Propellant:
                    p.SetPropellant(pType);
                    break;

                case ItemTypes.Oxygen:
                    collision.gameObject.GetComponent<PlayerManager>().oxygenAmount += oxygenReplinishAmount;
                    if(collision.gameObject.GetComponent<PlayerManager>().oxygenAmount > 100)
                    {
                        collision.gameObject.GetComponent<PlayerManager>().oxygenAmount = 100;
                    }
                    break;

                case ItemTypes.Collectable:
                    switch (cType)
                    {
                        case CollectableTypes.Armor:
                            pm.colResistance += 2;
                            break;

                        case CollectableTypes.Keys:
                            p.hasKeys = true;
                            break;

                        case CollectableTypes.Rebreather:
                            p.oxygenTickRate *= 0.75f;
                            break;

                        case CollectableTypes.PropulsionComputer:
                            collision.gameObject.GetComponent<Rigidbody2D>().angularDrag *= 0.75f;
                            break;
                    }
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}