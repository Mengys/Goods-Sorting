namespace _Project.Code.Gameplay.WinIncome
{
    public interface IWinIncomeProvider
    {
        int DefaultIncome { get; }
        int RewardedIncome { get; }
    }
}