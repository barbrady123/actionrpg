using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public int Damage;

    public GameObject HitEffect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Enemy)
            return;

        obj.GetComponent<EnemyHealthController>().Damage(this.Damage);
        obj.GetComponent<EnemyController>().Knockback(obj.transform.position - transform.position);
        Instantiate(this.HitEffect, transform.position, transform.rotation);
    }
}
