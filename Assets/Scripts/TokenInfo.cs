using System;
using TMPro;
using UnityEngine;

public class TokenInfo : MonoBehaviour
{
    // 0 => ability
    // 1 => shape
    public string ability = " ";
    public string shape = " ";
    [SerializeField] private TextMeshProUGUI abilityTMP;
    [SerializeField] private TextMeshProUGUI shapeTMP;
    public int index = 999;
    private void Awake()
    {
        UpdateVisuals();
    }
    
    public void EditTokenInfo(string newShape, string newAbility)
    {
        if (newShape != " ") shape = newShape;
        if (newAbility != " ") ability = newAbility;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        abilityTMP.text = ability;
        shapeTMP.text = shape;
    }
    
}
