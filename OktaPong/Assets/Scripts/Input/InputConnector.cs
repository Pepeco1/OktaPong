using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputConnector : MonoBehaviour
{
    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();

        var providers = FindObjectsOfType<PlayerInputProvider>().ToList();

        // Finds the 
        var provider = providers.Find(prov => prov.ID == playerInput.playerIndex);

        if(provider != null)
            provider.transform.parent = this.transform;

        //// TODO - Copy playerInput component to the provider's GO;
        //provider.gameObject.AddComponent<PlayerInput>();
        //var newComponent  = provider.gameObject.GetComponent<PlayerInput>();
        //ComponentUtils.GetCopyOf(newComponent, playerInput);

        //Destroy script
        Destroy(this);
       
    }
}
