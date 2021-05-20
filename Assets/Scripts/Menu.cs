using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text _textGold;
    [SerializeField] private Button _startGame;
    [SerializeField] private Button _exitGame;
    void Start()
    {
        _startGame.onClick.AddListener(() => StartGame());
        _exitGame.onClick.AddListener(() => ExitGame());
        if (PlayerPrefs.HasKey("Gold"))
            _textGold.text += " " + PlayerPrefs.GetInt("Gold");
        else
            _textGold.text += " " + 0;
    }

    // Update is called once per frame
    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
