using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private ItemTypes type;
    private PropellantTypes pType;
    private CollectableTypes cType;
    private float oxygenReplinishAmount;
    [SerializeField] private AudioClip pickup;
    public PolygonCollider2D[] polygons;


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

    public CollectableTypes CType
    {
        get { return cType; }
        set { cType = value; }
    }

    public float OxygenReplinishAmount
    {
        get { return oxygenReplinishAmount; }
        set { oxygenReplinishAmount = value; }
    }

    void Start()
    {
        polygons = gameObject.GetComponents<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case ItemTypes.Collectable:
                switch (cType)
                {
                    case CollectableTypes.Rebreather:
                        polygons[0].enabled = true;
                        break;

                    case CollectableTypes.Armor:
                        polygons[1].enabled = true;
                        break;

                    case CollectableTypes.PropulsionComputer:
                        polygons[2].enabled = true;
                        break;

                    case CollectableTypes.Keys:
                        polygons[7].enabled = true;
                        break;
                }
                break;

            case ItemTypes.Propellant:
                switch (pType)
                {
                    case PropellantTypes.FireExt:
                        polygons[3].enabled = true;
                        break;

                    case PropellantTypes.Jetpack:
                        polygons[4].enabled = true;
                        break;

                    case PropellantTypes.SolarSail:
                        polygons[5].enabled = true;
                        break;

                    case PropellantTypes.Popper:
                        polygons[6].enabled = true;
                        break;
                }
                break;

            case ItemTypes.Oxygen:
                polygons[8].enabled = true;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>())
        {
            PlayerManager p = collision.gameObject.GetComponent<PlayerManager>();
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.aSrcp.PlayOneShot(pickup);
            switch (type)
            {
                case ItemTypes.Propellant:
                    p.SetPropellant(pType);
                    break;

                case ItemTypes.Oxygen:
                    p.oxygenPickedUp++;
                    collision.gameObject.GetComponent<PlayerManager>().oxygenAmount += oxygenReplinishAmount;
                    if(collision.gameObject.GetComponent<PlayerManager>().oxygenAmount > 100)
                    {
                        collision.gameObject.GetComponent<PlayerManager>().oxygenAmount = 100;
                    }
                    break;

                case ItemTypes.Collectable:
                    p.collectablesPickedUp++;
                    switch (cType)
                    {
                        case CollectableTypes.Armor:
                            pm.colResistance += 2;
                            break;

                        case CollectableTypes.Keys:
                            p.hasKeys = true;
                            break;

                        case CollectableTypes.Rebreather:
                            p.oxygenTickRate *= 0.8f;
                            if(p.oxygenTickRate < 0.2)
                            {
                                p.oxygenTickRate = 0.2f;
                            }
                            break;

                        case CollectableTypes.PropulsionComputer:
                            collision.gameObject.GetComponent<Rigidbody2D>().drag += 0.1f;
                            if(collision.gameObject.GetComponent<Rigidbody2D>().drag > 0.7f)
                            {
                                collision.gameObject.GetComponent<Rigidbody2D>().drag = 0.7f;
                            }
                            break;
                    }
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}