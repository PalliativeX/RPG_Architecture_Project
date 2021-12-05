using Data;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private float movementSpeed;

        private IInputService _inputService;
        private Camera mainCamera;
        private CharacterController _characterController;
        private HeroAnimator _heroAnimator;
        private PositionOnLevel worldDataPositionOnLevel;

        private void Awake()
        {
            mainCamera = Camera.main;

            _inputService = AllServices.Container.Single<IInputService>();

            _characterController = GetComponent<CharacterController>();
            _heroAnimator = GetComponent<HeroAnimator>();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (!_heroAnimator.IsAttacking && _inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = mainCamera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0f;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * (Time.deltaTime * movementSpeed));
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;

            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition != null) 
                Warp(at: savedPosition);
        }

        private void Warp(Vector3Data at)
        {
            _characterController.enabled = false;
            transform.position = at.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}