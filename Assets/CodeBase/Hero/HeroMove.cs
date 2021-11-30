using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;
        
        private IInputService _inputService;
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
            _inputService = Game.InputService;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = mainCamera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0f;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            characterController.Move(movementVector * (Time.deltaTime * movementSpeed));
        }
    }
}
