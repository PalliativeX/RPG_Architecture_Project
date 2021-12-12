using UnityEngine;

namespace UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera mainCamera;

        private void Start() => 
            mainCamera = Camera.main;

        private void Update()
        {
            Quaternion rotation = mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
        
    }
}