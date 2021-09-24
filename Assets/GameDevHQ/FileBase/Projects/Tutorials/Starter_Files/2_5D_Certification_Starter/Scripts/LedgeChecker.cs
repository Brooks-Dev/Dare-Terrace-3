using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject _pointHanging, _pointStand;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrab"))
        {
            Player player = other.transform.parent.GetComponent<Player>();
            if (player != null)
            {
                player.LedgeGrab(_pointHanging.transform.position, _pointStand.transform.position);
            }
        }
    }
}
