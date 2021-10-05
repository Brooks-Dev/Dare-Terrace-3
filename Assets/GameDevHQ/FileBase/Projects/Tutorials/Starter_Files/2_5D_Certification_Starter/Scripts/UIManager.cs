using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Toggle[] _toggles;

    public void ToggleCard(int index)
    {
        _toggles[index].isOn = true;
    }
}
