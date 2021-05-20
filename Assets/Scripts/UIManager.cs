using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event Action StartGame;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Text _timer;
    [SerializeField] private Text _lvlStage;
    [SerializeField]  private Text _gold;
    public int Gold { get; private set; }
    private float timer = 5;
    void Start()
    {
        _lvlStage.text = GameManager.Manager.DifficultyMode.ToString();
        if (PlayerPrefs.HasKey("Gold"))
            Gold = PlayerPrefs.GetInt("Gold");
        else Gold = 0;
        _gold.text = "Gold" + " " + Gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            _timer.text = timer.ToString("f0");
            if (timer <= 0) 
            {
                _timer.enabled = false;
                StartGame?.Invoke();
            }

        }
        if(GameManager.Manager.CurrentStateOfTheGame == GameManager.GameState.Lose)
        {
            if (_losePanel.activeSelf) return;
            _losePanel.SetActive(true);
        }
    }

    public void GoldPlus()
    {
        Gold++;
        _gold.text = "Gold" + " " + Gold.ToString();
        PlayerPrefs.SetInt("Gold", Gold);
    }
}
