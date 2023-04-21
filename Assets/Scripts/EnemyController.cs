using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;

    public float MoveSpeed;
    public float WaitTime;
    public float MoveTime;

    private float _timer;
    private bool _isMoving;

    private Vector2 _moveDirection;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _timer = this.WaitTime;
        _isMoving = false;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_isMoving)
            {
                Move();
            }
            else
            {
                _rigidBody.velocity = Vector2.zero;
            }
        }
        else
        {
            _isMoving = !_isMoving;
            _timer = _isMoving ? this.MoveTime : this.WaitTime;
            if (_isMoving)
            {
                _moveDirection = new Vector2(
                    Random.Range(-1.0f, 1.0f),
                    Random.Range(-1.0f, 1.0f));
            }
        }

        _animator.SetBool("isMoving", _isMoving);
    }

    private void Move()
    {
        _rigidBody.velocity = _moveDirection.normalized;
    }
}
