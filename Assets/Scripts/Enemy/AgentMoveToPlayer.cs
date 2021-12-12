using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        [SerializeField] private NavMeshAgent agent;

        private const float MinimalDistance = 1f;

        private Transform _heroTransform;

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Update()
        {
            if (_heroTransform && HeroNotReached())
                agent.destination = _heroTransform.position;
        }

        private bool HeroNotReached() =>
            Vector3.Distance(agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}