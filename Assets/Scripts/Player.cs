using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool aktivovanStit = false;
    public int pocetZivotu = 3;
    public bool muzeTriple = false;
    public bool mamRychost = false;
    [SerializeField]
    private GameObject _raketka;
    [SerializeField]
    private GameObject _stitGrafika;
    [SerializeField]
    private GameObject _animaceExplozeSmrt;
    [SerializeField]
    private float _rychlost = 5F;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject _tripplePrefab;
    [SerializeField]
    private float _kadence = 0.33f;
    [SerializeField]
    private float nasobic = 2.0f;
    [SerializeField]
    private GameObject[] _motory;
    private float _muzeStrilet = 0.0F;

    private SpawnManager _spawnManager;
    private UiManager _uiManager;
    private GameManager _gameManager;
    private AudioSource _audioSource;
    private int pocetHitu = 0;
    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (_uiManager != null){
            _uiManager.UpdateZivotu(pocetZivotu);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
        //nastavení pozice na middle
        transform.position = new Vector3(0, 0, 0);
        _spawnManager.StartSpawn();
        pocetHitu = 0;
    }

    private void Update()
    {
        Pohyb();
        Strelba();
        ZobrazStit();
    }

    private void ZobrazStit(){
        if (aktivovanStit == true)
        {
            _stitGrafika.SetActive(true);
        }
        else{
            _stitGrafika.SetActive(false);
        }
    }
    private void Strelba(){
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _muzeStrilet)
        {
            _audioSource.Play();
            if (muzeTriple)
            {
                Instantiate(_tripplePrefab, transform.position, Quaternion.identity);
            }
            else{
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }
            _muzeStrilet = Time.time + _kadence;
        }
    }

    private void Pohyb() {
        float horizontalniVstup = Input.GetAxis("Horizontal");
        float vertikalniVstup = Input.GetAxis("Vertical");
        if (mamRychost) {
            if (vertikalniVstup > 0)
            {
                transform.Translate(Vector3.right * Time.deltaTime * horizontalniVstup * _rychlost * nasobic);
                transform.Translate(Vector3.up * Time.deltaTime * vertikalniVstup * _rychlost * nasobic);
                _raketka.SetActive(true);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * horizontalniVstup * _rychlost * nasobic);
                transform.Translate(Vector3.up * Time.deltaTime * vertikalniVstup * _rychlost * nasobic);
                _raketka.SetActive(false);
            }
            
        } else {
            if (vertikalniVstup > 0)
            {
                transform.Translate(Vector3.right * Time.deltaTime * horizontalniVstup * _rychlost);
                transform.Translate(Vector3.up * Time.deltaTime * vertikalniVstup * _rychlost);
                _raketka.SetActive(true);
            }
            else{
                transform.Translate(Vector3.right * Time.deltaTime * horizontalniVstup * _rychlost);
                transform.Translate(Vector3.up * Time.deltaTime * vertikalniVstup * _rychlost);
                _raketka.SetActive(false);
            }
        }
        

        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2F, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2F, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5F, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5F, transform.position.y, 0);
        }
    }

    public void Poskozeni(){
        if (aktivovanStit)
        {
            aktivovanStit = false;
        }
        else{
            pocetZivotu--;
            pocetHitu++;
            if (pocetHitu == 1)
            {
                _motory[0].SetActive(true);
            }
            else if (pocetHitu == 2){
                _motory[1].SetActive(true);
            }
            _uiManager.UpdateZivotu(pocetZivotu);
            if (pocetZivotu < 1)
            {
                Destroy(this.gameObject);
                Destroy(_stitGrafika.gameObject);
                Instantiate(_animaceExplozeSmrt, transform.position, Quaternion.identity);
                _gameManager.konecHry = true;
                _uiManager.UkazTitle();
            }
        }
    }

    public void TripleShotPowerupZac(){
        muzeTriple = true;
        StartCoroutine(TripleShotPowerupKonec());
    }
    public IEnumerator TripleShotPowerupKonec(){
        yield return new WaitForSeconds(5.0f);
        muzeTriple = false;
    }

    public void SpeedBoostPowerupZac(){
        mamRychost = true;
        StartCoroutine(SpeedBoostPowerupKonec());
    }

    public IEnumerator SpeedBoostPowerupKonec(){
        yield return new WaitForSeconds(5.0f);
        mamRychost = false;
    }
}
