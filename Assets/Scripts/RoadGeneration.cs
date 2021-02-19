using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadGeneration : MonoBehaviour
{
    [SerializeField] GameObject EasyRoad;
    [SerializeField] GameObject VeryHardRoad;
    [SerializeField] GameObject HardRoad;
    [SerializeField] int countSpawnRoad;
    [SerializeField] Material[] roadMat;
    public List<Transform> Cell = new List<Transform>();
    [SerializeField] int Step = 30;
    [SerializeField] Material finishMat;
    int turnChance = 0;
    MeshRenderer Mesh;
    Vector3 MeshSize;
    Vector3 position;
    GameObject _road;
    GameManager gameManager;
    GameObject road;
    void Start()
    {
        gameManager = GameManager.gameManager;
        turnChance = gameManager.turnChance;
        ChoiceOfRoad();
        GenerationCell();
    }
    private void GenerationCell()
    {
        if (countSpawnRoad > 0)
        {
            while (Step > 0) {
                int rand = Random.Range(turnChance, countSpawnRoad);
                for (int i = 0; i < rand; i++)
                {// тут решается сколько блоков рандомно будет чтоб после них начал спавнить блок справа
                   // Debug.Log(rand);
                    Mesh = road.GetComponent<MeshRenderer>();
                    MeshSize = Mesh.bounds.size + new Vector3(0.0f, 0, 0.0f);
                    if (Cell.Count > 0)
                        position = new Vector3(Cell.LastOrDefault().localPosition.x , 0f, Cell.LastOrDefault().localPosition.z + 1 * MeshSize.z);
                    else
                        position = new Vector3(transform.position.x, 0f, transform.position.z + i * MeshSize.z);
                    _road = Instantiate(road, position, Quaternion.identity);
                    _road.transform.parent = transform;
                    Cell.Add(_road.transform);
                    Step--;
                    if (i >= rand - 1)
                    {
                        int rand2 = Random.Range(turnChance, countSpawnRoad);
                        for (int j = 0; j < rand2; j++)
                        {
                            position = new Vector3(Cell.LastOrDefault().transform.localPosition.x + 1 * MeshSize.x, 0f, Cell.LastOrDefault().transform.localPosition.z);
                            _road = Instantiate(road, position, Quaternion.identity);
                            _road.transform.parent = transform;
                            Cell.Add(_road.transform);
                            Step--;
                        }
                    }
                }
            }
            ChangeMatCell();
        }
    }

    private void ChangeMatCell()
    {
        var _mat = roadMat[Random.Range(0, roadMat.Length)];
        foreach (var mat in Cell)
            mat.GetComponent<Renderer>().material = _mat;
        var lastCell = Cell.LastOrDefault();
        lastCell.GetComponent<Renderer>().material = finishMat;
        lastCell.gameObject.AddComponent<Finish>();
    }
    private void ChoiceOfRoad()
   {
        if(gameManager._gameMode == GameManager.GameMode.VeryHard)
        {
            road = VeryHardRoad;
            Step = 70;
        }
        else if (gameManager._gameMode == GameManager.GameMode.Hard)
        {
            road = HardRoad;
            Step = 50;
        }
        else
        {
            road = EasyRoad;
        }
   }


}

