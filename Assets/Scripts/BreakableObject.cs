using System.Linq;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject[] PickupPrefabs;

    public int PickupDropChance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Break()
    {
        Destroy(gameObject);
        AudioManager.Instance.PlaySFX(SFX.Breaking);

        if (this.PickupPrefabs?.Any() ?? false)
        {
            if (Global.Success(this.PickupDropChance))
            {
                Instantiate(this.PickupPrefabs.ChooseRandomElement().item, transform.position, Quaternion.identity);
            }
        }
    }
}
