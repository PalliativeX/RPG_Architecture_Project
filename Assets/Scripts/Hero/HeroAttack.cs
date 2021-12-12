using Data;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Logic;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    [RequireComponent(typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator heroAnimator;
        [SerializeField] private CharacterController characterController;

        private static readonly Collider[] Hits = new Collider[3];
        
        private IInputService _input;

        private static int _layerMask;
        private Stats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp() && !heroAnimator.IsAttacking)
            {
                heroAnimator.PlayAttack();
            }
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                Hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, Hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, characterController.center.y / 2f, transform.position.z);

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }
    }
}