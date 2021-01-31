using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public bool hasKeys;

    [SerializeField]
    public float oxygenAmount;

    public float distanceTraveled;

    public int numberOfCollisions;

    public int collectablesPickedUp;

    public int oxygenPickedUp;

    public int propellantsPickedUp;

    public Dictionary<PropellantTypes, float> distanceLibrary = new Dictionary<PropellantTypes, float>(); 

    private Vector3 lastPosition;
    [SerializeField]
    public float oxygenTickRate;
    public float propellantFuel;
    [SerializeField]
    public PropellantTypes currentPropellant;

    private AudioSource aSrc;

    // Start is called before the first frame update
    void Start()
    {
        currentPropellant = PropellantTypes.Jetpack;
        propellantFuel = 50;
        lastPosition = transform.position;
        distanceLibrary[PropellantTypes.Suit] = 0;
        distanceLibrary[PropellantTypes.Jetpack] = 0;

        aSrc = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        oxygenAmount -= Time.deltaTime * oxygenTickRate;
        if(propellantFuel <= 0)
        {
            propellantFuel = 1;
            currentPropellant = PropellantTypes.Suit;
            aSrc.Stop();
        }
        distanceTraveled += Vector3.Distance(lastPosition, transform.position);
        distanceLibrary[currentPropellant] += Vector3.Distance(lastPosition, transform.position);
        lastPosition = transform.position;
    }

    public void SetPropellant(PropellantTypes p)
    {
        propellantsPickedUp++;
        propellantFuel = 100;
        if (!distanceLibrary.ContainsKey(p)) { 
            distanceLibrary[p] = 0;
        }
        if(currentPropellant != PropellantTypes.Popper)
        {
            aSrc.Stop();
        }
        currentPropellant = p;
    }
}
