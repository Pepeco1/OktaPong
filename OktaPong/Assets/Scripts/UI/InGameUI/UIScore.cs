using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{

    public ShipFiliation ShipFiliation { get => ship.Filiation; }

    //Atributes
    private int currentScore = 0;

    //Members classes
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private Ship ship = null;


    public void IncrementScore()
    {
        currentScore++;
        text.SetText(currentScore.ToString());
    }

}
