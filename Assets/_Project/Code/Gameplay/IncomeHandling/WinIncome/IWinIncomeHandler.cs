namespace _Project.Code.Gameplay.IncomeHandling.WinIncome
{
    public interface IWinIncomeHandler : IWinIncomeProvider
    {
        void HandleDefault();
        void HandleRewarded();
    }
}