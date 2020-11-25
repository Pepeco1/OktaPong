using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public CanvasType canvasType;
    [HideInInspector] public bool isOpen = false;

    protected virtual void Awake()
    {
        isOpen = gameObject.activeSelf;
    }

    public virtual void OpenBehavior()
    {
        if (!isOpen)
        {
            isOpen = true;
            gameObject.SetActive(true);
        }
    }

    public virtual void CloseBehavior()
    {
        if (isOpen)
        {
            isOpen = false;
            gameObject.SetActive(false);
        }
    }
}
