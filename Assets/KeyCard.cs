using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    [SerializeField]
    private int _keyID;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 180 * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (_keyID)
            {
                case 0:
                    GameManager.Instance.HasCards[0] = true;
                    Debug.Log("Yellow card");
                    Destroy(this.gameObject);
                    break;
                case 1:
                    GameManager.Instance.HasCards[1] = true;
                    Debug.Log("Red card");
                    Destroy(this.gameObject);
                    break;
                case 2:
                    GameManager.Instance.HasCards[2] = true;
                    Debug.Log("Blue card");
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
