using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class SpawnerN : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _swawnDuration = 1.0f;
    [SerializeField] private int _maxSpawnCount = 20;
    private int _spawnCount = 0;
    private Transform _target;
    private bool _isSpawning;

    public List<EnemyStatus> SpawnedEnemies { get; set; } = new List<EnemyStatus>();

    private void Start()
    {
        _isSpawning = false;
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            if (_spawnCount >= _maxSpawnCount)
            {
                yield return new WaitForSeconds(10f);
                continue;
            }
            else
            {
                var distanceVector = new Vector3(1, 0);
                var spawnPositionFromAround = Quaternion.Euler(0, Random.Range(0, 360), 0) * distanceVector;
                var spawnPosition = transform.position + spawnPositionFromAround;

                NavMeshHit hit;

                var enemyPrefabIndex = Random.Range(0, _enemyPrefabs.Length);

                if (NavMesh.SamplePosition(spawnPosition, out hit, 10f, NavMesh.AllAreas))
                {
                    var rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);

                    var enemy = Instantiate(_enemyPrefabs[enemyPrefabIndex], hit.position, rotation);
                    enemy.gameObject.name = _enemyPrefabs[enemyPrefabIndex].name + "_" + _spawnCount.ToString("00");
                    var enemyStatus = enemy.GetComponent<EnemyStatus>();
                    enemyStatus.gameObject.GetComponent<EnemyFollow>().player = _target;

                    enemyStatus.EnewmyDieEvent.AddListener(OnEnemyDefeated);

                    SpawnedEnemies.Add(enemyStatus);
                    _spawnCount++;
                    _isSpawning = true;
                }

                yield return new WaitForSeconds(_swawnDuration);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            if(SpawnedEnemies.Count == 0)
            {
                _target = other.transform;
                StartCoroutine(SpawnLoop());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            StopCoroutine(SpawnLoop());
        }
    }

    public void OnEnemyDefeated(EnemyStatus enemy)
    {
        if (SpawnedEnemies.Contains(enemy))
        {
            SpawnedEnemies.Remove(enemy);
            _spawnCount--;

            if (_spawnCount == 0) _isSpawning = false;
        }
    }
}
