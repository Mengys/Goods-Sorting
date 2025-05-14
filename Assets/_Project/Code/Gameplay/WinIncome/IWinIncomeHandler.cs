namespace _Project.Code.Gameplay.WinIncome
{
    public interface IWinIncomeHandler : IWinIncomeProvider
    {
        void HandleDefault();
        void HandleRewarded();
    }
}