using Components;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configs
{
    [CreateAssetMenu(fileName = "Levels config", menuName = "Configs/Levels config")]
    class LoadLevelConfig : ScriptableObject
    {
        [SerializeField] public AssetReference[] levels = default;   

        public async Task<LevelComponent> GetLevelByIndex(int index)
        {
            if (levels == null || levels.Length == 0)
            {
                Debug.LogError("нет уровней");
                return default;
            }

            var loadlvl = await levels[index].InstantiateAsync().Task;
            
            if (loadlvl.TryGetComponent<LevelComponent>(out var levelComponent))
                return levelComponent;
            else
                Debug.LogError("нет компонента уровня у загружаемого уровня " + name);

            return default;
        }
    }
}