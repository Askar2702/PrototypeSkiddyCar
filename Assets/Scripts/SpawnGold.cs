using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGold : MonoBehaviour
{
    [SerializeField] private GameObject _gold;
    [SerializeField]  private Transform _spawnPoint;
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
        Debug.Log(gameObject);
       
        int chance = Random.Range(0, 10);
        if (chance > 5 && isCanSpawn)
        {
            Vector3 position = new Vector3(Random.Range(_spawnPoint.position.x - 1, _spawnPoint.position.x + 1), 0f,
                Random.Range(_spawnPoint.position.z - 1, _spawnPoint.position.z - 1));
            Instantiate(_gold, position, Quaternion.identity);
        }
    }

    
}
