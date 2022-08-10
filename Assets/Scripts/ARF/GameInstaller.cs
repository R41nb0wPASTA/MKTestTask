using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        
        Container.DeclareSignal<ARModuleWorkStartSignal>();
        Container.DeclareSignal<ARObjectPlacedSignal>();
        
        Container.Bind<ARWorldController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<AnimationController_Ingosick>().FromComponentInHierarchy().AsSingle();

        Container.BindSignal<ARModuleWorkStartSignal>()
            .ToMethod<AnimationController_Ingosick>(x => x.OnARModuleStart).FromResolve();

        Container.BindSignal<ARObjectPlacedSignal>()
            .ToMethod<AnimationController_Ingosick>(x => x.StartPlaying).FromResolve();

        Container.BindInterfacesTo<SignalController>().AsSingle();
    }
}

public class ARModuleWorkStartSignal
{
    public GameObject goToPlace;
}

public class ARObjectPlacedSignal { }