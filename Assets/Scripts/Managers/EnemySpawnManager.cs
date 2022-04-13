using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    float _spawnRate = 2.5f;
    [SerializeField]
    int _maxRoundEnemies, _maxCurrentEnemies = 30, _roundEnemiesSpawned = 0, _enemiesKilled = 0, _currentEnemiesAlive = 0, round = 1;
    [SerializeField]
    bool _spawning = false;

    [SerializeField]
    Enemy[] enemyTypes;
    [SerializeField]
    GameObject[] spawnLocations;

    Animator _anim;
    UIManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _anim = GameObject.Find("Round").GetComponent<Animator>();

        UpdateMaxRoundEnemies();
        StartCoroutine(CheckEnemySpawnRoutine());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    IEnumerator CheckEnemySpawnRoutine()
    {
        yield return new WaitForSeconds(3.5f);
        if (_spawning == false)
        {
            StartCoroutine(EnemySpawnRoutine());
            StartCoroutine(CheckEnemySpawnRoutine());
            Debug.LogWarning("SPAWNING ENEMIES");

            if (_enemiesKilled == _maxRoundEnemies)
            {
                StartCoroutine(NextLevelSetUp());
                
            }
        }
        else
        {
            Debug.LogWarning("CHECKING TO SPAWN ENEMIES");
            StartCoroutine(CheckEnemySpawnRoutine());
        }
    }

    IEnumerator EnemySpawnRoutine()
    {
        if (_roundEnemiesSpawned < _maxRoundEnemies && _currentEnemiesAlive < _maxCurrentEnemies)
        {
            _spawning = true;
            yield return new WaitForSeconds(_spawnRate);
            Enemy enemySpawn = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
            enemySpawn.transform.parent = this.transform.Find("Enemies");
            _currentEnemiesAlive++;
            _roundEnemiesSpawned++;
            StartCoroutine(EnemySpawnRoutine());
        }
        else
        {
            _spawning = false;
        }
    }

    IEnumerator NextLevelSetUp()
    {
        _enemiesKilled = 0;
        yield return new WaitForSeconds(3.25f);
        _anim.SetTrigger("RoundChange");
        yield return new WaitForSeconds(3f);
        round++;
        _uiManager.DisplayRound(round);
        yield return new WaitForSeconds(6.5f);
        _currentEnemiesAlive = 0;
        _roundEnemiesSpawned = 0;
        UpdateMaxRoundEnemies();


        if (round % 5 == 0)
        {
            _maxCurrentEnemies++;
        }
    }

    public void DecrementEnemiesAlive()
    {
        _currentEnemiesAlive--;
        _enemiesKilled++;
    }

    void UpdateMaxRoundEnemies()
    {
        _maxRoundEnemies = ((((round + (10 + round)) * 100) / 40) + round);
    }



}
