using UnityEngine;

[CreateAssetMenu(fileName = "ShuffleAbility", menuName = "Configs/Abilities/ ShuffleAbility")]
public class ShuffleAbilityConfig : AbilityConfig
{
    public override IAbility GetAbility()
    {
        Shuffle shuffle = new Shuffle();

        return shuffle;
    }
}