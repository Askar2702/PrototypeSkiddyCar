using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Text textGold;
    [SerializeField] Button startGame;
    [SerializeField] Button exitGame;
    void Start()
    {
        startGame.onClick.AddListener(() => StartGame());
        exitGame.onClick.AddListener(() => ExitGame());
        if (PlayerPrefs.HasKey("Gold"))
            textGold.text += " " + PlayerPrefs.GetInt("Gold");
        else
            textGold.text += " " + 0;
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
