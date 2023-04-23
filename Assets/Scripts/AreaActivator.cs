using System.Collections.Generic;
using UnityEngine;

public class AreaActivator : MonoBehaviour
{
    private BoxCollider2D _areaBox;

    private EnemyPosition[] _enemyPositions;
    private List<GameObject> _enemies;

    // Start is called before the first frame update
    void Start()
    {
        _areaBox = GetComponent<BoxCollider2D>();
        _enemyPositions = GetComponentsInChildren<EnemyPosition>();
        _enemies = new List<GameObject>(_enemyPositions.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;

        SpawnEnemies();
        Camera.main.GetComponent<CameraController>().AreaBox = _areaBox;
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;

        // If the player died, just let the mobs hang out...
        if (PlayerHealthController.Instance.CurrentHP <= 0)
            return;

        DespawnEnemies();
    }

    private void DespawnEnemies()
    {
        _enemies.ForEach(x => Destroy(x.gameObject));
        _enemies.Clear();
    }

    private void SpawnEnemies()
    {
        _enemies.Clear();
        
        foreach (var enemyPos in _enemyPositions)
        {
            var enemy = Instantiate(
                enemyPos.EnemyPrefab,
                enemyPos.transform.position,
                Quaternion.identity);
            
            enemy.transform.parent = enemyPos.transform;

            _enemies.Add(enemy);
                
        }
    }
}
