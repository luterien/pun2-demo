using System.Collections;
using UnityEngine;

public class PlayerInputController : IInputController
{
    public float HorizontalAxis { get; private set; }
    public float VerticalAxis { get; private set; }

    public void Tick(float deltaTime)
    {
        HorizontalAxis = Input.GetAxis("Horizontal");
    }
}