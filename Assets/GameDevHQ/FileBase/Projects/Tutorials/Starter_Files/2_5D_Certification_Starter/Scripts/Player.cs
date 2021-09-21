using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 9f;
    [SerializeField]
    private float _jumpHeight = 6f;
    private float _gravity = 9.8f;
    private Vector3 _playerVelocity;
    public float _zVelocity, _yVelocity;
    private CharacterController _controller;
    private Animator _anim;
    private bool _jumping;
    private Vector3 _ledgePos;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Character controller on player is null!");
        }
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator in Player is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.E) && _anim.GetBool("GrabLedge"))
        {
            _anim.SetBool("Climb", true);
            transform.Translate(new Vector3(0, 0.1f, -0.2f));
        }
        _yVelocity -= _gravity * Time.deltaTime;
        _anim.SetFloat("VertSpeed", _yVelocity);
        _playerVelocity = new Vector3(0f, _yVelocity, _zVelocity);
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
    
    private void CalculateMovement()
    {
        if (_controller.isGrounded == true)
        {
            if (_jumping == true || _anim.GetBool("Falling"))
            {
                _jumping = false;
                _anim.SetBool("Falling", false);
                _anim.SetBool("Jump", _jumping);
            }
            _zVelocity = Input.GetAxis("Horizontal") * _speed;
            if (_zVelocity != 0)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, _zVelocity));
            }
            _anim.SetFloat("Speed", Mathf.Abs(_zVelocity));
            if (_yVelocity < 0f)
            {
                _yVelocity = 0f;
            }
            _yVelocity = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jump", _jumping);
            }
        }
    }
    public void LedgeGrab(Vector3 ledge, Vector3 finalIdle)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetFloat("Speed", 0.0f);
        _anim.SetBool("Jump", false);
        _anim.SetBool("Falling", false);
        transform.position = ledge;
        _ledgePos = finalIdle;
    }

    public void ClimbLedge()
    {
        transform.position = _ledgePos;
        _anim.SetBool("GrabLedge", false); 
        _controller.enabled = true;
    }
}
