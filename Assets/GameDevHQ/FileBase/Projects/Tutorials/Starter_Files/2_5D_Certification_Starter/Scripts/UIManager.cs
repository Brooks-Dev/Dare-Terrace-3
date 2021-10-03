using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Toggle[] _toggle;

    public void ToggleCard(int index)
    {
        _toggle[index].isOn = true;
    }
}
