﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    Animator animator;
    Rigidbody2D rigidbody2d;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask WhatIsGround;
    public float jumpHeight;
    public float moveSpeed;
    public bool grounded;
    public int maxJumps;
    private int jumps;
    public GameObject Axe;
    Quaternion turnRight;
    Quaternion turnLeft;
    float playerWidth;
    public float underPlatformsPosition = -6f;
    public Vector2 DefaultPlayerPosition;
    void Start() {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        SetDirections();
        jumps = maxJumps;
        playerWidth = GetComponent<SpriteRenderer>().size.x * transform.localScale.x;
        DefaultPlayerPosition = new Vector2(-7.25f, transform.localPosition.y);
    }
    void SetDirections()
    {
        turnRight = transform.rotation;
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        turnLeft = Quaternion.Euler(rot);
    }
    void FixedUpdate()
    {
        if (MainManager.GameManager.GameMode != Assets.GameModeEnum.GAME)
            return;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        if (rigidbody2d.velocity.magnitude < .01)
        {
            rigidbody2d.velocity = Vector3.zero;
        }
    }
    void GameOver()
    {
        MainManager.GameManager.ShowPlayerGravestone(gameObject.transform.localPosition);
        gameObject.SetActive(false);
        MainManager.CanvasManager.SetGameOverActive();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ckeckCollisionWithEnemies(collision))
        {
            GameOver();
        }
    }
    bool ckeckCollisionWithEnemies(Collision2D collision)
    {
        return
            collision.gameObject.CompareTag("Enemy")
            || collision.gameObject.CompareTag("AxeEnemy");
    }
    public void ResetRotationAndPosition()
    {
        transform.localPosition = DefaultPlayerPosition;
        transform.rotation = turnRight;
    }
    public void SetPlayerFreeze()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        if(animator != null)
            animator.enabled = false;
    }
    public void SetPlayerMoving()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        animator.enabled = true;
    }

    // Update is called once per frame
    void Update() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround) ;
        if (grounded)
        {
            jumps = maxJumps;
        }
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));
        animator.SetBool("Grounded", grounded);
        

        if (MainManager.GameManager.GameMode != Assets.GameModeEnum.GAME)
            return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            transform.rotation = turnLeft;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
        {
            rigidbody2d.velocity = new Vector2(moveSpeed, rigidbody2d.velocity.y);
            transform.rotation = turnRight;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Shoot();
        }
        if (transform.localPosition.y < underPlatformsPosition)
            GameOver();
        if (!Input.anyKey)
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x * 0.95f, rigidbody2d.velocity.y);
    }
    IEnumerator DecreaseJumpsAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        jumps --;
    }
    private void Shoot()
    {
        if (transform.rotation.eulerAngles.y == 0)
            Instantiate(Axe, new Vector3(transform.position.x + playerWidth, transform.position.y, 0), transform.rotation);
        else
            Instantiate(Axe, new Vector3(transform.position.x - playerWidth, transform.position.y, 0), transform.rotation);
    }

    private void Jump()
    {
        if (jumps > 0)
        {
            grounded = false;
            rigidbody2d.velocity = new Vector2(0, jumpHeight);
            StartCoroutine(DecreaseJumpsAfterDelay());
        }
        if (jumps == 0)
        {
            return;
        }
    }
}
