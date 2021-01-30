using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {

    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -5);
    }
}
