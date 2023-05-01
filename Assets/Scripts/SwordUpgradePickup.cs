using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordUpgradePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (!obj.tag.IsPlayerTag())
            return;

        var sword = PlayerController.Instance.transform.Find("SwordHolder").Find("Sword");
        var dmg = sword.GetComponent<DamageEnemy>();
        var sprite = sword.GetComponent<SpriteRenderer>();

        dmg.Damage += 5;
        sprite.sprite = GetComponent<SpriteRenderer>().sprite;
        AudioManager.Instance.PlaySFX(SFX.CoinPickup);
        Destroy(gameObject);
        DialogManager.Instance.ShowDialog(
            new[] { 
                $"{Global.Labels.None}|You got a sword upgrade!\n+5 Damage",
                $"{Global.Labels.None}|Time to return to the village..."
            },
            blockFirstClick: false);
    }
}
