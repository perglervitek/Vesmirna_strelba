using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerupka : MonoBehaviour
{
    [SerializeField]
    private float _rychlost = 3.0f;
    [SerializeField]
    private int powerupID; //0 = triple 1= speed boost, 2 = stit
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _rychlost);
        if (transform.position.y < -7){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.tag == "Player"){
            //pristup k player
            Player player = col.GetComponent<Player>();
            //povoleni triple
            if (player != null){
                if (powerupID == 0) {
                    player.TripleShotPowerupZac();
                } else if (powerupID == 1) {
                    player.SpeedBoostPowerupZac();
                } else if(powerupID == 2){
                    player.aktivovanStit = true;
                }
            }
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            //zniceni powerupky
            Destroy(this.gameObject);
        }
    }
}
