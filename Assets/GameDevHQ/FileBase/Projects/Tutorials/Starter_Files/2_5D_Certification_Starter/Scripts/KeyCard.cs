using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    [SerializeField]
    private int _keyID;
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI manager is null in UI Manager!");
        }
    }

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
                    if (_uiManager != null)
                    {
                        _uiManager.ToggleCard(0);
                    }
                    Destroy(this.gameObject);
                    break;
                case 1:
                    GameManager.Instance.HasCards[1] = true;
                    Debug.Log("Red card");
                    if (_uiManager != null)
                    {
                        _uiManager.ToggleCard(1);
                    }
                    Destroy(this.gameObject);
                    break;
                case 2:
                    GameManager.Instance.HasCards[2] = true;
                    Debug.Log("Blue card");
                    if (_uiManager != null)
                    {
                        _uiManager.ToggleCard(2);
                    }
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
