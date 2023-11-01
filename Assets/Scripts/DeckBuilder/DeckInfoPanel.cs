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

    public List<DeckInfo> deckInfos = new List<DeckInfo>();

    private void Start()
    {
        _deckBuilder = gameObject.GetComponent<DeckBuilder>();
        CreateInitialSaveFiles();
        LoadDeckInfos();
    }

    public void OpenDeckInfo(DeckInfo info)
    {
        currentDeckInfo = info;
        InputField.text = info.deckName;
        HideTokens();
        string[] tokenInfo = info.deckInfoString.Split(':');
        
        if (tokenInfo[0].Length == 0) return;
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
        SaveSystem.SaveData(new SaveFile(currentDeckInfo), currentDeckInfo.deckFileName);
    }

    public void OpenDeckInfo()
    {
        _deckBuilder.canvasDeckBuilderObjects.ForEach(obj => obj.SetActive(false));
        _deckBuilder.canvasDeckPicker.SetActive(true);
        OpenDeckInfo(currentDeckInfo);
    }

    // load info from file
    public void LoadDeckInfos()
    {
        foreach (var info in deckInfos)
        {
            var file = SaveSystem.LoadData<SaveFile>(info.deckFileName);
            
            if (file == null)
            {
                Debug.LogWarning("SAVE FILE NOT FOUND");
                continue;
            }
            
            info.deckInfoString = file.deckTokenString;
            info.deckName = file.deckName;
            info.deckValidity = file.deckValidity;

            info.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = info.deckName;
        }
    }

    public void CreateInitialSaveFiles()
    {
        foreach (var info in deckInfos)
        {
            if (SaveSystem.LoadData<SaveFile>(info.deckFileName) != null) continue;
            SaveSystem.SaveData(new SaveFile(info), info.deckFileName);
        }
    }
    
    public void SaveCurrentDeckInfo()
    {
        SaveSystem.SaveData(new SaveFile(currentDeckInfo), currentDeckInfo.deckFileName);
    }
}
