using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private Turns _turns;
    [SerializeField] private PreAttackMovement _preAttackMovement;
    [SerializeField] private SlideDamageSpawner _slideDamageSpawner;
    [SerializeField] private DefendReference _defendReference;
    [SerializeField] private CharacterPlacement _characterPlacement;
    [SerializeField] private BattleNetwork _battleNetwork;
    [SerializeField] private AbstractPanelProperties _abstractPanelProperties;
    public override void InstallBindings()
    {
        Container.Bind<Turns>().FromInstance(_turns);
        Container.Bind<PreAttackMovement>().FromInstance(_preAttackMovement);
        Container.Bind<SlideDamageSpawner>().FromInstance(_slideDamageSpawner);
        Container.Bind<DefendReference>().FromInstance(_defendReference);
        Container.Bind<CharacterPlacement>().FromInstance(_characterPlacement);
        Container.Bind<BattleNetwork>().FromInstance(_battleNetwork).AsSingle();
        Container.Bind<AbstractPanelProperties>().FromInstance(_abstractPanelProperties);
    }
}
