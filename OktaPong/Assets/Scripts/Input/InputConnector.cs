using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputConnector : MonoBehaviour
{
    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();

        var providers = FindObjectsOfType<PlayerInputProvider>().ToList();

        // Finds the 
        var provider = providers.Find(prov => prov.ID == playerInput.playerIndex);

        // TODO - Copy playerInput component to the provider's GO;
        //ComponentUtils.CopyComponent(playerInput, provider.gameObject);
        provider.gameObject.AddComponent<PlayerInput>();
        var newComponent  = provider.gameObject.GetComponent<PlayerInput>();
        ComponentUtils.GetCopyOf(newComponent, playerInput);
       
    }

}
