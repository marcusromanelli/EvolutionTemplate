using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Game Library File", menuName = "Template/Game Library", order = 100)]
public class GameLibraryScriptable : ScriptableObjectInstaller<GameLibraryScriptable> {
    public GameLibrary GameLibrary;
    public FarmSettings FarmSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(GameLibrary);
        Container.BindInstance(FarmSettings);
    }
}
