using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public GameObject DeathAnimation;

    public int CurrentHP;

    void Start()
    {
        
    }

    private void Update()
    {
        if (this.CurrentHP <= 0)
        {
            Dead();
        }
    }

    public int Damage(int dmg)
    {
        int actualDmg = (this.CurrentHP >= dmg) ? dmg : this.CurrentHP;
        this.CurrentHP -= actualDmg;
        print($"Enemy {gameObject.name} took {actualDmg} damage...");
        return actualDmg;
    }

    private void Dead()
    {
        Instantiate(this.DeathAnimation, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
