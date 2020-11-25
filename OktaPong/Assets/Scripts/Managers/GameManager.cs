using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{

    Dictionary<ShipFiliation, Ship> shipsInGame = null;

    public UnityAction<ShipFiliation> onShipDeath = null;


    #region Unity Functions
    private void Awake()
    {
        shipsInGame = FindObjectsOfType<Ship>().ToDictionary(ship => ship.Filiation , ship => ship);
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    #endregion

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    #region Events related functions
    
    private void SubscribeToEvents()
    {
        foreach (var ship in shipsInGame.Values)
        {
            ship.onDeath += Ship_OnDeath;
        }
    }

    private void UnsubscribeToEvents()
    {
        foreach (var ship in shipsInGame.Values)
        {
            ship.onDeath -= Ship_OnDeath;
        }
    }

    private void Ship_OnDeath(ShipFiliation filiation)
    {
        onShipDeath?.Invoke(filiation);
    }

    #endregion

}
