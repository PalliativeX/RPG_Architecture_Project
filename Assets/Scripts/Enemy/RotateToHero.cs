using UnityEngine;

namespace Enemy
{
    public class RotateToHero : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Transform _heroTransform;
        private Vector3 _positionToLook;

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Update()
        {
            if (_heroTransform)
                RotateTowardsHero();
        }

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDelta = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) => 
            Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 positionToLook) => 
            Quaternion.LookRotation(positionToLook);

        private float SpeedFactor() => 
            speed * Time.deltaTime;
    }
}