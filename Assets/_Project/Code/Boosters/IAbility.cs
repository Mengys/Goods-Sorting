using Zenject;

public interface IAbility
{
    void Use();
    void Initialize(DiContainer container);
}