using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Manager;
    private GameMode _gameMode = GameMode.Easy;
    private GameState _gameState = GameState.Start;


    public GameMode DifficultyMode => _gameMode;
    public GameState CurrentStateOfTheGame => _gameState;



    [SerializeField] private CarController _player;
    [SerializeField] private Button _restartGame;
    [SerializeField]  private Button _exitGame;

    private string _currentScene;
    public int TurnChance { get; private set; }

    private void Awake()
    {
        if (!Manager) Manager = this;
        else return;
        if (PlayerPrefs.HasKey("lvl"))
        {
            if (PlayerPrefs.GetInt("lvl") == 2) _gameMode = GameMode.Hard;
            else if (PlayerPrefs.GetInt("lvl") == 3) _gameMode = GameMode.VeryHard;
        }
        else _gameMode = GameMode.Easy;
        _gameState = GameState.Start;
        DifficultySelection();
    }
    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().name;
        _restartGame.onClick.AddListener(() => RestartGame());
        _exitGame.onClick.AddListener(() => LoadMenu());
    }
    private void Update()
    {
        if (_player.transform.position.y <= -1f) 
        {
            _gameState = GameState.Lose;
        }
    }
    private void DifficultySelection()
    {
        if (_gameMode == GameMode.Easy) TurnChance = 5;
        else if (_gameMode == GameMode.Hard) TurnChance = 3;
        else if (_gameMode == GameMode.VeryHard) TurnChance = 2;
    }

    private void StartGame()
    {
        _gameState = GameState.Play;
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(_currentScene);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("StartGame");
    }
    public void WinGame()
    {
        _gameState = GameState.Win;
        if (_gameMode == GameMode.Easy) PlayerPrefs.SetInt("lvl", 2);
        else if (_gameMode == GameMode.Hard) PlayerPrefs.SetInt("lvl", 3);
        else if (_gameMode == GameMode.VeryHard) PlayerPrefs.DeleteKey("lvl");
        RestartGame();
    }

    private void OnEnable()
    {
        FindObjectOfType<UIManager>().StartGame += StartGame;
    }
    
    public enum GameMode { Easy, Hard, VeryHard }
    public enum GameState { Start, Play, Win , Lose }
}
