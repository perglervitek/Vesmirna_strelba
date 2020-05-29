using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public Sprite[] zivotSprite;
    public Image zivotyImage;
    public Text skoreText;
    public int skore;
    public GameObject title;

    public void UpdateZivotu(int pocetZivotu) {
        zivotyImage.sprite = zivotSprite[pocetZivotu];
    }

    public void UpdateSkore() {
        skore += 10;
        skoreText.text = "Score: " + skore;
    }

    public void UkazTitle() {
        title.SetActive(true);
    }

    public void SkryjTitle(){
        title.SetActive(false);
        skore = 0;
        skoreText.text = "Score: " + skore;
    }
}
