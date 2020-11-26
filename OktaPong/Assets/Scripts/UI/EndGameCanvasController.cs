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

    public void SetEndGameTextAccordingToWinner(Filiation winner)
    {


        if (winner == Filiation.Player1)
            SetText("Player 1 wins");
        else if (winner == Filiation.Player2)
            SetText("Player 2 wins");
        else
            Debug.LogWarning("[EndGameCanvas] Filiation not supported by function");
    }
}
