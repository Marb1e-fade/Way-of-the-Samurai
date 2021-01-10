using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 350f;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = 0f;
    private Animator _animationController;
    private sightDirection _playerSightDirection = sightDirection.Right;
    private bool _isJumping = false;
    private float _horizontalMove = 0f;
    private float _walkSpeed = .8f;
    [SerializeField] private bool _onGround;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckRadius = .08f;
    [SerializeField] private LayerMask _groundLayer;
    private Rigidbody2D _playerRigidBody;
    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //_meleeAttackTimeStamp = Time.time;
        //_attackPoint = GameObject.Find("MeleeAttackPoint").transform;
        //_animationController = GetComponent<Animator>();

        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame 
    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _walkSpeed;
        if(Input.GetButtonDown("Jump")){
            _isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        move(_horizontalMove * Time.fixedDeltaTime);
        _isJumping = false;
        checkGround();
        //Debug.Log(_isJumping);
    }

    private void move(float moveby){
        Vector3 targetVelocity = new Vector2(_horizontalMove, _playerRigidBody.velocity.y);

        _playerRigidBody.velocity = Vector3.SmoothDamp(_playerRigidBody.velocity, targetVelocity, 
          ref _velocity, _movementSmoothing);
          //maybe add *sign(moveby) to movementSmoothing

        if (moveby > 0 && _playerSightDirection == sightDirection.Left || 
          moveby < 0 && _playerSightDirection == sightDirection.Right){
            Flip();
        }

        if (_onGround && _isJumping){
			_onGround = false;
            _playerRigidBody.AddForce(Vector2.up * _jumpForce);
            //_playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _jumpForce);
		}
    }
    private void Flip()
	{
        if(_playerSightDirection == sightDirection.Left){
		    _playerSightDirection = sightDirection.Right;
        }
        else{
            _playerSightDirection = sightDirection.Left;
        }

		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}

    private void checkGround(){
        _onGround = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _groundLayer);
    }
/*
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
    }*/
}
