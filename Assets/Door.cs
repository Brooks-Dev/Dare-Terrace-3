using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _openDoor;
    private float _speed = 3.0f;
    [SerializeField]
    private Transform _doorOpen, _doorClosed;

    void Update()
    {
        if (GameManager.Instance.HasCards[0] == true && _openDoor == false)
        {
            _openDoor = true;
        }
    }

    private void FixedUpdate()
    {
        if (_openDoor == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _doorOpen.position, _speed * Time.deltaTime);
        }
    }
}
