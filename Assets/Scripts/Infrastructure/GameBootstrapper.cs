using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain curtainPrefab;
        
        private Game _game;
        
        private void Awake()
        {
            _game = new Game(this, Instantiate(curtainPrefab));
            _game.StateMachine.Enter<BoostrapState>();
            
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _game.StateMachine.Update();
        }
    }
}