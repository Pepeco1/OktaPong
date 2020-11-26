using UnityEngine;
using UnityEngine.Events;

public interface IScoreTrigger
{
    Filiation Filiation { get;}
    UnityAction<Filiation> OnScoreTrigger { get; set; }
}