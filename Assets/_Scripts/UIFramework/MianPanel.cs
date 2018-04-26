using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MianPanel : BasePanel {

    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }
    public void OnPushPanel(int panelID)
    {
        UIPanelType panelType = (UIPanelType)panelID;
        UIManager.Instance.PushPanel(panelType);
    }
}
