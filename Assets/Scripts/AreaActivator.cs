using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaActivator : MonoBehaviour
{
    private BoxCollider2D _areaBox;

    private EnemyPosition[] _enemyPositions;
    private List<GameObject> _enemies;

    public bool LockDoors;

    private bool _doorsActivated;
    private bool _enemiesSpawned;

    private List<Door> _doors = new List<Door>();

    public CameraMode CustomCameraMode = CameraMode.None;
    public GameObject CustomCameraPointMin = null;
    public GameObject CustomCameraPointMax = null;

    // Start is called before the first frame update
    void Start()
    {
        _areaBox = GetComponent<BoxCollider2D>();
        _enemyPositions = GetComponentsInChildren<EnemyPosition>();
        _enemies = new List<GameObject>(_enemyPositions.Length);
        _doorsActivated = false;
        _enemiesSpawned = false;

        if (this.LockDoors && _enemyPositions.Any())
        {
            var doors = transform.Find("Doors");
            if (doors != null)
            {
                foreach (var door in doors.GetComponentsInChildren<Door>(true))
                {
                    _doors.Add(door);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_doors.Any() || !_enemiesSpawned)
            return;

        print("update with doors and enemies spawned");

        if (!_doorsActivated)
        {
            print("about to activate doors");
            ActivateDoors();
            return;
        }

        print("gonna check to see if all enemies are dead");
        if (_enemies.All(x => x == null))
        {
            print("all enemies dead, deactivating doors");
            DeactivateDoors();
        }
    }

    private void ActivateDoors()
    {
        print("activate doors");
        if (_doors.Any(x => x.Collider.DistanceEx(PlayerController.Instance.Collider) < 0.8f))
            return;

        _doorsActivated = true;
        _doors.ForEach(x => x.gameObject.SetActive(true));
        print("doors activated");
    }

    private void DeactivateDoors()
    {
        print("deactivate doors");
        _doors.ForEach(x => x.gameObject.SetActive(false));
        // _doors.ForEach(x => Destroy(x));
        // _doors.Clear();
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;
        print($"trigger enter area: {name}");

        SpawnEnemies();

        CustomCameraController.Instance.EnteringArea(
            _areaBox,
            this.CustomCameraMode,
            this.CustomCameraPointMin != null ? this.CustomCameraPointMin.transform.position : null,
            this.CustomCameraPointMax != null ? this.CustomCameraPointMax.transform.position : null);
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;
        print($"trigger exit area: {name}");

        // If the player died, just let the mobs hang out...
        if (PlayerHealthController.Instance.CurrentHP <= 0)
            return;

        CustomCameraController.Instance.SwitchArea();
        DespawnEnemies();
    }

    private void DespawnEnemies()
    {
        if (_enemies.Any())
        {
            print("despawn enemies");
        }

        _enemies.ForEach(x => Destroy(x.gameObject));
        _enemies.Clear();
    }

    private void SpawnEnemies()
    {
        _enemies.Clear();

        _enemyPositions = _enemyPositions.Where(x => x != null).ToArray();

        if (_enemyPositions.Any())
        {
            print("spawn enemies");
        }

        foreach (var enemyPos in _enemyPositions)
        {
            var enemy = Instantiate(
                enemyPos.EnemyPrefab,
                enemyPos.transform.position,
                Quaternion.identity);

            enemy.transform.parent = enemyPos.transform;

            _enemies.Add(enemy);
            print("enemy added");
            _enemiesSpawned = true;
            print("enemiesSpawned = true");
        }
    }
}
