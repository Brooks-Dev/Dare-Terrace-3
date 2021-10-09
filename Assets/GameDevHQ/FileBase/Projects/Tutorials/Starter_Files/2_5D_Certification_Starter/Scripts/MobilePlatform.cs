using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA, _pointB;
    private Transform _target;
    private float _speed;
    private bool _activePlatform;
    [SerializeField]
    private int _mobilePlatformID;

    // Start is called before the first frame update
    void Start()
    {
        _target = _pointA;
        _speed = 0f;
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.HasCards[_mobilePlatformID] == true && _activePlatform == false)
        {
            _activePlatform = true;
            _speed = 3.0f;
        }
        else
        {
            _activePlatform = false;
            _speed = 0f;
        }
        if (_activePlatform == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _pointA.position) < 0.05f)
            {
                _target = _pointB;
            }
            else if (Vector3.Distance(transform.position, _pointB.position) < 0.05f)
            {
                _target = _pointA;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player trigger"); 
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
