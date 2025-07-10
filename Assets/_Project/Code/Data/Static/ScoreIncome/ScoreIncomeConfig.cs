using System;

namespace _Project.Code.Data.Static.ScoreIncome
{
    [Serializable]
    public struct ScoreIncomeConfig
    {
        public int MatchIncome;
        
        public static ScoreIncomeConfig Default => new()
        {
            MatchIncome = 1
        };
    }
}