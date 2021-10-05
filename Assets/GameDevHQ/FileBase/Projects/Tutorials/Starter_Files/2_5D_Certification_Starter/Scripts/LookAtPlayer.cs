using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").transform;
        if (_player == null)
        {
            Debug.LogError("Player is null in Look At Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.LookAt(_player);
    }
}
