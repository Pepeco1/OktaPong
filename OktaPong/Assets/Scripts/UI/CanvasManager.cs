using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CanvasManager : SingletonMono<CanvasManager>
{

    [SerializeField] private CanvasType openOnAwake = CanvasType.none;

    private Dictionary<CanvasType, CanvasController> controllersDic = new Dictionary<CanvasType, CanvasController>();
    private CanvasController lastActiveCanvas = null;

    private void Awake()
    {
        controllersDic = GetComponentsInChildren<CanvasController>(true).ToDictionary(controller => controller.canvasType, controller => controller);
        controllersDic.Select(kv => kv.Value).ToList().ForEach(value => value.CloseBehavior());
        SwitchState(openOnAwake);
    }

    public void SwitchState(CanvasType type)
    {
        lastActiveCanvas?.CloseBehavior();

        if (type == CanvasType.none)
            return;

        CanvasController desiredCanvas = null;
        controllersDic.TryGetValue(type, out desiredCanvas);

        if (desiredCanvas != null)
        {
            desiredCanvas.OpenBehavior();
            lastActiveCanvas = desiredCanvas;
        }
        else
        {
            Debug.LogError("[CanvasManager] Desired canvas not found");
        }


    }
}

public enum CanvasType
{
    none,
    MainMenu,
    InGameHUD,
    EndGame
}
