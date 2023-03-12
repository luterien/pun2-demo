using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySlotUI : MonoBehaviour
{
    public PlayerController PlayerController { get; private set; }

    public AbilityAsset abilityAsset;

    [Header("Dependencies")]
    public Image abilityIcon;
    public TMP_Text abilityCooldownText;
    public Button abilityInputButton;

    private bool AbilityOnCooldown { get; set; }
    private float _timer;

    public void Initialize(PlayerController playerController)
    {
        PlayerController = playerController;
        AbilityOnCooldown = false;

        abilityIcon.sprite = abilityAsset.abilityIcon;
        abilityInputButton.onClick.AddListener(OnAbilityClick);
    }

    private void Update()
    {
        if (AbilityOnCooldown)
        {
            _timer -= Time.deltaTime;

            if (_timer >= 1f)
            {
                abilityCooldownText.text = ((int)_timer).ToString();
            }
            else if (_timer <= 0f)
            {
                AbilityOnCooldown = false;
                abilityCooldownText.enabled = false;
            }
        }
    }

    public void OnAbilityClick()
    {
        if (!AbilityOnCooldown && PlayerController.TryUseAbility(abilityAsset))
        {
            AbilityOnCooldown = true;
            _timer = abilityAsset.cooldown;
            abilityCooldownText.enabled = true;
        }
    }
}