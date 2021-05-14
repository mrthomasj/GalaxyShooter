using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private int _currentScore;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _menu;
    


    public void UpdateLives(int reference)
    {
        Image livesUI = GameObject.Find("Lives").GetComponent<Image>();
        livesUI.sprite = _livesSprites[reference];
    }

    public void UpdateScore()
    {
        _currentScore += 10;
        _scoreText.text = $"{_currentScore}";
    }

    public void StartGame()
    {

        _currentScore = 0;
        _scoreText.text = "0";

        _menu.SetActive(false);

       
    }

    public bool GameOver(bool gameRunning)
    {
        
        _menu.SetActive(true);

        return gameRunning = false;

    }

}
