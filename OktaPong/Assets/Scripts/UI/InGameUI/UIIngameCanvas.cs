using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIIngameCanvas : CanvasController
{
    //Members
    private List<IScoreTrigger> scoreTriggers = null;
    private List<UIScore> panelsList = null;

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();
        panelsList = GetComponentsInChildren<UIScore>(true).ToList();
        scoreTriggers = Utils.FindInterfacesOfType<IScoreTrigger>().ToList();
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
        scoreTriggers.ForEach(trigger => trigger.OnScoreTrigger += ScoreTrigger_OnScoreTrigger);
    }

    private void UnSubscribeToEvents()
    {
        scoreTriggers.ForEach(trigger => trigger.OnScoreTrigger -= ScoreTrigger_OnScoreTrigger);   
    }

    private void ScoreTrigger_OnScoreTrigger(Filiation filiation)
    {
        foreach(var scorePanel in panelsList)
        {
            if(scorePanel.Filiation == filiation)
            {
                scorePanel.IncrementScore();
            }
        }
    }

    #endregion

}
