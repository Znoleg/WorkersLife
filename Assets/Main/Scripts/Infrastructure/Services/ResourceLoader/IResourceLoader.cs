using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.ResourceLoader
{
    public interface IResourceLoader : IService
    {
        Task<T> LoadAsync<T>(string assetPath) where T : Object;
        T Load<T>(string assetPath) where T : Object;
    }
}