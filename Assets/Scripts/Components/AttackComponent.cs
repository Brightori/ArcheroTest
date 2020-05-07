using HECS.Controllers;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Components
{
    [DefaultExecutionOrder(100)]
    public class AttackComponent : MonoBehaviour, ICanAttack
    {
        [SerializeField] private float dmg = 30;
        [SerializeField] private float attackInterval = 0.3f;
        [SerializeField] private AssetReference projectile = default;
        [SerializeField] private float attackMoveSpeed = 3;

        public float Dmg => dmg;
        public float AttackInterval => attackInterval;

        //public bool IsReadyForAttack { get; private set; }
        public bool IsReadyForAttack => InputController.Instance.HorizontalMove == 0 && InputController.Instance.VerticalMove == 0;
        public float AttackMoveSpeed => attackMoveSpeed;
        public Vector3 ShootPosition => transform.position;  //TODO сюда прописать в итоге оружие в руках

        public Transform Transform => transform;

        private void Awake()
        {
            projectile.LoadAssetAsync<GameObject>(); // сразу загружаем в память, отсюда будет очень быстро доставаться
        }

        //TODO дописать сюда pull
        public async Task<GameObject> GetProjectile(Vector3 position)
        {
            return await projectile.InstantiateAsync(position, Quaternion.identity).Task;
        }
    }
}
