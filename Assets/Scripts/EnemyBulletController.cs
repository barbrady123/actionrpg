using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float MoveSpeed;

    public int Damage;

    private Vector3 _moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        _moveDirection = (PlayerController.Instance.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _moveDirection * this.MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag.IsPlayerTag())
        {
            PlayerHealthController.Instance.Damage(this.Damage, false);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
