using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public int coinValue;
    public TextMeshProUGUI coinScore;
    public float timer, multiplier = 1, baseTimeMultiplier = 5f, multBase = 2f;

    void Start()
    {
        coinValue = 0;
        // if (PlayerPrefs.HasKey("Coins")){
        //     coinValue = PlayerPrefs.GetInt("Coins");
        // }

        coinScore.text = "" + coinValue;
    }
    public void GetCoin()
    {

        coinScore.text = " " + coinValue;
        coinValue += 1 * (int)multiplier;
        coinScore.text = " " + coinValue;
        PlayerPrefs.SetInt("Coins", coinValue);
    }

    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else if (timer <= 0 && multiplier > 1) multiplier = 1;
        coinScore.text = " " + coinValue;
    }

    public void PUPMult()
    {
        if (timer <= 0) timer += baseTimeMultiplier;
        else timer = baseTimeMultiplier;
        multiplier = multBase;
    }
    public void Pluscoin()
    {
        coinValue += 2;
    }
}
