using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField] private GameObject tokenFitter;
    [SerializeField] private UISwitchGroup tokenSwitchGroup;
    [SerializeField] private GameObject tokenInfoPrefab;
    [SerializeField] private List<TokenInfo> tokens = new List<TokenInfo>();
    
    public UISwitchGroup shapeSwitchGroup;
    public UISwitchGroup abilitySwitchGroup;
    
    public List<GameObject> abilities3D = new List<GameObject>();
    public List<GameObject> shapes3D = new List<GameObject>();

    //  O none,        none    0
    //  A square,      alro    1
    //  B triangle,    krino   2
    //  C pentagon,    xeno    3
    //  D trapaizoid   hamo    4
    //  E circle,      stro    5

    private void Start()
    {
        //AddNewToken();
    }
    
    private Dictionary<string, int> shapeDict = new Dictionary<string, int>()
    {
        {"O", 6},
        {"A", 1},
        {"B", 2},
        {"C", 3},
        {"D", 4},
        {"E", 5}
    };

    // create new token
    public void AddNewToken()
    {
        GameObject token = Instantiate(tokenInfoPrefab, tokenFitter.transform);
        
        TokenInfo ti = token.GetComponent<TokenInfo>();
        ti.index = tokens.Count;
        ti.EditTokenInfo("A","1");
        tokens.Add(ti);
        
        UISwitch tokenSwitch = token.transform.GetChild(0).GetComponent<UISwitch>();
        tokenSwitchGroup.switches.Add(tokenSwitch);
        tokenSwitch.switchGroup = tokenSwitchGroup;
        tokenSwitch.groupIndex = ti.index;

        tokenSwitchGroup.SwitchStates(ti.index);
        token.transform.SetAsLastSibling();
    }

    public void openTokenForEditing()
    {
        shapeSwitchGroup.SwitchStates(shapeDict[tokens[tokenSwitchGroup.currentIndex].shape]-1);
        abilitySwitchGroup.SwitchStates(int.Parse(tokens[tokenSwitchGroup.currentIndex].ability)-1);
        Update3DShapesAndTokens();
    }

    public void EditCurrentToken(string info)
    {
        tokens[tokenSwitchGroup.currentIndex].EditTokenInfo(info);
        Update3DShapesAndTokens();
    }

    private void Update3DShapesAndTokens()
    {
        foreach (var ability in abilities3D)
        {
            ability.SetActive(false);
        }
        foreach (var shape in shapes3D)
        {
            shape.SetActive(false);
        }
        
        shapes3D[shapeDict[tokens[tokenSwitchGroup.currentIndex].shape]-1].SetActive(true);
        abilities3D[int.Parse(tokens[tokenSwitchGroup.currentIndex].ability)-1].SetActive(true);
    }
    
}
