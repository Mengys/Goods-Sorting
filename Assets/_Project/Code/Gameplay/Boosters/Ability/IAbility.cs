using Zenject;

namespace _Project.Code.Gameplay.Boosters.Ability
{
    public interface IAbility
    {
        void Use();
        void Initialize(DiContainer container);
    }
}