using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public GameObject DeathAnimation;

    public int CurrentHP;

    public GameObject[] PickupPrefabs;

    public int PickupDropChance;

    private EnemyPosition _spawn;

    public GameObject Reward;

    void Start()
    {
        _spawn = GetComponentInParent<EnemyPosition>();
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
        AudioManager.Instance.PlaySFX(SFX.Death);

        if (this.PickupPrefabs?.Any() ?? false)
        {
            if (Global.Success(this.PickupDropChance))
            {
                Instantiate(this.PickupPrefabs.ChooseRandomElement().item, transform.position, Quaternion.identity);
            }
        }

        if (this.Reward != null)
        {
            this.Reward.SetActive(true);
        }

        if (_spawn != null)
        {
            Destroy(_spawn.gameObject);
        }

        Destroy(gameObject);
    }
}
