using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIIngameCanvas : CanvasController
{

    List<UIScore> panelsList = null;

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();
        panelsList = GetComponentsInChildren<UIScore>(true).ToList();
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

    #region Events related 

    private void SubscribeToEvents()
    {
        GameManager.Instance.onShipDeath += GameManager_OnShipDeath;
    }

    private void UnSubscribeToEvents()
    {
        GameManager.Instance.onShipDeath -= GameManager_OnShipDeath;
    }

    private void GameManager_OnShipDeath(ShipFiliation filiation)
    {
        foreach(var scorePanel in panelsList)
        {
            if(scorePanel.ShipFiliation != filiation)
            {
                scorePanel.IncrementScore();
            }
        }
    }

    #endregion

}
