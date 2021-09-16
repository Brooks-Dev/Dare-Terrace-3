using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _jumpHeight = 6f;
    private float _gravity = 9.8f;
    private Vector3 _playerVelocity;
    private CharacterController _controller;
    private float _zVelocity, _yVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Character contyroler on player is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.isGrounded == true)
        {
            _zVelocity = Input.GetAxis("Horizontal") * _speed;
            if (_yVelocity < 0f)
            {
                _yVelocity = 0f;
            }
            _yVelocity = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        _yVelocity -= _gravity * Time.deltaTime;
        _playerVelocity = new Vector3(0f, _yVelocity, _zVelocity);
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}
