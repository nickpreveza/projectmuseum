using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameColors
{
    public Color transparent;

    [Header("UI")]
    public Color uiButtonIdle;
    public Color uiButtonHighlighted;
    public Color uiButtonPressed;

    public Color uiHeaderColor;
    public Color bodyColor;

    public Color uiPanelMain;
    public Color uiPanelSecondary;
    public Color backgroundColor;

    public Color uiSelected;
    public Color uiDeselected;

    [Header("Game Buttons")]
    public Color buttonIdle;
    public Color buttonHighlighted;
    public Color buttonPressed;
    public Color buttonUnavailable;

    [Header("Shaders")]
    public Color highlightColor;
    public Color normalColor;

 

}
