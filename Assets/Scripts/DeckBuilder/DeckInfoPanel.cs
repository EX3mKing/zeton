using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(DeckBuilder))]
public class DeckInfoPanel : MonoBehaviour
{
    private DeckBuilder _deckBuilder;
    
    public TMP_InputField InputField;

    public List<GameObject> tokens = new List<GameObject>();
    public DeckInfo currentDeckInfo;

    private void Start()
    {
        _deckBuilder = gameObject.GetComponent<DeckBuilder>();
    }

    public void OpenDeckInfo(DeckInfo info)
    {
        currentDeckInfo = info;
        InputField.text = info.deckName;
        HideTokens();
        string[] tokenInfo = info.deckInfoString.Split(':');
        
        if (tokenInfo.Length <= 1) return;
        for (int i = 0; i < tokenInfo.Length; i++)
        {
            tokens[i].SetActive(true);
            tokens[i].transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tokenInfo[i][0].ToString(); // shape
            tokens[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tokenInfo[i][1].ToString(); // ability
            tokens[i].transform.GetChild(2).GetComponent<TMP_Text>().text = (i+1).ToString();                                        // index
        }
    }

    public void HideTokens()
    {
        foreach (var token in tokens)
        {
            token.SetActive(false);
        }
    }

    public void SaveDeckName()
    {
        currentDeckInfo.deckName = InputField.text;
        currentDeckInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentDeckInfo.deckName;
    }

    public void OpenDeckInfo()
    {
        _deckBuilder.canvasDeckBuilderObjects.ForEach(obj => obj.SetActive(false));
        _deckBuilder.canvasDeckPicker.SetActive(true);
        OpenDeckInfo(currentDeckInfo);
    }
}
