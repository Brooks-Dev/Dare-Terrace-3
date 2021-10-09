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
    [SerializeField]
    private bool _onLadder, _climbingLadder;
    private Vector3 _ladderTop, _ladderBottom;
    private float _ladderSpeed = 2f;
    private UIManager _uiManager;
    
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
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager in player is null!");
        }
        _distToGround = _controller.bounds.extents.y + 0.54f;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameRunning == true)
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, _distToGround);
            if (_onLadder == true)
            {
                if (_gravity > 0)
                {
                    _gravity = 0f;
                    _anim.SetBool("Ladder", _onLadder);
                    _climbingLadder = true;
                    transform.position = _ladderBottom;
                    transform.rotation = Quaternion.LookRotation(Vector3.back);
                    Debug.Log("Bottom of ladder " + _ladderBottom);
                }
                _yVelocity = Input.GetAxis("Vertical") * _ladderSpeed;
                if (Mathf.Abs(_yVelocity) > 0.1f)
                {
                    if (_yVelocity > 0)
                    {
                        _anim.speed = 1f;
                        _anim.SetFloat("Movement", 1f);
                    }
                    else
                    {
                        _anim.speed = 1f;
                        _anim.SetFloat("Movement", -1f);
                    }
                }
                else
                {
                    if (_gravity == 0)
                    {
                        _anim.speed = 0f;
                    }
                }
            }
            CalculateMovement();
            if (Input.GetKeyDown(KeyCode.E) && _grabbingLedge == true)
            {
                _anim.SetBool("Climb", true);
                transform.Translate(new Vector3(0, 0.1f, -0.2f));
            }
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
                if (_yVelocity < -20)
                {
                    GameManager.Instance.GameOver = true;
                    PlayerDies();
                }
                else if (_yVelocity < 0)
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
            if (GameManager.Instance.GameRunning == true)
            {
                _controller.Move(_playerVelocity * Time.deltaTime);
            }
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

    public void ClimbLadder(Vector3 top, Vector3 bottom)
    {
        _ladderTop = top;
        _ladderBottom = bottom;
        _anim.SetFloat("Speed", 0);
        _onLadder = true;
    }

    public void OffLadder()
    {
        Debug.Log("Top position " + _ladderTop);
        transform.position = _ladderTop;
        transform.rotation = Quaternion.LookRotation(Vector3.back);
        _gravity = 9.8f;
        _climbingLadder = false;
        _controller.enabled = true;
    }

    public void ExitLadder()
    {
        _onLadder = false;
        _anim.SetBool("Ladder", _onLadder);
        _anim.speed = 1;
        _yVelocity = 0;
        if (_climbingLadder == true)
        {
            _controller.enabled = false;
        }
    }

    public void PlayerDies()
    {
        GameManager.Instance.GameOver = true;
        GameManager.Instance.GameRunning = false;
        gameObject.SetActive(false);
        _uiManager.GameOverUI();
    }

    public void ObjectivesCompleted()
    {
        GameManager.Instance.GameRunning = false;
        gameObject.SetActive(false);
        _uiManager.GameOverUI();
    }

    public void ActivatePlayer()
    {
        transform.position = new Vector3(0f, 2f, 0f);
        _yVelocity = 0f;
        gameObject.SetActive(true);
    }
}
