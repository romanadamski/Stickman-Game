﻿using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameModeEnum GameMode;
    public GameObject Key;
    public GameObject Doors;
    public bool IsKeyAchieved;
    public bool AreDoorsAchieved;
    // Start is called before the first frame update
    void Start()
    {
        setObjects();
    }

    private void setObjects()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        ResetGame();
        MainManager.GameManager.IsKeyAchieved = false;
        AreDoorsAchieved = false;
        AreDoorsAchieved = false;
        MainManager.CanvasManager.SetItemsOnScreen();
        StartCoroutines();
        GameMode = GameModeEnum.GAME;
    }

    private void StartCoroutines()
    {
        StartCoroutine(waitForAllEnemiesKilled());
        StartCoroutine(waitForKeyToGet());
        StartCoroutine(waitForDoorsToGet());
    }
    void showKey()
    {
        Key.SetActive(true);
    }
    IEnumerator waitForAllEnemiesKilled()
    {
        yield return new WaitUntil(() => MainManager.MainEnemyManager.Enemies.Where(x => x.activeSelf == true).Count() == 0);
        showKey();
    }
    void showDoors()
    {
        Doors.SetActive(true);
    }

    private IEnumerator waitForDoorsToGet()
    {
        yield return new WaitUntil(() => AreDoorsAchieved);
        MainManager.CanvasManager.SetWinGameActive();
    }

    public void WinGame()
    {
        StopMovingObjects();
        GameMode = GameModeEnum.WIN_GAME;
    }

    private IEnumerator waitForKeyToGet()
    {
        yield return new WaitUntil(() => IsKeyAchieved);
        showDoors();
        MainManager.CanvasManager.KeyImage.SetActive(true);
    }

    public void GoToMainMenu()
    {
        StopMovingObjects();
        GameMode = GameModeEnum.MAIN_MENU;
    }
    public void ResetGame()
    {
        setObjectsVisiblity();
        setDefaultPositionsForObjects();
        MainManager.MainEnemyManager.IgnoreEnemies();
        startMovingObjects();
    }

    private void setObjectsVisiblity()
    {
        foreach (var enemy in MainManager.MainEnemyManager.Enemies)
        {
            enemy.SetActive(true);
        }
        Key.gameObject.SetActive(false);
        Doors.gameObject.SetActive(false);
    }

    private void setDefaultPositionsForObjects()
    {
        MainManager.PlayerManager.GetComponent<PlayerManager>().ResetRotationAndPosition();
        MainManager.ElevatorManager.transform.localPosition = MainManager.ElevatorManager.DefaultElevatorPosition;
        foreach (var enemy in MainManager.MainEnemyManager.Enemies)
        {
            enemy.GetComponent<EnemyManager>().ResetRotationAndPosition();
            enemy.GetComponent<Animator>().enabled = true;
        }
    }

    internal void GameOver()
    {
        StopMovingObjects();
        MainManager.PlayerManager.SetPlayerFreeze();
        GameMode = GameModeEnum.GAME_OVER;
    }

    public void PauseGame()
    {
        StopMovingObjects();
        GameMode = GameModeEnum.PAUSE;
    }

    private void StopMovingObjects()
    {
        MainManager.ElevatorManager.MoveSpeed = 0;
        MainManager.ElevatorManager.CanElevatorMove = false;
        MainManager.PlayerManager.SetPlayerFreeze();
        foreach (var enemy in MainManager.MainEnemyManager.Enemies)
        {
            enemy.GetComponent<EnemyManager>().MoveSpeed = 0;
            enemy.GetComponent<Animator>().enabled = false;
        }
    }

    public void ResumeGame()
    {
        startMovingObjects();
        GameMode = GameModeEnum.GAME;
    }

    private void startMovingObjects()
    {
        MainManager.PlayerManager.SetPlayerMoving();
        MainManager.ElevatorManager.MoveSpeed = MainManager.ElevatorManager.DefaultElevatorSpeed;
        MainManager.ElevatorManager.CanElevatorMove = true;
        foreach (var enemy in MainManager.MainEnemyManager.Enemies)
        {
            enemy.GetComponent<EnemyManager>().MoveSpeed = MainManager.MainEnemyManager.DefaultEnemySpeed;
            enemy.GetComponent<Animator>().enabled = true;
        }
    }
}
