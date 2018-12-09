using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject waitScreen;
    public Text scoreText;

    [Header("End game section")]
    public GameObject endGameScreen;
    public Text yourScoreText;
    public Text enemyScoreText;
    public Text winText;

    public void ToggleWaitScreen(bool flag)
    {
        waitScreen.SetActive(flag);
    }

    public void SetEndGameScreen(float yourScore, float enemyScore)
    {
        endGameScreen.SetActive(true);
        if (yourScore > enemyScore) SetWinText();
        else if (yourScore < enemyScore) SetLoseText();
        else SetDrawText();
        yourScoreText.text = "Your score: " + yourScore;
        enemyScoreText.text = "Enemy score: " + enemyScore;
    }

    public void SetWinText()
    {
        winText.text = "Вы победили!";
        winText.color = Color.green;
    }

    public void SetLoseText()
    {
        winText.text = "Вы Проиграли!";
        winText.color = Color.red;
    }

    public void SetDrawText()
    {
        winText.text = "Ничья!";
        winText.color = Color.yellow;
    }
}
