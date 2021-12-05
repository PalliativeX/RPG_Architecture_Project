using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        [SerializeField] private NavMeshAgent agent;

        private const float MinimalDistance = 1f;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        // public void Construct(Transform heroTransform) => 
        //     _heroTransform = heroTransform;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject)
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += OnHeroCreated;
            }
        }

        private void Update()
        {
            if (_heroTransform && HeroNotReached())
                agent.destination = _heroTransform.position;
        }

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private void OnHeroCreated() => 
            InitializeHeroTransform();

        private bool HeroNotReached() =>
            Vector3.Distance(agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}