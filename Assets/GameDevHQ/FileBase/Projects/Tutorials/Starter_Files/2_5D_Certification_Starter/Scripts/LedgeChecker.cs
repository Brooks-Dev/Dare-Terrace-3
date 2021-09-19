using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    [SerializeField]
    private Vector3 _ledgePos, _idlePos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrab"))
        {
            Player player = other.transform.parent.GetComponent<Player>();
            if (player != null)
            {
                player.LedgeGrab(_ledgePos, _idlePos);
            }
        }
    }
}
