using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerups;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartSpawn(){
        StartCoroutine(PowerupSpawn());
        StartCoroutine(EnemySpawn());
    }
    IEnumerator PowerupSpawn(){
        while (_gameManager.konecHry == false)
        {
            Instantiate(_powerups[Random.Range(0, 2)], new Vector3(Random.Range(-8.14f, 8.14f), 5.9f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5.0f, 2.0f));
        }
    }
    IEnumerator EnemySpawn()
    {
        while (true)
        {
            if (_gameManager.konecHry == false){
                Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-8.4f, 8.4f), 5.9f, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(0.5f,2.5f));
        }
    }

}
