using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is null!");
            }
            return _instance;
        }
    }

    public bool[] HasCards { get; set; }
    public bool GameOver { get; set; }
    public bool GameRunning { get; set; }

    private void Awake()
    {
        _instance = this;
        HasCards = new bool[3];
        GameRunning = false;
        GameOver = false;
    }
}
