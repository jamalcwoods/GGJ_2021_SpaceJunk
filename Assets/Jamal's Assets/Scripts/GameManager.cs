using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject ItemPreab, playerPrefab, debriPrefab;

    [SerializeField]
    private GameObject oxygenBar; 

    private int spawnRange = 50;
    private int oxygenSpawnAmount = 90;
    private int propellantSpawnAmount = 60;
    private int debriSpawnAmount = 400;

    private int minDebriSize = 1;
    private int maxDebriSize = 4;

    private GameObject playerInstance;

    private List<GameObject> itemInstances = new List<GameObject>(), debriInstances = new List<GameObject>(),entityInstances = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < oxygenSpawnAmount; i++) {
            Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange),0);
            while (!checkForOpenSpawn(v))
            {
                v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            }
            GameObject g = Instantiate(ItemPreab, v,Quaternion.identity);
            ItemManager item = g.GetComponent<ItemManager>();
            g.transform.parent = transform;
            item.Type = ItemTypes.Oxygen;
            item.OxygenReplinishAmount = 20;
            item.GetComponent<SpriteRenderer>().color = Color.blue;
            itemInstances.Add(g);
            entityInstances.Add(g);
        }

        for (int i = 0; i < propellantSpawnAmount; i++) {
            Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            while (!checkForOpenSpawn(v))
            {
                v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            }
            GameObject g = Instantiate(ItemPreab, v, Quaternion.identity);
            g.transform.parent = transform;
            ItemManager item = g.GetComponent<ItemManager>();
            item.Type = ItemTypes.Propellant;
            int pIndex = Random.Range(0, 4);
            item.PType = (PropellantTypes)pIndex;
            item.GetComponent<SpriteRenderer>().color = new Color[] { Color.red, Color.green, Color.yellow, Color.cyan }[pIndex];
            itemInstances.Add(g);
            entityInstances.Add(g);
        }

        for (int i = 0; i < debriSpawnAmount; i++)
        {
            Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            while (!checkForOpenSpawn(v))
            {
                v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            }
            GameObject g = Instantiate(debriPrefab, v, Quaternion.identity);
            g.transform.parent = transform;
            int size = Random.Range(minDebriSize, maxDebriSize);
            g.transform.localScale = new Vector3(size,size,1);
            g.GetComponent<Rigidbody2D>().mass *= size;
            debriInstances.Add(g);
            entityInstances.Add(g);
        }
        playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    bool checkForOpenSpawn(Vector3 v)
    {
        bool valid = true;
        foreach(GameObject g in entityInstances)
        {
            if(Vector3.Distance(g.transform.position,v) < 2)
            {
                valid = false;
                break;
            }
        }
        return valid;
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform r = oxygenBar.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(600f * (playerInstance.GetComponent<PlayerManager>().oxygenAmount / 100), r.sizeDelta.y);
    }
}
