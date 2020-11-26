using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
    public UnityAction<Filiation> OnShipDeath { get => onUpdateScoreEvent; set => onUpdateScoreEvent = value; }

    
    //Events
    private UnityAction<Filiation> onUpdateScoreEvent = null;

    #region Unity Functions

    private void OnEnable()
    {
        //SubscribeToEvents();
    }

    private void OnDisable()
    {
        //UnsubscribeToEvents();
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
    

    private void Ship_OnDeath(Filiation filiation)
    {
        OnShipDeath?.Invoke(filiation);
    }

    #endregion

}
