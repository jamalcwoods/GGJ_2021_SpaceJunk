using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float oxygenAmount;
    private float fuelAmount;
    private float propellantFuel;
    private PropellantTypes currentPropellant;
    private CollectableTypes currentCollectable;


    public float OxygenAmount
    {
        get { return oxygenAmount; }
        set { oxygenAmount = value; }
    }

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
