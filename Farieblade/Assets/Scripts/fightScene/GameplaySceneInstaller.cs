using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private Turns _turns;
    [SerializeField] private PreAttackMovement _preAttackMovement;
    [SerializeField] private SlideDamageSpawner _slideDamageSpawner;
    public override void InstallBindings()
    {
        Container.Bind<Turns>().FromInstance(_turns);
        Container.Bind<PreAttackMovement>().FromInstance(_preAttackMovement);
        Container.Bind<SlideDamageSpawner>().FromInstance(_slideDamageSpawner);
        Container.Bind<CharacterPlacement>().AsSingle();
    }
}
