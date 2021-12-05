using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerExited;

        private void OnTriggerEnter(Collider other) => 
            TriggerEntered?.Invoke(other);

        private void OnTriggerExit(Collider other) => 
            TriggerExited?.Invoke(other);
    }
}