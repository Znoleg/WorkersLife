using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.Services.ServiceLocator
{
    public interface IService
    {
    }

    public class AllServices
    {
        private static AllServices _instance;
        
        public static AllServices Container => _instance ??= new AllServices();

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}