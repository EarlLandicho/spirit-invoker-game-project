﻿using UnityEngine;

public class MudGolem : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float stopDistance;

    private Rigidbody2D rigidBody;
    private Transform playerTranform;
    private bool canMove = true;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerTranform = GameObject.Find("Player").GetComponent<Transform>();
    }


    void Update()
    {
        if(rigidBody.velocity.y == 0)
        {
            if(playerTranform.position.x - transform.position.x > stopDistance + .1f && canMove)
            {
                rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            }
            else if(playerTranform.position.x - transform.position.x < -stopDistance - .1f && canMove)
            {
                rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }

        }
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }






}