using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ToolTipZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [FormerlySerializedAs("_tool")] public Image tool;
    public UnityEngine.UI.Button moonButton;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("mouse enter");
        //tool.enabled = true;
        if(!moonButton.interactable)
            tool.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("mouse exit");
        if(!moonButton.interactable)
            tool.enabled = false;
    }
}

