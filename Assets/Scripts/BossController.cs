using System.Linq;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform[] _spawnPoints;

    public float TimeBetweenTeleports = 5f;
    public float TimeBeforeFirstTeleport = 10f;

    public int InitialSpawnPointIndex = 2;

    private float _teleportTimer;

    private bool _firstSpawn;

    private int _currentSpawnIndex;

    private Vector3 _currentTarget;

    public float MoveSpeed = 3f;

    void Start()
    {
        _spawnPoints = transform.parent.GetComponentsInChildren<SpawnLocation>().Select(x => x.transform).ToArray();
        _teleportTimer = 0f;
        _firstSpawn = true;
        _currentSpawnIndex = -1;
    }

    void Update()
    {
        _teleportTimer -= Time.deltaTime;

        if (_teleportTimer <= 0f)
        {
            _teleportTimer = this.TimeBetweenTeleports;
            Teleport();
        }
        else if (_currentTarget != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _currentTarget,
                Time.deltaTime * this.MoveSpeed);
        }
    }

    private void Teleport()
    {
        if (_firstSpawn)
        {
            _firstSpawn = false;
            transform.position = _spawnPoints[this.InitialSpawnPointIndex].position;
            _teleportTimer = this.TimeBeforeFirstTeleport;
            _currentTarget = Vector3.zero;
            return;
        }

        (var spawn, int selectedIndex) = _spawnPoints.ChooseRandomElement(_currentSpawnIndex);
        transform.position = spawn.position;
        _currentSpawnIndex = selectedIndex;
        AudioManager.Instance.PlaySFX(SFX.Equip);

        (var target, int targetIndex) = _spawnPoints.ChooseRandomElement(_currentSpawnIndex);
        _currentTarget = target.position;
        _currentSpawnIndex = targetIndex;
    }
}
