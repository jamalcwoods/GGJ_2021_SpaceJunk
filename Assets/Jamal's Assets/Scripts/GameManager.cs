using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject ItemPreab, playerPrefab;

    private int spawnRange = 20;
    private int spawnAmount = 80;

    private GameObject playerInstance;

    private List<GameObject> itemInstances = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnAmount; i++) {
            Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange),0);
            GameObject g = Instantiate(ItemPreab, v,Quaternion.identity);
            ItemManager item = g.GetComponent<ItemManager>();
            switch (Random.Range(0,2))
            {
                case 0:
                    item.Type = ItemTypes.Propellant;
                    item.PType = PropellantTypes.Jetpack;
                    break;

                case 1:
                    item.Type = ItemTypes.Oxygen;
                    item.OxygenReplinishAmount = 20;
                    break;
            }
            itemInstances.Add(g);
        }
        playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
