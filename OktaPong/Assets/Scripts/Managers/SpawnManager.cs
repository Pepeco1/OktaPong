using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : SingletonMono<SpawnManager>
{

    [SerializeField] private float respawnShipTimer = 2f;


    private List<Ship> shipList = null;
    private Dictionary<Ship, Vector3> transformDic = null;

    private void Awake()
    {
        shipList = FindObjectsOfType<Ship>().ToList();
        transformDic = new Dictionary<Ship, Vector3>();
    }

    private void Start()
    {
        foreach (var ship in shipList)
        {
            transformDic.Add(ship, ship.transform.position);
        }
    }

    public void ShipDeath(Ship ship)
    {
        StartCoroutine(ShipBlink(ship));

        shipList.ForEach(s => RestoreLifeAndReposition(s));
    }

    private IEnumerator ShipBlink(Ship ship)
    {
        var timeToStop = Time.time + respawnShipTimer;
        while (timeToStop > Time.time)
        {
            bool setTo = ship.gameObject.activeSelf ? false : true;

            ship.gameObject.SetActive(setTo);
            yield return new WaitForSeconds(0.2f);
        }

        ship.gameObject.SetActive(true);
        TurnManager.Instance.ChangeTurnToThis(ship.InputProvider.ID);

    }

    private void RestoreLifeAndReposition(Ship ship)
    {
        FillLife(ship);
        ship.transform.position = transformDic[ship];
    }

    private void FillLife(Ship ship)
    {
        ship.Heal(10000);
    }

}
