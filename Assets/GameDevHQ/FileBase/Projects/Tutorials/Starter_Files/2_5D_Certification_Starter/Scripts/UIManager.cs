using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Toggle[] _toggles;
    [SerializeField]
    private GameObject[] _keyCards;
    [SerializeField]
    private Text _title, _gameOver, _levelComplete;
    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private GameObject _startButton;
    [SerializeField]
    private Text _buttonText;
    private Player _player;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null in UImanager");
        }
        _buttonText.text = "Start";
    }

    private void Start()
    {
        _title.enabled = true;
        _gameOver.enabled = false;
        _levelComplete.enabled = false;
        _startButton.SetActive(true);
        _panel.SetActive(false);
    }

    public void ToggleCard(int index)
    {
        if (GameManager.Instance.GameRunning == true)
        {
            _toggles[index].isOn = true; 
        }
        else
        {
            _toggles[index].isOn = false;
            _keyCards[index].SetActive(true);
        }
    }

    public void StartGame()
    {
        _title.enabled = false;
        _gameOver.enabled = false;
        _levelComplete.enabled = false;
        for (int i = 0; i < GameManager.Instance.HasCards.Length; i++)
        {
            GameManager.Instance.HasCards[i] = false;
            ToggleCard(i);
        }
        GameManager.Instance.GameOver = false;
        GameManager.Instance.GameRunning = true;
        _player.ActivatePlayer();
        _startButton.SetActive(false);
        _panel.SetActive(true);
        GetComponent<GraphicRaycaster>().enabled = false;

    }

    public void GameOverUI()
    {
        if (GameManager.Instance.GameOver == true) 
        {
            _gameOver.enabled = true;
        }
        else
        {
            _levelComplete.enabled = true;
        }
        GameManager.Instance.GameRunning = false;
        _buttonText.text = "Restart";
        _startButton.SetActive(true);
        _panel.SetActive(false);
        GetComponent<GraphicRaycaster>().enabled = true;
    }
}
