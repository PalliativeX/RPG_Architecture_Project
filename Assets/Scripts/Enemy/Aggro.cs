using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private AgentMoveToPlayer follow;

        [SerializeField] private float cooldown;
        
        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            triggerObserver.TriggerEntered += OnTriggerEntered;
            triggerObserver.TriggerExited += OnTriggerExited;

            SwitchFollowOff();
        }

        private void OnTriggerEntered(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAggroCoroutine();

                SwitchFollowOn();
            }
        }

        private void OnTriggerExited(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return  new WaitForSeconds(cooldown);
            SwitchFollowOff();
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private void SwitchFollowOn() => 
            follow.enabled = true;
        private void SwitchFollowOff() => 
            follow.enabled = false;
    }
}