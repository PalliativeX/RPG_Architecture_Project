using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider collider;
        
        private ISaveLoadService _saveLoadService;
        
        public bool Active { get; set; }

        private void Awake()
        {
            Active = true;
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Active)
            {
                _saveLoadService.SaveProgress();
                Debug.Log("Progress Saved!");
                gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            if (!collider)
                return;
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + collider.center, collider.size);
        }
    }
}