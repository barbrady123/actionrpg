using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    public float MoveSpeed;
    public float RotateSpeed;

    public int Damage;

    private Vector3 _moveDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = 
            Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y, 
                transform.rotation.eulerAngles.z + this.RotateSpeed * Time.deltaTime);

        transform.position += _moveDirection * this.MoveSpeed * Time.deltaTime;
    }

    public void SetDirection(Vector3 spawnerPosition)
    {
        _moveDirection = transform.position - spawnerPosition;
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
