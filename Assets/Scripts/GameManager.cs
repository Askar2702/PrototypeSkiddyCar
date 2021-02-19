using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameMode gameMode = GameMode.Easy;
    public GameState gameState = GameState.Start;


    public GameMode _gameMode => gameMode;
    public GameState _gameState => gameState;



    [SerializeField] CarController player;
    [SerializeField] Button restartGame;
    [SerializeField] Button ExitGame;

    string currentScene;
    public int turnChance { get; private set; }
    private void Awake()
    {
        if (!gameManager) gameManager = this;
        else return;
        if (PlayerPrefs.HasKey("lvl"))
        {
            if (PlayerPrefs.GetInt("lvl") == 2) gameMode = GameMode.Hard;
            else if (PlayerPrefs.GetInt("lvl") == 3) gameMode = GameMode.VeryHard;
        }
        else gameMode = GameMode.Easy;
        gameState = GameState.Start;
        DifficultySelection();
        player = GameObject.FindObjectOfType<CarController>();
    }
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        restartGame.onClick.AddListener(() => RestartGame());
        ExitGame.onClick.AddListener(() => LoadMenu());
    }
    private void Update()
    {
        if (player.transform.position.y <= -1f) 
        {
            gameState = GameState.Lose;
        }
    }
    private void DifficultySelection()
    {
        if (gameMode == GameMode.Easy) turnChance = 5;
        else if (gameMode == GameMode.Hard) turnChance = 3;
        else if (gameMode == GameMode.VeryHard) turnChance = 2;
    }

    private void StartGame()
    {
        gameState = GameState.Play;
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(currentScene);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("StartGame");
    }
    public void WinGame()
    {
        gameState = GameState.Win;
        if (gameMode == GameMode.Easy) PlayerPrefs.SetInt("lvl", 2);
        else if (gameMode == GameMode.Hard) PlayerPrefs.SetInt("lvl", 3);
        else if (gameMode == GameMode.VeryHard) PlayerPrefs.DeleteKey("lvl");
        RestartGame();
    }

    private void OnEnable()
    {
        FindObjectOfType<UIManager>()._event += StartGame;
    }
    
    public enum GameMode { Easy, Hard, VeryHard }
    public enum GameState { Start, Play, Win , Lose }
}
