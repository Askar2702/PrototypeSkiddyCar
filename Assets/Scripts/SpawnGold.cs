using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGold : MonoBehaviour
{
    [SerializeField] GameObject gold;
    [SerializeField] Transform SpawnPoint;
    RoadGeneration roadGeneration;
    bool isCanSpawn = false;
    private void Awake()
    {
        roadGeneration = FindObjectOfType<RoadGeneration>();
    }
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            if (roadGeneration.Cell[i] == transform)
            {
                isCanSpawn = false;
                break;
            }
            else isCanSpawn = true;
        }
       
        int chance = Random.Range(0, 10);
        if (chance > 5 && isCanSpawn)
        {
            Vector3 position = new Vector3(Random.Range(SpawnPoint.position.x - 1, SpawnPoint.position.x + 1), 0f,
                Random.Range(SpawnPoint.position.z - 1, SpawnPoint.position.z - 1));
            Instantiate(gold, position, Quaternion.identity);
        }
    }

    
}
