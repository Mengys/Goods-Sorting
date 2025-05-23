namespace _Project.Code.Gameplay.IncomeHandling.WinIncome
{
    public interface IWinIncomeProvider
    {
        int DefaultIncome { get; }
        int RewardedIncome { get; }
    }
}