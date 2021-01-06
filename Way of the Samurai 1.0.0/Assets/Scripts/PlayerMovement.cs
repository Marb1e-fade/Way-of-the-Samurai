using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _playerSightDirection = sightDirection.Right;
        //_meleeAttackTimeStamp = Time.time;

        //_attackPoint = GameObject.Find("MeleeAttackPoint").transform;
        _animationController = GetComponent<Animator>();
        //_playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame 
    void Update()
    {
        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        transform.position += horizontal * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        DefinePlayerAction();
    }

    private void DefinePlayerAction()
    {
        // if (Input.GetKeyDown(KeyCode.W) && _isAbleToJump)
        // {
        //     Jump();
        //     AnimateJumping(_playerSightDirection);
        // }
        if (Input.GetKey(KeyCode.D))
        {
            Walk(sightDirection.Right);
            AnimateWalking(_playerSightDirection);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Walk(sightDirection.Left);
            AnimateWalking(_playerSightDirection);
        }
        // if (Input.GetKeyDown(KeyCode.Space) && _meleeAttackTimeStamp <= Time.time)
        // {
        //     _meleeAttackTimeStamp = Time.time + _meleeAttackCooldown;
        //     AttackMelee(_meleeAttackDamage);
        //     AnimateAttackingMelee(_playerSightDirection);
        // }
        if (!Input.anyKey)
        {
            Idle();
            AnimateIdling();
        }
    }

    public void Idle()
    {
    }

    public void Walk(sightDirection direction)
    {
        Vector3 targetVelocity = new Vector3(_walkSpeed * (int)direction, _playerRigidBody.velocity.y);
        _playerRigidBody.velocity = Vector3.SmoothDamp(_playerRigidBody.velocity, targetVelocity, ref _velocity, (int)direction * _movementSmoothing);
        _playerSightDirection = direction;
    }

    public void AnimateIdling()
    {
        _animationController.SetBool("Attack", false);
        _animationController.SetBool("Walk", false);
        _animationController.SetBool("Jump", false);
        _animationController.SetBool("InAir", false);
        _animationController.SetBool("Landing", false);
    }

    public void AnimateWalking(sightDirection direction)
    {
        RigidbodyTurning(direction);
        _animationController.SetBool("Walk", true);
    }
}
