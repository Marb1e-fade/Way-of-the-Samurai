using System.Collections;
using UnityEngine;
using static Loot;
public enum sightDirection { Left = -1, Right = 1 }

public class PlayerControls : MonoBehaviour, IMovable, IFightable, IAnimatable, IPlayerOnlyActions
{
    [Range(0, 4f)] [SerializeField] private float _jumpForce = 3.5f;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = 0f;

    [SerializeField] private Transform _attackPoint;

    [SerializeField] private LayerMask _enemyLayer;

    public uint CoinsCount { get; private set; }

    private const float _meleeAttackCooldown = 1.5f;
    private float _walkSpeed = 0.8f;
    private float _runSpeed = 2.5f;
    private float _meleeAttackTimeStamp;
    private float _meleeAttackRange = 0.32f;
    private float _meleeAttackDamage = 50f;

    private bool _isAbleToJump = false;
    private bool _isTurnedRight = true;

    private Animator _animationController;
    private Rigidbody2D _playerRigidBody;

    private Vector3 _velocity = Vector3.zero;

    private sightDirection _playerSightDirection;

    private void Start()
    {
        _playerSightDirection = sightDirection.Right;
        _meleeAttackTimeStamp = Time.time;

        _attackPoint = GameObject.Find("MeleeAttackPoint").transform;
        _animationController = GetComponent<Animator>();
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        DefinePlayerAction();
    }

    private void DefinePlayerAction()
    {
        if (Input.GetKeyDown(KeyCode.W) && _isAbleToJump)
        {
            Jump();
            AnimateJumping(_playerSightDirection);
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && _meleeAttackTimeStamp <= Time.time)
        {
            _meleeAttackTimeStamp = Time.time + _meleeAttackCooldown;
            AttackMelee(_meleeAttackDamage);
            AnimateAttackingMelee(_playerSightDirection);
        }
        if (!Input.anyKey)
        {
            Idle();
            AnimateIdling();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        switch(collider.tag)
        {
            case "Loot":
                if (collision.otherCollider.GetType() == typeof(CapsuleCollider2D))
                {
                    LootTaken(collider.gameObject.GetComponent<Loot>().GetLootType());
                    Destroy(collider.gameObject);
                }
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        switch (collider.tag)
        {
            case "Ground":
                _isAbleToJump = true;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        switch (collider.tag)
        {
            case "Ground":
                _isAbleToJump = false;
                AnimateJumping(_playerSightDirection);
                break;
        }
    }

    private void LootTaken(lootTypes lootType)
    {
        switch (lootType)
        {
            case lootTypes.Coin:
                CoinsCount++;
                break;
            case lootTypes.Weapon:

                break;
            default:
                throw new System.Exception(lootType.ToString());
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

    public void Jump()
    {
        _playerRigidBody.velocity = Vector2.up * _jumpForce;
    }

    public void AttackMelee(float damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _meleeAttackRange, _enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyUnit>().RecieveDamage(_meleeAttackDamage);
        }
    }

    public void AttackRanged(float range, float damage)
    {

    }

    public void RecieveDamage(float damage)
    {

    }

    public void DodgeEnemyAttack()
    {

    }

    public void Die()
    {
        Respawn();
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

    public void AnimateJumping(sightDirection direction)
    {
        RigidbodyTurning(direction);
    }

    public void AnimateJumpingInAir(sightDirection direction)
    {
        RigidbodyTurning(direction);
    }

    public void AnimateJumpingLanding(sightDirection direction)
    {
        RigidbodyTurning(direction);
    }

    public void AnimateAttackingMelee(sightDirection direction)
    {
        RigidbodyTurning(direction);
        _animationController.SetBool("Attack", true);
    }
    
    public void AnimateAttackingRanged(sightDirection direction)
    {
        
    }

    public void AnimateRunning(sightDirection direction)
    {
        
    }

    public void AnimateRecievingDamage(sightDirection direction)
    {

    }

    public void AnimateDodgingEnemyAttack(sightDirection direction)
    { 
        
    }

    public IEnumerator AnimateDeath()
    {
        return null;
    }

    public void RigidbodyTurning(sightDirection direction)
    {

        if (direction.Equals(sightDirection.Left) && _isTurnedRight || direction.Equals(sightDirection.Right) && _isTurnedRight == false)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            _isTurnedRight = !_isTurnedRight;
        }
    }

    public void ChangeWeapon()
    {
        throw new System.NotImplementedException();
    }

    public void UsePotion()
    {
        throw new System.NotImplementedException();
    }

    public void Respawn()
    {
        throw new System.NotImplementedException();
    }

    public enum playerStates { Idle, Walk, Run, Jump, Attack }
}
