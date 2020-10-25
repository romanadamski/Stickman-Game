using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public GameObject StartElevatorPosition;
    public GameObject EndElevatoryPosition;
    int direction = 1;
    public float MoveSpeed;
    Rigidbody2D rigidbody2d;
    int elevatorStopTime = 2;
    public bool CanElevatorMove = true;
    public float DefaultElevatorSpeed = 5;
    public Vector2 DefaultElevatorPosition;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        DefaultElevatorPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, MoveSpeed * direction);
        if (transform.localPosition.y <= StartElevatorPosition.transform.position.y && CanElevatorMove)
        {
            direction = 1;
            StartCoroutine(stopTheElevatorForSeconds());
        }
        else if (transform.localPosition.y >= EndElevatoryPosition.transform.position.y && CanElevatorMove)
        {
            direction = -1;
            StartCoroutine(stopTheElevatorForSeconds());
        }
    }
    IEnumerator stopTheElevatorForSeconds()
    {
        MoveSpeed = 0;
        CanElevatorMove = false;
        yield return new WaitForSeconds(elevatorStopTime);
        yield return new WaitUntil(() => MainManager.GameManager.GameMode == Assets.GameModeEnum.GAME);
        MoveSpeed = DefaultElevatorSpeed;
        yield return new WaitForSeconds(0.1f);
        //yield return new WaitUntil(() => MainManager.GameManager.GameMode == Assets.GameModeEnum.GAME);
        CanElevatorMove = true;
    }
}
