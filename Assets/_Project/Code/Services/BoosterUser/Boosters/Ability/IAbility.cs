using Zenject;

namespace _Project.Code.Services.BoosterUser.Boosters.Ability
{
    public interface IAbility
    {
        void Use();
        void Initialize(DiContainer container);
    }
}