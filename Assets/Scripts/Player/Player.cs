using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);

    public float speed;
    public float speedRun;

    public float jumpForce = 2;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float fallScaleY = 0.5f;
    public float jumpScaleX = 0.7f;
    public float fallScaleX = 2f;
    public float animationDuration = .2f;
    
    public Ease ease = Ease.OutBack;

    private float _currentSpeed;
    public bool _isOnFloor;

    private void Awake()
    {
        _isOnFloor = false;
    }

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    public void HandleMovement()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
        }
        else
        {
            _currentSpeed = speed;
        }

        if(Input.GetKey(KeyCode.A))
        {
            //myRigidbody.MovePosition(myRigidbody.position - velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
        }else if(Input.GetKey(KeyCode.D))
        {
            //myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
        }

        if(myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity += friction;
        }else if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity -= friction;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("floor"))
        {
            _isOnFloor = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            _isOnFloor = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("floor"))
        {
            _isOnFloor = false;
        }
    }

    public void HandleJump()
    {
        if(_isOnFloor)
        {
                if(Input.GetKeyDown(KeyCode.Space))
            {
                HandleScaleJump();
                myRigidbody.velocity = Vector2.up * jumpForce;
            }
        }
    }

    private void HandleScaleJump()
    {
        myRigidbody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
