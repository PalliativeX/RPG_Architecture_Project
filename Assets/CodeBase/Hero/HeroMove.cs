using System;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;
        
        private IInputService inputService;
        private Camera camera;

        private void Awake()
        {
            inputService = Game.InputService; 
        }

        private void Start()
        {
            camera = Camera.main;
            SetCameraFollow();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = camera.transform.TransformDirection(inputService.Axis);
                movementVector.y = 0f;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            characterController.Move(movementVector * (Time.deltaTime * movementSpeed));
        }

        private void SetCameraFollow() => 
            camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}
