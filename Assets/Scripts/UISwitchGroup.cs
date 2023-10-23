using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitchGroup : MonoBehaviour
{
    public List<UISwitch> switches = new List<UISwitch>();
    public int currentIndex;
    
    public GameObject messageReceiver;
    public string methodName = "openTokenForEditing";

    public void SwitchStates(int index)
    {
        currentIndex = index;
        SwitchAllTo(false);
        switches[index].SwitchState();
    }
    
    public void SwitchStates(int index, bool state)
    {
        currentIndex = index;
        SwitchAllTo(state);
        switches[index].SwitchState();
    }

    public void SwitchAllTo(bool state)
    {
        foreach (var varSwitch in switches)
        {
            varSwitch.SwitchState(state);
        }
        
        if(messageReceiver != null) messageReceiver.SendMessage(methodName);
    }
}
