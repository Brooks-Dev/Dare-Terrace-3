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
    private Vector3 _ledgePos;
    [SerializeField]
    private bool _jumpingIdle, _jumpingRunning;
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private float _distToGround;
    [SerializeField]
    private bool _isFalling;
    [SerializeField]
    private bool _grabbingLedge;
    
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
        _distToGround = _controller.bounds.extents.y + 0.53f;
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _distToGround);
        CalculateMovement(); 
        if (Input.GetKeyDown(KeyCode.E) && _grabbingLedge == true)
        {
            _anim.SetBool("Climb", true);
            transform.Translate(new Vector3(0, 0.1f, -0.2f));
        }
    }

    private void CalculateMovement()
    {
        if (_grabbingLedge == true)
        {
            _jumpingRunning = false;
            _anim.SetBool("Jump", _jumpingRunning);
        }
        else
        {
            if (_isGrounded == true)
            {
                if (_jumpingRunning == true)
                {
                    _jumpingRunning = false;
                    _anim.SetBool("Jump", _jumpingRunning);
                }
                if (_isFalling == true)
                {
                    _anim.SetBool("Falling", false);
                    _isFalling = false;
                }
                if (_jumpingIdle == false && _jumpingRunning == false && _isFalling == false)
                {
                    _zVelocity = Input.GetAxis("Horizontal") * _speed;
                }
                if (_zVelocity != 0)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, _zVelocity));
                }
                _anim.SetFloat("Speed", Mathf.Abs(_zVelocity));
                if (_yVelocity < 0f)
                {
                    _yVelocity = 0f;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (Mathf.Abs(_zVelocity) > 0.1)
                    {
                        RunningJump();
                    }
                    else
                    {
                        StartCoroutine(IdleJumpRoutine());
                    }
                }
            }
            _yVelocity -= _gravity * Time.deltaTime;
            _anim.SetFloat("VertSpeed", _yVelocity);
            _playerVelocity = new Vector3(0f, _yVelocity, _zVelocity);
            _controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
    public void LedgeGrab(Vector3 ledge, Vector3 finalIdle)
    {
        _grabbingLedge = true;
        _controller.enabled = false;
        _jumpingRunning = false;
        _jumpingIdle = false;
        _anim.SetBool("Jump", false);
        _grabbingLedge = true;
        _anim.SetBool("GrabLedge", true);
        _anim.SetFloat("Speed", 0.0f);
        _isFalling = false;
        _anim.SetBool("Falling", false);
        transform.position = ledge;
        _ledgePos = finalIdle;
    }

    public void ClimbLedge()
    {
        transform.position = _ledgePos;
        _grabbingLedge = false;
        _anim.SetBool("GrabLedge", false); 
        _controller.enabled = true;
    }

    IEnumerator IdleJumpRoutine()
    {
        _jumpingIdle = true;
        _anim.SetBool("Jump", _jumpingIdle);
        yield return new WaitForSeconds(0.5f);
        _yVelocity = _jumpHeight;
        yield return new WaitForSeconds(1.4f);
        _jumpingIdle = false;
        _anim.SetBool("Jump", _jumpingIdle);
    }

    private void RunningJump()
    {
        _jumpingRunning = true;
        _anim.SetBool("Jump", _jumpingRunning);
        _yVelocity = _jumpHeight;
    }
}
