using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private GameObject fireExPart, suitPart, jetpackPart, popperPart;
    private PlayerManager manager;

    // how fast can the player spin
    public float rotationRate = 4f;

    // how fast does the player get accelerated
    public float thrust = 1f;

    // how fast fuel gets used up
    public float fuelConsumptionRate = 1f;

    // the threshold where the player loses oxygen from collisions
    public float colResistance = 1;

    // how fast oxygen gets used up when using suit as propullsion
    public float oxygenConsumptionRate = 4;

    private float popperCooldown = 1f;
    private float curCooldown = 0f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        manager = gameObject.GetComponent<PlayerManager>();

        fireExPart = gameObject.transform.Find("FireExtinguisherParticles").gameObject;
        suitPart = gameObject.transform.Find("SuitParticles").gameObject;
        jetpackPart = gameObject.transform.Find("JetPackParticles").gameObject;
        popperPart = gameObject.transform.Find("PopperParticles").gameObject;
    }

    private void Update()
    {
        if (manager.currentPropellant == PropellantTypes.Popper)
        {
            ThrustImpulse();
        }

        SetThrustVariables();

        rb.angularVelocity = 0;
    }

    void FixedUpdate()
    {
        // adds current horizontal input axis value to z axis rotation
        gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.eulerAngles.z - (Input.GetAxis("Horizontal") * rotationRate));

        if((manager.currentPropellant == PropellantTypes.FireExt || manager.currentPropellant == PropellantTypes.Jetpack || manager.currentPropellant == PropellantTypes.Suit) && manager.propellantFuel > 0)
        {
            ThrustContinuous();
        }

        if (manager.currentPropellant == PropellantTypes.SolarSail && manager.propellantFuel > 0)
        {
            ThrustConstant();
        }
    }

    private void SetThrustVariables()
    {
        switch (manager.currentPropellant)
        {
            case PropellantTypes.FireExt:
                thrust = 2f;
                rotationRate = 10f;
                fuelConsumptionRate = 12f;
                break;

            case PropellantTypes.Jetpack:
                thrust = 0.8f;
                rotationRate = 5f;
                fuelConsumptionRate = 6f;
                break;

            case PropellantTypes.Popper:
                thrust = 3f;
                rotationRate = 4f;
                fuelConsumptionRate = 15f;
                break;

            case PropellantTypes.SolarSail:
                thrust = 0.6f;
                rotationRate = 3f;
                fuelConsumptionRate = 0f;
                break;

            case PropellantTypes.Suit:
                thrust = 0.4f;
                rotationRate = 3f;
                fuelConsumptionRate = 0f;
                break;
        }
    }

    private void ThrustContinuous()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            manager.propellantFuel -= Time.deltaTime * fuelConsumptionRate;
            if(manager.currentPropellant == PropellantTypes.Suit)
            {
                manager.oxygenAmount -= Time.deltaTime * oxygenConsumptionRate;
            }

            switch (manager.currentPropellant)
            {
                case PropellantTypes.FireExt:
                    fireExPart.GetComponent<ParticleSystem>().Emit(15);
                    break;

                case PropellantTypes.Suit:
                    suitPart.GetComponent<ParticleSystem>().Emit(15);
                    break;

                case PropellantTypes.Jetpack:
                    jetpackPart.GetComponent<ParticleSystem>().Emit(15);
                    break;
            }

            rb.AddForce(gameObject.transform.up * thrust);
        }
    }

    private void ThrustImpulse()
    {
        if(curCooldown > 0)
        {
            curCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && curCooldown <= 0)
        {
            manager.propellantFuel -= fuelConsumptionRate;
            popperPart.GetComponent<ParticleSystem>().Emit(50);
            rb.AddForce(gameObject.transform.up * thrust, ForceMode2D.Impulse);

            curCooldown = popperCooldown;
        }
    }

    private void ThrustConstant()
    {
        rb.AddForce(gameObject.transform.up * thrust);
    }
}
