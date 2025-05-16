using UnityEngine;

namespace _Project.Code.Data.Static.ScoreIncome
{
    [CreateAssetMenu(fileName = "ScoreIncomeConfig", menuName = "Configs/ScoreIncomeConfig")]
    public class ScoreIncomeConfigAsset : ScriptableObject
    {
        [field: SerializeField] public ScoreIncomeConfig Config { get; private set; } = ScoreIncomeConfig.Default;
    }
}