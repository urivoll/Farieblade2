using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private AbstractPanelProperties _abstractPanelProperties;

    public override void InstallBindings()
    {
        print("1");
        Container.Bind<AbstractPanelProperties>().FromInstance(_abstractPanelProperties);
    }
}
