using System;
using Unity.Collections;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float rotationAngleX;
        [SerializeField] private float distance;
        [SerializeField] private float offsetY;

        [ReadOnly]
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            if (!target)
                return;

            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0f, 0f);

            Vector3 position = rotation * new Vector3(0f, 0f, -distance) + FollowingPointPosition();

            transform.SetPositionAndRotation(position, rotation);
        }

        public void Follow(GameObject following)
        {
            target = following.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = target.position;
            followingPosition.y += offsetY;
            
            return followingPosition;
        }
    }
}