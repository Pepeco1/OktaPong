using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
    public UnityAction<Filiation> OnShipDeath { get => onShipDead; set => onShipDead = value; }


    //Atributes
    //Members
    private List<Ship> shipLists = null;
    
    //Events
    private UnityAction<Filiation> onShipDead = null;

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

    #endregion


    #region private functions



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
