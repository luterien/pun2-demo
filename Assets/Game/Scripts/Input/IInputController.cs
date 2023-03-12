using System.Collections;
using UnityEngine;

public interface IInputController
{
    float HorizontalAxis { get; }
    float VerticalAxis { get; }

    void Tick(float deltaTime);
}