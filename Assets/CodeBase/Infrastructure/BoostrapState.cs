using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BoostrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BoostrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            Game.InputService = SetupInputService();
        }

        private static IInputService SetupInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

        private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>("Main");
    }
}