using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _animaceExplozeEnemy;
    [SerializeField]
    private float _rychlost = 5F;
    [SerializeField]
    private AudioClip _clip;
    private UiManager _uiManager;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _rychlost);
        if (transform.position.y < -6){
            transform.position = new Vector3(Random.Range(-8.14f, 8.14f), 5.9f, 0);
            
        }
        if (_gameManager.konecHry == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            Player player = col.GetComponent<Player>();
            if (player != null){
                player.Poskozeni();
            }
            Destroy(this.gameObject);
            _uiManager.UpdateSkore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Instantiate(_animaceExplozeEnemy, transform.position, Quaternion.identity);
        }
        else if(col.tag == "Laser"){
            if (col.transform.parent != null){
                Destroy(col.transform.parent.gameObject);
            }
            Instantiate(_animaceExplozeEnemy, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            _uiManager.UpdateSkore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
