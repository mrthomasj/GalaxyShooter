using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private UIManager _uiManager;

    public bool gameRunning = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameRunning)
        {
            _uiManager.StartGame();

            gameRunning = true;

            Instantiate(_playerPrefab);
        }
    }


}
