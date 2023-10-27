using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(DeckInfoPanel))]
public class DeckBuilder : MonoBehaviour
{
    private DeckInfoPanel _deckInfoPanel;
    
    [SerializeField] private GameObject tokenFitter;
    [SerializeField] private UISwitchGroup tokenSwitchGroup;
    [SerializeField] private GameObject tokenInfoPrefab;
    [SerializeField] private List<TokenInfo> tokens = new List<TokenInfo>();
    
    public UISwitchGroup shapeSwitchGroup;
    public UISwitchGroup abilitySwitchGroup;
    
    public List<GameObject> abilities3D = new List<GameObject>();
    public List<GameObject> shapes3D = new List<GameObject>();

    public TextMeshProUGUI pointsText;
    public GameObject pointsActualText;
    public GameObject pointsConfirmButton;

    public int startPoints = 30;
    public int points = 0;
    
    public List<GameObject> canvasDeckBuilderObjects;
    public GameObject canvasDeckPicker;

    public TMP_InputField deckNameInputField;
    public GameObject addNewTokenButton;
    
    //symbol name       name  symbol     cost     
    //  O   none,        none    0        0
    //  A   square,      alro    1        1
    //  B   triangle,    krino   2        1
    //  C   pentagon,    xeno    3        2
    //  D   trapaizoid   hamo    4        2
    //  E   circle,      stro    5        3

    private void Start()
    {
        UpdatePoints();
        _deckInfoPanel = gameObject.GetComponent<DeckInfoPanel>();
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
    
    private Dictionary<string, int> costDict = new Dictionary<string, int>()
    {
        {"O", 500},
        {"0", 500},
        {"A", 1},
        {"B", 1},
        {"C", 2},
        {"D", 2},
        {"E", 3},
        {"1", 1},
        {"2", 1},
        {"3", 2},
        {"4", 2},
        {"5", 3}
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
        
        UpdatePoints();
    }
    
    public void AddNewToken (string shape, string ability)
    {
        GameObject token = Instantiate(tokenInfoPrefab, tokenFitter.transform);
        
        TokenInfo ti = token.GetComponent<TokenInfo>();
        ti.index = tokens.Count;
        ti.EditTokenInfo(shape, ability);
        tokens.Add(ti);
        
        UISwitch tokenSwitch = token.transform.GetChild(0).GetComponent<UISwitch>();
        tokenSwitchGroup.switches.Add(tokenSwitch);
        tokenSwitch.switchGroup = tokenSwitchGroup;
        tokenSwitch.groupIndex = ti.index;

        tokenSwitchGroup.SwitchStates(ti.index);
        token.transform.SetAsLastSibling();
        
        UpdatePoints();
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
        UpdatePoints();
    }

    private void Update3DShapesAndTokens()
    {
        Hide3DShapesAndTokens();
        shapes3D[shapeDict[tokens[tokenSwitchGroup.currentIndex].shape]-1].SetActive(true);
        abilities3D[int.Parse(tokens[tokenSwitchGroup.currentIndex].ability)-1].SetActive(true);
    }

    private void Hide3DShapesAndTokens()
    {
        foreach (var ability in abilities3D)
        {
            ability.SetActive(false);
        }
        foreach (var shape in shapes3D)
        {
            shape.SetActive(false);
        }
    }
    
    private void UpdatePoints()
    {
        int tempPoints = 0;
        foreach (var token in tokens)
        {
            tempPoints += costDict[token.shape];
            tempPoints += costDict[token.ability];
        }

        points = startPoints - tempPoints;
        pointsText.text = points.ToString();

        bool canConfirmDeck = points == 0;
        pointsActualText.SetActive(!canConfirmDeck);
        pointsText.gameObject.SetActive(!canConfirmDeck);
        pointsConfirmButton.SetActive(canConfirmDeck);
    }

    public void RemoveAllTokens()
    {
        foreach (var token in tokens) { Destroy(token.gameObject); }
        tokens.Clear();
        
        tokenSwitchGroup.switches.Clear();
        tokenSwitchGroup.currentIndex = 0;
        
        shapeSwitchGroup.SwitchAllToNoMSG(false);
        abilitySwitchGroup.SwitchAllToNoMSG(false);
        tokenSwitchGroup.SwitchAllToNoMSG(false);
        
        Hide3DShapesAndTokens();
        UpdatePoints();
    }

    public void RemoveCurrentToken()
    {
        Destroy(tokens[tokenSwitchGroup.currentIndex].gameObject);
        tokens.RemoveAt(tokenSwitchGroup.currentIndex);
        tokenSwitchGroup.switches.RemoveAt(tokenSwitchGroup.currentIndex);

        shapeSwitchGroup.SwitchAllToNoMSG(false);
        abilitySwitchGroup.SwitchAllToNoMSG(false);
        tokenSwitchGroup.SwitchAllToNoMSG(false);
        
        for (int i = 0; i < tokens.Count; i++)
        {
            tokens[i].index = i;
            tokenSwitchGroup.switches[i].groupIndex = i;
        }
        
        tokenSwitchGroup.SwitchStates(tokens.Count - 1);

        openTokenForEditing();
        UpdatePoints();
    }
    
    // FIRST GOES SHAPE THAN ABILITY
    // example SA:SA:SA -> A1:B2:C3

    public void ConfirmDeck()
    {
        string finalOutcome = "";
        foreach (var token in tokens)
        {
            finalOutcome += token.shape + token.ability + ':';
        }
        finalOutcome = finalOutcome.Remove(finalOutcome.Length - 1, 1);
        _deckInfoPanel.currentDeckInfo.deckInfoString = finalOutcome;
        string[] infos = _deckInfoPanel.currentDeckInfo.deckInfoString.Split(':');
        _deckInfoPanel.currentDeckInfo.deckValidity = points == 0;
        Debug.LogWarning(finalOutcome);
    }

    public void SaveDeckName()
    {
        _deckInfoPanel.currentDeckInfo.deckName = deckNameInputField.text;
        _deckInfoPanel.currentDeckInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _deckInfoPanel.currentDeckInfo.deckName;
    }

    public void OpenDeckBuilder()
    {
        if (_deckInfoPanel.currentDeckInfo == null) return;
        RemoveAllTokens();
        deckNameInputField.text = _deckInfoPanel.currentDeckInfo.deckName;
        canvasDeckPicker.SetActive(false);
        foreach (var obj in canvasDeckBuilderObjects) { obj.SetActive(true); }
        string[] deckInfo = _deckInfoPanel.currentDeckInfo.deckInfoString.Split(':');
        if (deckInfo.Length <= 1) return;
        foreach (var info in deckInfo)
        {
            AddNewToken(info[0].ToString(), info[1].ToString());
        }
        addNewTokenButton.transform.SetAsLastSibling();
    }
}
