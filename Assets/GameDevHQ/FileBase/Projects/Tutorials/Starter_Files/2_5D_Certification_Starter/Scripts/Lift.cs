using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField]
    private Transform _floor1, _floor2, _floor3;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _nextFloor;
    private float _speed = 3f;
    private float _delay = 5f;
    [SerializeField]
    private bool _goingUp;
    
    // Start is called before the first frame update
    void Start()
    {
        _target = _floor2;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null && _nextFloor == null)
        {
            Debug.Log("Distance " + Vector3.Distance(transform.position, _floor2.position)); 
            if (Vector3.Distance(transform.position, _floor1.position) < 0.05f)
            {
                _goingUp = true;
                _nextFloor = _floor2;
                StartCoroutine(LiftMove());
            }
            else if (Vector3.Distance(transform.position, _floor2.position) < 0.05f)
            {
                if (_goingUp == true)
                {
                    _nextFloor = _floor3;
                    StartCoroutine(LiftMove());
                }
                else
                {
                    _nextFloor = _floor1;
                    StartCoroutine(LiftMove());
                }
            }
            else if (Vector3.Distance(transform.position, _floor3.position) < 0.05f)
            {
                _goingUp = false;
                _nextFloor = _floor2;
                StartCoroutine(LiftMove());
            }
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _target.transform.position) < 0.05f)
            {
                _target = null;
            }
        }
    }

    IEnumerator LiftMove()
    {
        yield return new WaitForSeconds(_delay);
        _target = _nextFloor;
        _nextFloor = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player trigger");
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
