using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private BoxCollider2D _area;

    public float MoveSpeed;
    public float WaitTime;
    public float MoveTime;

    private float _timer;
    private bool _isMoving;

    private Vector2 _moveDirection;

    public bool ShouldChase;
    private bool _isChasing;

    public float ChaseSpeed;
    public float RangeToChase;
    public float WaitAfterHitting;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _area = GetComponentInParent<BoxCollider2D>();
        _timer = this.WaitTime;
        _isMoving = false;
    }

    private void Update()
    {
        if (_isChasing)
        {
            Chase();
            return;
        }

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
            _timer += Random.Range(-1.0f, 1.0f);

            if (_isMoving)
            {
                _moveDirection = new Vector2(
                    Random.Range(-1.0f, 1.0f),
                    Random.Range(-1.0f, 1.0f)).normalized * this.MoveSpeed;
            }
        }

        _animator.SetBool("isMoving", _isMoving);
    }

    private void Chase()
    {
        _moveDirection = (PlayerController.Instance.transform.position - transform.position).normalized * this.ChaseSpeed;
        Move();
    }

    private void Move()
    {
        _rigidBody.velocity = _moveDirection;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, _area.bounds.min.x + 1f, _area.bounds.max.x - 1f),
            Mathf.Clamp(transform.position.y, _area.bounds.min.y + 1f, _area.bounds.max.y - 1f),
            transform.position.z);

        if (!this.ShouldChase)
            return;

        _isChasing = Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <= this.RangeToChase;
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag != Global.Tags.Player)
            return;

        if (!_isChasing)
            return;

        _isChasing = false;
        _isMoving = false;
        _timer = this.WaitAfterHitting;
        
        PlayerController.Instance.Knockback(_rigidBody.velocity);
    }
}
