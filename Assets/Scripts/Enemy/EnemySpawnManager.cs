using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    float _spawnRate = 3;
    [SerializeField]
    int _maxRoundEnemies, _maxCurrentEnemies = 30, _roundEnemiesSpawned = 0, _enemiesKilled = 0, _currentEnemiesAlive = 0;
    int round = 1;

    [SerializeField]
    Enemy[] enemyTypes;
    [SerializeField]
    GameObject[] spawnLocations;

    GameManager gManager;


    // Start is called before the first frame update
    void Start()
    {
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateMaxRoundEnemies();
        StartCoroutine(EnemySpawnRoutine());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void spawnEnemies()
    {

    }

    IEnumerator EnemySpawnRoutine()
    {

        while (_roundEnemiesSpawned < _maxRoundEnemies || _currentEnemiesAlive < _maxCurrentEnemies)
        {
            yield return new WaitForSeconds(_spawnRate);
            Enemy enemySpawn = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnLocations[Random.Range(0, 3)].transform.position, Quaternion.identity);
            enemySpawn.transform.parent = this.transform.Find("Enemies");
            _currentEnemiesAlive++;
            _roundEnemiesSpawned++;
        }
    }

    public void DecrementEnemiesAlive()
    {
        _currentEnemiesAlive--;
        _enemiesKilled++;
    }

    void UpdateMaxRoundEnemies()
    {
        _maxRoundEnemies = gManager.MaxRoundEnemies(round);
    }

    void TransitionNextLevel()
    {
        if (_enemiesKilled == _maxRoundEnemies)
        {
            round++;
            _enemiesKilled = 0;
            UpdateMaxRoundEnemies();
        }
    }
}
