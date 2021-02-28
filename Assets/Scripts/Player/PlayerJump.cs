﻿using UnityEngine;
using EasyFeedback;

public class PlayerJump : MonoBehaviour, IJump
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;

    private float jumpHeightTemp;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Transform groundCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");

        jumpHeightTemp = jumpHeight;
    }

    private void FixedUpdate()
    {
        JumpAbleCheck();
    }

    private void Update()
    {
        
        if (!FeedbackForm.formIsOpened)
        {
            JumpCheck();
        }
    }

    public void SetLockJump(bool isLocked)
    {
        //this.isLocked = isLocked;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public void Jump()
    {
        rb.velocity = transform.up * jumpHeight;
    }

    public void SetJumpHeightByFactor(float factor)
    {
        jumpHeight *= factor;
    }

    public void SetJumpHeightToDefault()
    {
        jumpHeight = jumpHeightTemp;
    }

    private void JumpAbleCheck()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer) && rb.velocity.y <= 0.01f && !Input.GetKey(KeyCode.S))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(groundCheck.position, 0.05f);
    //}
}