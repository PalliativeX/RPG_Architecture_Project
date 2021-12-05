using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null)
        {
            coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }
        
        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}