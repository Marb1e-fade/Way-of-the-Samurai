using System.Collections;
using UnityEditor;
using UnityEngine;
using static IAnimatable;

public class EnemyUnit : MonoBehaviour, IMovable, IFightable, IAnimatable
{
    public string _unitName;
    public float _maxHealthPoints { get; private set; } = 100f;
    public float _currentHealthPoints { get; private set; } = 100f;
    
    private Vector3 _startingPosition;
    private Vector3 _position;
    private float _dyingTime;
    private bool _isAlive;

    private Rigidbody2D _unitRigidBody;
    private Renderer _renderer;
    private GameObject _player;
    [SerializeField] private GameObject _coinPrefab;

    private void Start()
    {
        _isAlive = true;

        _unitRigidBody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (_isAlive == false)
        {
            StartCoroutine(AnimateDeath());
        }
    }

    private void DropLoot()
    {
        int coinsAmount = Random.Range(3, 7);
        Vector2 unitPosition = gameObject.transform.position;

        for (int i = 0; i < coinsAmount; i++)
        {
            Instantiate(_coinPrefab, new Vector2(unitPosition.x + Random.Range(-0.20f, 0.20f), unitPosition.y), Quaternion.identity);
        }
    }

    public void Idle()
    {
        throw new System.NotImplementedException();
    }

    public void Walk(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }

    public void Jump()
    {
        throw new System.NotImplementedException();
    }

    public void AttackMelee(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void AttackRanged(float range, float damage)
    {
        throw new System.NotImplementedException();
    }

    public void RecieveDamage(float damage)
    {
        _currentHealthPoints -= damage;

        if (_currentHealthPoints <= 0f)
        {
            _isAlive = false;
        }
    }

    public void DodgeEnemyAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void AnimateIdling()
    {
        throw new System.NotImplementedException();
    }

    public void AnimateWalking(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }

    public void AnimateRunning(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }

    public void AnimateJumping(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }

    public void AnimateAttackingMelee(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }

    public void AnimateAttackingRanged(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }

    public void AnimateRecievingDamage(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator AnimateDeath()
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Color c = _renderer.material.color;
        for (float ft = 1f; ft >= 0f; ft -= 0.01f)
        {
            c.a = ft;
            _renderer.material.color = c;
            yield return null;
        }
        DropLoot();
        Die();
    }

    public void AnimateDodgingEnemyAttack(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }

    public void AnimateJumpingInAir(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }

    public void AnimateJumpingLanding(sightDirection direction)
    {
        throw new System.NotImplementedException();
    }
}
