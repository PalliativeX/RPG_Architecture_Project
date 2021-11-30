using CodeBase.CameraLogic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";
        private const string HeroPath = "Hero/hero";
        private const string HudPath = "Hud/Hud";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Update()
        {
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag(InitialPoint);
            
            GameObject hero = Instantiate(HeroPath, at: initialPoint.transform.position);
            Instantiate(HudPath, Vector3.zero);

            SetCameraFollow(hero);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private static GameObject Instantiate(string path, Vector3 at)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab, at, Quaternion.identity);
        }

        private void SetCameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}