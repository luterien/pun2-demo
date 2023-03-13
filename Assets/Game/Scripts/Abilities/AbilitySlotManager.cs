using System.Collections;
using UnityEngine;

public class AbilitySlotManager : MonoBehaviour
{
    public AbilitySlotUI basicAbilitySlot;
    public AbilitySlotUI specialAbilitySlot;

    public void Initialize(PlayerController playerController)
    {
        basicAbilitySlot.Initialize(playerController);
        specialAbilitySlot.Initialize(playerController);
    }
}