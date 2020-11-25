using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{

    private int idOfCurrent = -1;

    private List<InputProvider> gameParticipants = null;

    public UnityAction onTurnChange = null;

    private void Awake()
    {
        gameParticipants = FindObjectsOfType<InputProvider>().ToList();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }


    private void OnDisable()
    {
        UnsubscribeToEvents();
    }


    private void Start()
    {
        //Start first turn
        ChangeTurnToThis(0);
    }

    private void ChangeTurnToThis(int id)
    {
        gameParticipants.ForEach(provider => SetPermissionTrueIfSameID(provider));

        void SetPermissionTrueIfSameID(InputProvider provid)
        {
            if(provid.ID == id)
            {
                provid.Permission = true;
                idOfCurrent = id;
            }
            else
            {
                provid.Permission = provid.ID == id ? true : false;
            }
        }

    }


    private void NextTurn()
    { 
        //Change to next Turn
        ChangeTurnToThis((idOfCurrent + 1) % gameParticipants.Count);
        onTurnChange?.Invoke();
    }


    #region Events Functions
    private void InputProvider_OnTurnChangeEvent()
    {
        NextTurn();
    }

    private void SubscribeToEvents()
    {
        gameParticipants.ForEach(participant => participant.onTurnChangeEvent += InputProvider_OnTurnChangeEvent);
    }

    private void UnsubscribeToEvents()
    {
        gameParticipants.ForEach(participant => participant.onTurnChangeEvent -= InputProvider_OnTurnChangeEvent);
    }
    #endregion

}
