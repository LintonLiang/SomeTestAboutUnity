using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushUI : MonoBehaviour {

    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }
}
