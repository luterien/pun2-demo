using System.Collections;
using UnityEngine;

public class AbilitySlotManager : MonoBehaviour
{
    public AbilitySlotUI basicAbilitySlot;
    public AbilitySlotUI specialAbilitySlot;

    private void Awake()
    {
        PlayerController.OnLocalPlayerCreated += PlayerController_OnLocalPlayerCreated;
    }

    private void PlayerController_OnLocalPlayerCreated(PlayerController playerController)
    {
        basicAbilitySlot.Initialize(playerController);
        specialAbilitySlot.Initialize(playerController);
    }
}