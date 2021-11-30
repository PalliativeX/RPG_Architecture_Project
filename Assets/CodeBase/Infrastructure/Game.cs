using CodeBase.Services.Input;
using CodeBase.UI;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain);
        }

        public void Update()
        {
            StateMachine.Update();
        }
        

        
    }
}