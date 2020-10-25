using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    GameObject MenuPanel;
    GameObject GamePanel;
    GameObject PausePanel;
    GameObject GameOverPanel;
    GameObject WinGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        getObjects();
        SetMenuActive();
    }

    private void getObjects()
    {
        MenuPanel = GameObject.Find("MenuPanel");
        GamePanel = GameObject.Find("GamePanel");
        PausePanel = GameObject.Find("PausePanel");
        GameOverPanel = GameObject.Find("GameOverPanel");
        WinGamePanel = GameObject.Find("WinGamePanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMenuActive()
    {
        MenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        WinGamePanel.SetActive(false);
        MainManager.GameManager.GoToMainMenu();
    }
    public void SetGameActive()
    {
        MenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        WinGamePanel.SetActive(false);
        if (MainManager.GameManager.GameMode == Assets.GameModeEnum.MAIN_MENU || MainManager.GameManager.GameMode == Assets.GameModeEnum.GAME_OVER)
        {
            MainManager.GameManager.StartGame();
        }
        else if(MainManager.GameManager.GameMode== Assets.GameModeEnum.PAUSE)
        {
            MainManager.GameManager.ResumeGame();
        }
    }
    public void SetPauseActive()
    {
        MenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        PausePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        WinGamePanel.SetActive(false);
        MainManager.GameManager.PauseGame();
    }
    public void SetGameOverActive()
    {
        MenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(true);
        WinGamePanel.SetActive(false);
    }
    public void SetWinGameActive()
    {
        MenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        WinGamePanel.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
