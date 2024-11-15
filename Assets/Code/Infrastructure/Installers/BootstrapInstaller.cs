﻿using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Features.DragAndDrop.Services;
using Code.Gameplay.Features.Rotating.Services;
using Code.Gameplay.Input.Services;
using Code.Infrastructure.Loading;
using UnityEngine;
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
            BindRotating();
            BindCamera();
            BindDragAndDrop();
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

        private void BindRotating()
        {
            Container.Bind<IRotateWitchMouseDeltaService>().To<RotateWitchMouseDeltaService>().AsTransient();
        }

        private void BindCamera()
        {
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
        }
        
        private void BindDragAndDrop()
        {
            Container.BindInterfacesTo<DragService>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<ISceneLoader>().LoadSceneAsync(Scenes.Game);
        }
    }
}