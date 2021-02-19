using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event Action _event;
    [SerializeField] GameObject losePanel;
    [SerializeField] Text Timer;
    [SerializeField] Text LvlStage;
    [SerializeField] Text Gold;
    public int _gold { get; private set; }
    float timer = 5;
    void Start()
    {
        LvlStage.text = GameManager.gameManager._gameMode.ToString();
        if (PlayerPrefs.HasKey("Gold"))
            _gold = PlayerPrefs.GetInt("Gold");
        else _gold = 0;
        Gold.text = "Gold" + " " + _gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Timer.text = timer.ToString("f0");
            if (timer <= 0) 
            {
                Timer.enabled = false;
                _event?.Invoke();
            }

        }
        if(GameManager.gameManager._gameState == GameManager.GameState.Lose)
        {
            if (losePanel.activeSelf) return;
            losePanel.SetActive(true);
        }
    }

    public void GoldPlus()
    {
        _gold++;
        Gold.text = "Gold" + " " + _gold.ToString();
        PlayerPrefs.SetInt("Gold", _gold);
    }
}
