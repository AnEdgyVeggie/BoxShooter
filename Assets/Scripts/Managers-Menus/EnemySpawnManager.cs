using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    float _spawnRate = 1.75f;
    [SerializeField]
    int _maxRoundEnemies, _maxCurrentEnemies = 30, _roundEnemiesSpawned = 0,  _currentEnemiesAlive = 0, round = 1;
    [SerializeField]
    bool _spawning = false, _roundUpdate = false;

    [SerializeField]
    EnemyAI[] enemyTypes;
    [SerializeField]
    GameObject[] spawnLocations;
    [SerializeField]
    List<EnemyAI> enemies;

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

    IEnumerator CheckEnemySpawnRoutine()
    {
        yield return new WaitForSeconds(3.5f);
        if (_spawning == false)
        {
            StartCoroutine(EnemySpawnRoutine());
            StartCoroutine(CheckEnemySpawnRoutine());

            if (CheckForRoundUpdate() == true)
            {
                _roundUpdate = false;
                StartCoroutine(NextLevelSetUp());

            }
        }
        else
        {
            StartCoroutine(CheckEnemySpawnRoutine());
        }
    }

    IEnumerator EnemySpawnRoutine()
    {
        if (_roundEnemiesSpawned <= _maxRoundEnemies - 1)
        {
            _spawning = true;
            yield return new WaitForSeconds(_spawnRate);
            if (round > 5 && _roundEnemiesSpawned % 5 == 0)
            {
                EnemyAI enemySpawn = Instantiate(enemyTypes[1], spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
                enemySpawn.transform.parent = this.transform.Find("Enemies");
            }
            else { 
                EnemyAI enemySpawn = Instantiate(enemyTypes[0], 
                spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
                enemySpawn.name = "Zombie";
                enemies.Add(enemySpawn);
                enemySpawn.transform.parent = this.transform.Find("Enemies"); 
            }
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
        _roundUpdate = false;
        _roundEnemiesSpawned = 0;
        enemies.Clear();
        yield return new WaitForSeconds(3.25f);
        _anim.SetTrigger("RoundChange");
        yield return new WaitForSeconds(3f);
        round++;
        _uiManager.DisplayRound(round);
        yield return new WaitForSeconds(6.5f);

        UpdateMaxRoundEnemies();

        if (round % 5 == 0)
        {
            _maxCurrentEnemies++;
        }
    }
    bool CheckForRoundUpdate()
    {
        if (_roundEnemiesSpawned > 5 && CheckEnemiesDead())
        {
            Debug.Log("Updated CheckForRoundUpdate TRUE");
            return _roundUpdate = true;
        }
        Debug.Log("CheckForRoundUpdate FALSE");
        return _roundUpdate = false;
    }

    void UpdateMaxRoundEnemies()
    {
        _maxRoundEnemies = ((round * 3) * 100 / 40) + round;
      
       //used for short round testing :  
       // _maxRoundEnemies = ((((round + (1 + round)) * 100) / 40) + round);
    }


    bool CheckEnemiesDead()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                return false;
            }
        }
        return true;
    }
}
