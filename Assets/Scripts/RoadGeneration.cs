using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadGeneration : MonoBehaviour
{
    [SerializeField] private GameObject _easyRoad;
    [SerializeField] private GameObject _veryHardRoad;
    [SerializeField] private GameObject _hardRoad;
    [SerializeField]  private int _countSpawnRoad;
    [SerializeField] private Material[] _roadMat;
    public List<Transform> Cell = new List<Transform>();
    [SerializeField] private int _step = 30;
    [SerializeField] private Material _finishMat;
    int turnChance = 0;
    MeshRenderer Mesh;
    Vector3 MeshSize;
    Vector3 position;
    GameObject _road;
    GameManager gameManager;
    GameObject road;
    void Start()
    {
        gameManager = GameManager.Manager;
        turnChance = gameManager.TurnChance;
        ChoiceOfRoad();
        GenerationCell();
    }
    private void GenerationCell()
    {
        if (_countSpawnRoad > 0)
        {
            while (_step > 0) {
                int rand = Random.Range(turnChance, _countSpawnRoad);
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
                    _step--;
                    if (i >= rand - 1)
                    {
                        int rand2 = Random.Range(turnChance, _countSpawnRoad);
                        for (int j = 0; j < rand2; j++)
                        {
                            position = new Vector3(Cell.LastOrDefault().transform.localPosition.x + 1 * MeshSize.x, 0f, Cell.LastOrDefault().transform.localPosition.z);
                            _road = Instantiate(road, position, Quaternion.identity);
                            _road.transform.parent = transform;
                            Cell.Add(_road.transform);
                            _step--;
                        }
                    }
                }
            }
            ChangeMatCell();
        }
    }

    private void ChangeMatCell()
    {
        var _mat = _roadMat[Random.Range(0, _roadMat.Length)];
        foreach (var mat in Cell)
            mat.GetComponent<Renderer>().material = _mat;
        var lastCell = Cell.LastOrDefault();
        lastCell.GetComponent<Renderer>().material = _finishMat;
        lastCell.gameObject.AddComponent<Finish>();
    }
    private void ChoiceOfRoad()
   {
        if(gameManager.DifficultyMode == GameManager.GameMode.VeryHard)
        {
            road = _veryHardRoad;
            _step = 70;
        }
        else if (gameManager.DifficultyMode == GameManager.GameMode.Hard)
        {
            road = _hardRoad;
            _step = 50;
        }
        else
        {
            road = _easyRoad;
        }
   }


}

