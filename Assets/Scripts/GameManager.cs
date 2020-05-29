using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool konecHry = true;
    public GameObject _playerPrefab;
    private UiManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }
    /*
    private void Update()
    {
        if (zacalo == false && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            zacalo = true;
            Destroy(GameObject.Find("Main_menu").gameObject);
            skore = 0;
        }
    }
   */
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (konecHry == true){
            if (Input.GetKeyDown(KeyCode.Space)){
                Instantiate(_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                konecHry = false;
                _uiManager.SkryjTitle();
            }
        }   
    }
}
