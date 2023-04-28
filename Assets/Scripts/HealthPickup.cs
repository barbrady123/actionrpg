using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int Health;

    public float DespawnTime = 10.0f;

    private float _waitTime = 0.5f;

    private void Start()
    {
        if (this.DespawnTime <= 0f)
            return;

        Destroy(gameObject, this.DespawnTime);
    }

    private void Update()
    {
        if (_waitTime > 0f)
        {
            _waitTime -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;

        if (_waitTime > 0f)
            return;

        AudioManager.Instance.PlaySFX(SFX.HealthPickup);
        PlayerHealthController.Instance.AddHealth(this.Health);
        Destroy(gameObject);
    }
}
