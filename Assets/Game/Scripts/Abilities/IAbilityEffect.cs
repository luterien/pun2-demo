using System.Collections;
using UnityEngine;

public interface IAbilityEffect
{
    PlayerController PlayerController { get; set; }
    Vector3 Direction { get; set; }
    AbilityAsset AbilityAsset { get; set; }
}
