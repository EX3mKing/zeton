using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SCRIPT ACTUALLY USED FOR SAVING THE CURRENT STATE

[System.Serializable]
public class SaveFile
{
    public string deckName;
    public string deckTokenString;
    public bool deckValidity;

    public SaveFile(DeckInfo info)
    {
        deckName = info.deckName;
        deckTokenString = info.deckInfoString;
        deckValidity = info.deckValidity;
    }
}