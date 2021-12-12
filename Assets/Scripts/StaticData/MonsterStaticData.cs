using Logic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "MonsterStaticData", menuName = "StaticData/MonsterStaticData", order = 0)]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        
        [Range(1, 100)]
        public int Hp;
        
        [Range(1f, 30f)]
        public float Damage;
        
        [Range(0.5f, 1f)]
        public float EffectiveDistance;
        
        [Range(0.5f, 1f)]
        public float Cleavage;
        
        public float MoveSpeed;
        
        public GameObject Prefab;
    }
}