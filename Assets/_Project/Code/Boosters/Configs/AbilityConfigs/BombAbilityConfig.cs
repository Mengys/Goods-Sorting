using UnityEngine;

[CreateAssetMenu(fileName = "BombAbility", menuName = "Configs/Abilities/BombAbility")]
public class BombAbilityConfig : AbilityConfig
{
    public override IAbility GetAbility()
    {
        Bomb bomb = new Bomb();

        return bomb;
    }
}