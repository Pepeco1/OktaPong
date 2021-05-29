using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
    public Action<Filiation> OnShipDeath { get => onShipDead; set => onShipDead = value; }
    public int PointsToWinGame { get => pointsToWinGame; set => pointsToWinGame = value; }

    //Atributes
    [SerializeField] private int pointsToWinGame = 3;

    //Members
    private List<Ship> shipLists = null;

    //Events
    private Action<Filiation> onShipDead = null;

    #region Unity Functions

    private void Awake()
    {

        shipLists = FindObjectsOfType<Ship>().ToList();

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

    #region public functions

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DoEndGame(Filiation winner)
    {
        TurnManager.Instance.ChangeTurnToThis(-1);

        // Some visual feedback?

        SetEndGameTextAccordingToWinner(winner);
        StartCoroutine(DelayedEnd());
    }
    #endregion

    #region private functions

    private IEnumerator DelayedEnd()
    {
        yield return new WaitForSeconds(2f);
        CanvasManager.Instance.SwitchState(CanvasType.EndGame);
    }

    private void SetEndGameTextAccordingToWinner(Filiation winner)
    {

        var endGameController = CanvasManager.Instance.GetControllerWithType(CanvasType.EndGame)
                                        .GetComponent<EndGameCanvasController>();

        if (endGameController == null)
        {
            Debug.LogError("[GameManager] endGameController not found");
            return;
        }

        endGameController.SetEndGameTextAccordingToWinner(winner);
        
    }

    #endregion
    
    #region Events related functions

    private void SubscribeToEvents()
    {
        shipLists.ForEach(ship => ship.OnDeath += Ship_OnDeath);
    }

    private void UnSubscribeToEvents()
    {
        shipLists.ForEach(ship => ship.OnDeath -= Ship_OnDeath);
    }

    private void Ship_OnDeath(Ship ship)
    {
        //OnShipDeath?.Invoke(filiation);
        TurnManager.Instance.ChangeTurnToThis(-1);
        SpawnManager.Instance.ShipDeath(ship);

    }

    #endregion

}
