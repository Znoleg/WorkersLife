using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.ResourceLoader
{
    public class ResourceLoader : IResourceLoader
    {
        private readonly Dictionary<string, Object> _objectsCache = new();

        public async Task<T> LoadAsync<T>(string assetPath) where T : Object
        {
            if (_objectsCache.ContainsKey(assetPath))
            {
                return _objectsCache[assetPath] as T;
            }
            
            ResourceRequest request = Resources.LoadAsync<T>(assetPath);
            while (!request.isDone)
            {
                await Task.Yield();
            }

            _objectsCache.Add(assetPath, request.asset);
            return request.asset as T;
        }

        public T Load<T>(string assetPath) where T : Object
        {
            if (_objectsCache.ContainsKey(assetPath))
            {
                return _objectsCache[assetPath] as T;
            }

            T asset = Resources.Load<T>(assetPath);
            _objectsCache.Add(assetPath, asset);
            return asset;
        }
    }
}