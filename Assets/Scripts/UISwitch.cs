using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwitch : MonoBehaviour
{
    public bool state = false;
    
    public Color offColor;
    public Color onColor;

    public Image targetImage;
    
    public UISwitchGroup switchGroup;
    public int groupIndex;

    public void SwitchState()
    {
        state = !state;
        targetImage.color = state ? onColor : offColor;
    }
    
    public void SwitchState(bool newState)
    {
        state = newState;
        targetImage.color = state ? onColor : offColor;
    }
    
    public void SwitchGroupState()
    {
        switchGroup.SwitchStates(groupIndex);
    }
}
