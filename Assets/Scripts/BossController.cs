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

    public BossShot TheShot;
    public Transform[] ShotPoints;
    public Transform ShotCenter;
    public float TimeBetweenShots;
    public float ShotRotationSpeed;
    public float ShootingTime;

    private float _shotCounter;
    private float _shootingTimer;

    private EnemyHealthController _healthController;
    private int _maxHP;

    void Start()
    {
        _healthController = GetComponent<EnemyHealthController>();
        _spawnPoints = transform.parent.GetComponentsInChildren<SpawnLocation>().Select(x => x.transform).ToArray();
        _teleportTimer = 0f;
        _firstSpawn = true;
        _currentSpawnIndex = -1;
        _maxHP = _healthController.CurrentHP;
    }

    void Update()
    {
        _teleportTimer -= Time.deltaTime;
        _shootingTimer -= Time.deltaTime;

        if (_teleportTimer <= 0f)
        {
            _teleportTimer = this.TimeBetweenTeleports;
            Teleport();
            _shootingTimer = this.ShootingTime;
        }
        else if (_currentTarget != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _currentTarget,
                Time.deltaTime * this.MoveSpeed);

            _shotCounter -= Time.deltaTime;
            if ((_shotCounter <= 0f) && (_shootingTimer > 0f))
            {
                _shotCounter = this.TimeBetweenShots;

                int numShots = _healthController.CurrentHP > (_maxHP / 2) ? 4 : 8;

                for (int x = 0; x < numShots; x++)
                {
                    Instantiate(this.TheShot, this.ShotPoints[x].position, this.ShotPoints[x].rotation).SetDirection(this.ShotCenter.position);
                }
            }
        }

        if (_healthController.CurrentHP <= _maxHP / 4)
        {
            this.ShotCenter.transform.rotation =
                Quaternion.Euler(
                    this.ShotCenter.transform.rotation.eulerAngles.x,
                    this.ShotCenter.transform.rotation.eulerAngles.y,
                    this.ShotCenter.transform.rotation.eulerAngles.z + (this.ShotRotationSpeed * Time.deltaTime));
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
