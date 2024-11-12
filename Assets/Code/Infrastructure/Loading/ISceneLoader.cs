using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Loading
{
    public interface ISceneLoader
    {
        void LoadSceneAsync(Scenes sceneName, Action onLoaded = null);
    }
}