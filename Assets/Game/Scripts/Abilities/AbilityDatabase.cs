using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Abilities/Database")]
public class AbilityDatabase : ScriptableObject
{
    private static AbilityDatabase instance;
    public static AbilityDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<AbilityDatabase>("AbilityDatabase");
            }

            return instance;
        }
    }

    public List<AbilityAsset> abilities;

    public AbilityAsset GetAbilityAsset(int id)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].id == id)
            {
                return abilities[i];
            }
        }

        throw new UnityException("Invalid ability ID");
    }
}