using Assets;
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
        GameMode = GameModeEnum.GAME;
        ResetGame();
        MainManager.GameManager.IsKeyAchieved = false;
        MainManager.GameManager.AreDoorsAchieved = false;
        StartCoroutines();
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
        WinGame();
    }

    private void WinGame()
    {
        GameMode = GameModeEnum.WIN_GAME;
        MainManager.CanvasManager.SetWinGameActive();
    }

    private IEnumerator waitForKeyToGet()
    {
        yield return new WaitUntil(() => IsKeyAchieved);
        showDoors();
    }

    public void GoToMainMenu()
    {
        GameMode = GameModeEnum.MAIN_MENU;
        StopMovingObjects();
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
        GameMode = GameModeEnum.GAME_OVER;
        StopMovingObjects();
        MainManager.PlayerManager.GetComponent<PlayerManager>().ResetRotationAndPosition();
        MainManager.CanvasManager.SetGameOverActive();
    }

    public void PauseGame()
    {
        GameMode = GameModeEnum.PAUSE;
        StopMovingObjects();
    }

    private void StopMovingObjects()
    {
        MainManager.ElevatorManager.MoveSpeed = 0;
        MainManager.ElevatorManager.CanElevatorMove = false;
        foreach (var enemy in MainManager.MainEnemyManager.Enemies)
        {
            enemy.GetComponent<EnemyManager>().MoveSpeed = 0;
            enemy.GetComponent<Animator>().enabled = false;
        }
    }

    public void ResumeGame()
    {
        GameMode = GameModeEnum.GAME;
        startMovingObjects();
    }

    private void startMovingObjects()
    {
        MainManager.ElevatorManager.MoveSpeed = MainManager.ElevatorManager.DefaultElevatorSpeed;
        MainManager.ElevatorManager.CanElevatorMove = true;
        foreach (var enemy in MainManager.MainEnemyManager.Enemies)
        {
            enemy.GetComponent<EnemyManager>().MoveSpeed = MainManager.MainEnemyManager.DefaultEnemySpeed;
        }
    }
}
