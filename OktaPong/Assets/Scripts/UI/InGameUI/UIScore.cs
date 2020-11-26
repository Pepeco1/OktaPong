using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{

    public Filiation Filiation { get => ship.Filiation; }

    //Atributes
    private int currentScore = 0;

    //Members classes
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private Ship ship = null;
    [SerializeField] private Filiation filiation = Filiation.none;


    #region Unity Functions

    private void Awake()
    {
        if (filiation == Filiation.none)
            Debug.LogWarning("[UIScore] missing shipFiliation reference");
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnSubscribeToEvents();
    }

    #endregion


    public void IncrementScore()
    {
        currentScore++;
        text.SetText(currentScore.ToString());

        CheckForEndGame();
    }

    private void CheckForEndGame()
    {
        if(currentScore >= GameManager.Instance.PointsToWinGame)
        {
            GameManager.Instance.DoEndGame(this.filiation);
        }
    }

    #region Events

    private void SubscribeToEvents()
    {
        GameManager.Instance.OnShipDeath += GameManager_OnShipDeath;
        //ship.OnHit += Ship_OnHit;
    }

    private void UnSubscribeToEvents()
    {
        GameManager.Instance.OnShipDeath -= GameManager_OnShipDeath;
        //ship.OnHit -= Ship_OnHit;
    }

    private void Ship_OnHit()
    {
        IncrementScore();
    }

    private void GameManager_OnShipDeath(Filiation deadShipFiliation)
    {
        if(this.filiation != deadShipFiliation)
            IncrementScore();
    }

    #endregion
}
