using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected float _speed = 3, _rotateSpeed = 5;
    protected Player _player;
    [SerializeField]
    protected bool _atEnviroment = false, _atPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is null in Enemy Script");

        Init();
    }

    public virtual void Init()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        MoveEnemy();

    }

    void MoveEnemy()
    {
        if (!_atEnviroment)
        {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }

    }
    void MoveEnemyFromEnviroment(Vector3 enviromentPosition)
    {
        Debug.LogWarning(this.name + "is Moving from enviroment");

        float velocity = _speed * Time.deltaTime;

        if (_atEnviroment)
        {
            if (enviromentPosition.x < this.transform.position.x)
            {
                transform.Translate(Vector3.right * velocity);
            }
            if (enviromentPosition.x > this.transform.position.x)
            {
                transform.Translate(Vector3.left * velocity);
            }
            if (enviromentPosition.y < this.transform.position.y)
            {
                transform.Translate(Vector3.up * velocity);
            }
            if (enviromentPosition.y > this.transform.position.y)
            {
                transform.Translate(Vector3.down * velocity);
            }
        }
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enviroment")
        {
            Debug.LogWarning(this.name + "Collided with enviroment");
            _atEnviroment = true;
            Vector3 envPos = other.transform.position;
            MoveEnemyFromEnviroment(envPos);
        }
        if (other.tag == "Player")
        {
            _atPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enviroment")
        {
            Debug.LogWarning(this.name + "exited enviroment");
            _atEnviroment = false;

        }
    }
}
