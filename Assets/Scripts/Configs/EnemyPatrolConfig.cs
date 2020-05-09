using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PatrolConfig", menuName ="Configs/ Enemy Patrol Movement config")]
    public class EnemyPatrolConfig : ScriptableObject
    {
        [SerializeField] private float patrolRange = 5;
        [SerializeField] private Vector2 waitBeforeNextpatrol = new Vector2(1,5); 
        [SerializeField] private bool patrolRandom = false;

        public float PatrolRange => patrolRange;
        public float WaitBeforeNextpatrol => Random.Range(waitBeforeNextpatrol.x, waitBeforeNextpatrol.y);
        public bool PatrolRandom => patrolRandom;
    }
}
