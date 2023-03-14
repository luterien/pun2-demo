using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    bool IsComplete { get; }

    void OnEnter();
    void Tick(float deltaTime);
    void OnExit();
}
