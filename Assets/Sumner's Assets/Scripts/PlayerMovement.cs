using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private ParticleSystem particles;
    private PlayerManager manager;

    // how fast can the player spin
    public float rotationRate = 4f;

    // how fast does the player get accelerated

    public float thrust = 1f;
    // how fast fuel gets used up

    public float fuelConsumptionRate = 1f;

    // the threshold where the player loses oxygen from collisions
    public float colResistance = 3;

    private float popperCooldown = 1f;
    private float curCooldown = 0f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        particles = gameObject.GetComponent<ParticleSystem>();
        manager = gameObject.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (manager.currentPropellant == PropellantTypes.Popper)
        {
            ThrustImpulse();
        }

        rb.angularVelocity = 0;
    }

    void FixedUpdate()
    {
        // adds current horizontal input axis value to z axis rotation
        gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.eulerAngles.z - (Input.GetAxis("Horizontal") * rotationRate));

        if((manager.currentPropellant == PropellantTypes.FireExt || manager.currentPropellant == PropellantTypes.Jetpack) && manager.propellantFuel > 0)
        {
            ThrustContinuous();
        }

        if (manager.currentPropellant == PropellantTypes.SolarSail)
        {
            ThrustConstant();
        }
    }

    private void ThrustContinuous()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            manager.propellantFuel -= Time.deltaTime * fuelConsumptionRate;
            particles.Emit(5);
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
            particles.Emit(50);
            rb.AddForce(gameObject.transform.up * thrust, ForceMode2D.Impulse);

            curCooldown = popperCooldown;
        }
    }

    private void ThrustConstant()
    {
        rb.AddForce(gameObject.transform.up * thrust);
    }
}
