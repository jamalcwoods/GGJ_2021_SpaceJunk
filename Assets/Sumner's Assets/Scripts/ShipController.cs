using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private PlayerManager player;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

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
                gm.WinGame();
            }
            else
            {
                print("I need my keys!");
            }
        }
    }
}
