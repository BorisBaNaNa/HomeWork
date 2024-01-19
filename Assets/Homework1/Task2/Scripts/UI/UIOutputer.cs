using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIOutputer : MonoBehaviour, IService
{
    [SerializeField] private TextMeshProUGUI _ammoCountText;
    [SerializeField] private TextMeshProUGUI _respectCountText;
    [SerializeField] private TextMeshProUGUI _interactionText;

    public void OutAmmoCount(string ammoCount)
    {
        _ammoCountText.text = ammoCount;
    }

    public void OutRespectCount(string respectCount)
    {
        _respectCountText.text = respectCount;
    }

    public void SetActiveInteractionText(bool active)
    {
        _interactionText.gameObject.SetActive(active);
    }
}
