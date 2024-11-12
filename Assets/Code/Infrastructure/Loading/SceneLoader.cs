using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
    public class SceneLoader : ISceneLoader, IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;
        
        public SceneLoader()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        public async void LoadSceneAsync(Scenes sceneName, Action onLoaded = null) 
            => await Load(sceneName, onLoaded);

        private async UniTask Load(Scenes nextScene, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == nextScene.ToString())
            {
                onLoaded?.Invoke();
                return;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene.ToString());
            
            while (!waitNextScene.isDone)
            {
                _cancellationToken.ThrowIfCancellationRequested();
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            
            if (!_cancellationToken.IsCancellationRequested)
            {
                await waitNextScene;
                onLoaded?.Invoke();
            }
            else
            {
                Debug.Log("Scene load canceled");
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}