using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Statistic : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI highScore, wheatCollected, gamesPlayed, highPlayingTime, canisterCollected, stoneCollected, wrenchCollected;
    // Start is called before the first frame update

    private void Start()
    {
        UpdateStatisticScore();
    }
    
    public void UpdateStatisticScore()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        wheatCollected.text = PlayerPrefs.GetInt("WheatCollected", 0).ToString();
        gamesPlayed.text = PlayerPrefs.GetInt("GamesPlayed", 0).ToString();
        highPlayingTime.text = PlayerPrefs.GetInt("Minutes", 0).ToString() + ":" +
                                                          PlayerPrefs.GetInt("Seconds", 0).ToString();
        canisterCollected.text = PlayerPrefs.GetInt("CanisterCollected", 0).ToString();
        wrenchCollected.text = PlayerPrefs.GetInt("HealCollected", 0).ToString();
        stoneCollected.text = PlayerPrefs.GetInt("StoneCollected", 0).ToString();
    }

    public void ResetStat()
    {
        PlayerPrefs.DeleteAll();
    }




}
