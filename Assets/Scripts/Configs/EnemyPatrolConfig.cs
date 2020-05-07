using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PatrolConfig", menuName ="Configs/ Enemy Patrol Movement config")]
    public class EnemyPatrolConfig : ScriptableObject
    {
        [SerializeField] private float patrolRange = 5;
        [SerializeField] private float waitBeforeNextpatrol = 1; 
        [SerializeField] private bool patrolRandom = false;

        public float PatrolRange => patrolRange;
        public float WaitBeforeNextpatrol => waitBeforeNextpatrol;
        public bool PatrolRandom => patrolRandom;
    }
}
