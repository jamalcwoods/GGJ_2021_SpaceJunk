using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private PlayerManager player;

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.hasKeys)
            {
                print("You Win!");
            }
            else
            {
                print("I need my keys!");
            }
        }
    }
}
