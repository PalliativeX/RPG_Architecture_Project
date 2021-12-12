using UnityEngine;

namespace Logic
{
    public class UniqueId : MonoBehaviour
    {
        [SerializeField] private string id;

        public string ID
        {
            get => id;
            set => id = value;
        }
    }
}