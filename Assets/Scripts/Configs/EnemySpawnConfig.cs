using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configs
{
    [CreateAssetMenu(fileName ="EnemysConfig", menuName ="Configs/Enemy config")]
    class EnemySpawnConfig : ScriptableObject
    {
        [SerializeField] private int enemysCount = 3;
        [SerializeField] private EnemyContainer[] enemyContainers = default;

        public EnemyContainer[] EnemyContainers { get => enemyContainers; }

        public int EnemysCount => enemysCount;


        private void OnEnable()
        {
          
        }

        public async Task<GameObject> GetRandomEnemy(Vector3 position)
        {
            if (enemyContainers == null || enemyContainers.Length == 0)
            {
                Debug.LogError("массив противников не заполнен");
                return default;
            }

            var random = Random.Range(0, enemyContainers.Length);
            return await enemyContainers[random].EnemyPrfb.InstantiateAsync(position, Quaternion.identity).Task;
        }
    }
}

[System.Serializable]
public struct EnemyContainer
{
    public string Name;
    public AssetReference EnemyPrfb;
}