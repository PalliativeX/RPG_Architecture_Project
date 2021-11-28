using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game game;
        
        private void Awake()
        {
            game = new Game();
            
            DontDestroyOnLoad(this);
        }
    }
}