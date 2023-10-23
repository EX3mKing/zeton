using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitchGroup : MonoBehaviour
{
    public List<UISwitch> switches = new List<UISwitch>();

    public void SwitchStates(int index)
    {
        SwitchAllOff();
        switches[index].SwitchState();
    }

    public void SwitchAllOff()
    {
        foreach (var varSwitch in switches)
        {
            varSwitch.SwitchState(false);
        }
    }
}
