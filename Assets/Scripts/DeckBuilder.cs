using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpPoints;
    [SerializeField] private GameObject tokenFitter;
    [SerializeField] private UISwitchGroup tokenSwitchGroup;
    [SerializeField] private GameObject tokenInfoPrefab;
    [SerializeField] private List<TokenInfo> tokens = new List<TokenInfo>();
    

    private int currentPoints = 30;

    private Dictionary<string, string> TokenShape = new Dictionary<string, string>()
    {
        { "a", "a" },
        { "b", "b" },
        { "c", "c" },
        { "d", "d" },
        { "e", "e" }
    };
    
    private Dictionary<string, string> TokenAbilities = new Dictionary<string, string>()
    {
        { "1", "1" },
        { "2", "2" },
        { "3", "3" },
        { "4", "4" },
        { "5", "5" }
    };
    
    private void Awake()
    {
        tmpPoints.text = currentPoints.ToString();
    }

    // create new token
    public void AddNewToken()
    {
        GameObject token = Instantiate(tokenInfoPrefab, tokenFitter.transform);
        
        TokenInfo ti = token.GetComponent<TokenInfo>();
        ti.index = tokens.Count;
        ti.EditTokenInfo("B","5");
        tokens.Add(ti);
        
        UISwitch tokenSwitch = token.transform.GetChild(0).GetComponent<UISwitch>();
        tokenSwitchGroup.switches.Add(tokenSwitch);
        tokenSwitch.switchGroup = tokenSwitchGroup;
        tokenSwitch.groupIndex = ti.index;
    }
    

    // remove selected token
    public void RemoveToken()
    {
        
    }
    
    // remove token at index
    public void RemoveToken(int index)
    {
        
    }
}
