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

    private void SpawnHitEffect() => Instantiate(this.HitEffect, transform.position, transform.rotation);

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == Global.Tags.Enemy)
        {
            obj.GetComponent<EnemyHealthController>().Damage(this.Damage);
            obj.GetComponent<EnemyController>().Knockback(obj.transform.position - transform.position);
            SpawnHitEffect();
        }
        else if (obj.tag == Global.Tags.Breakable)
        {
            obj.GetComponent<BreakableObject>().Break();
            SpawnHitEffect();
        }
    }
}
