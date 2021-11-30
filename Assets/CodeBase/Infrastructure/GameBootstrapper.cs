using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain curtain;
        
        private Game _game;
        
        private void Awake()
        {
            _game = new Game(this, curtain);
            _game.StateMachine.Enter<BoostrapState>();
            
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _game.StateMachine.Update();
        }
    }
}