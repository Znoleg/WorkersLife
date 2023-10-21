using System.Collections;
using UnityEngine;

namespace Main.Scripts.Infrastructure.GameBootstrap
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}