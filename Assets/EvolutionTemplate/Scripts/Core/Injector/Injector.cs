using Zenject;

/// <summary>
/// To avoid the use of singletons and maintain a project-wide communication, I choosed the IoC injection method.
/// Here I choose which classes are going to be injectable.
/// Also I setup a factoring system (using the same library) to generate procedural GameObjects if needed (should have linked it to a pooling system, but time ran out)
/// </summary>
public class Injector : MonoInstaller {

    [Inject]
    GameLibrary _gameLibrary;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EventHandler>().AsSingle();

        Container.BindInterfacesAndSelfTo<InputController>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerDataController<RecordsController, SkinController, CurrencyController, WalletController<CurrencyController>>>().AsSingle();

        Container.BindInterfacesAndSelfTo<FarmController>().AsSingle();

        //Game Elements
        Container.BindFactory<EvolutionElement, EvolutionElement.Factory>()
            .FromComponentInNewPrefab(_gameLibrary.FactoryTemplates.EvolutionElementPrefab)
            .WithGameObjectName("Element")
            .UnderTransformGroup("FarmElements");

        Container.BindFactory<ElementPoop, ElementPoop.Factory>()
            .FromComponentInNewPrefab(_gameLibrary.FactoryTemplates.PoopPrefab)
            .WithGameObjectName("Poop")
            .UnderTransformGroup("FarmElements");


        //UI Elements
        Container.BindFactory<StoreItemObject, StoreItemObject.Factory>()
            .FromComponentInNewPrefab(_gameLibrary.FactoryTemplates.StoreObjectPrefab)
            .WithGameObjectName("StoreObject");

        Container.BindFactory<SkinObject, SkinObject.Factory>()
            .FromComponentInNewPrefab(_gameLibrary.FactoryTemplates.SkinObjectPrefab)
            .WithGameObjectName("SkinObject");

        Container.BindFactory<StatsItem, StatsItem.Factory>()
            .FromComponentInNewPrefab(_gameLibrary.FactoryTemplates.StatsObjectPrefab)
            .WithGameObjectName("StatsItem");



        Container.BindExecutionOrder<EventHandler>(-20);
        Container.BindExecutionOrder<IPlayerDataController>(-10);
        Container.BindExecutionOrder<FarmController>(-5);
    }
}
