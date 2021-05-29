using System;
using UnityEngine;
using UnityEngine.Events;

public interface IScoreTrigger
{
    Filiation Filiation { get;}
    Action<Filiation> OnScoreTrigger { get; set; }
}