using Code.Gameplay.Input.Services;
using Code.Infrastructure.Loading;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInputService();
            BindInfrastructureServices();
            BindCommonServices();
        }
        
        private void BindInputService()
        {
            Container.BindInterfacesTo<InputService>().AsSingle();
        }
        
        private void BindInfrastructureServices()
        {
            Container.BindInterfacesAndSelfTo<BootstrapInstaller>().FromInstance(this).AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
        
        public void Initialize()
        {
            Container.Resolve<ISceneLoader>().LoadSceneAsync(Scenes.Game);
        }
    }
}