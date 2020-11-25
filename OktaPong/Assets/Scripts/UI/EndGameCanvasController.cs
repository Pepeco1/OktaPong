using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameCanvasController : CanvasController
{

    [SerializeField] private TextMeshProUGUI textComponent = null;

    public void SetText(string text)
    {
        textComponent.SetText(text);
    }
}
