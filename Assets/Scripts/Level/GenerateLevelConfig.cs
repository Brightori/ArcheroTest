using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "LevelPrfbConfig", menuName = "Configs / Prefabs for level generation")]
public class GenerateLevelConfig : ScriptableObject
{
    [Header("Размер игрового поля, ширина высота, с учётом стенок")]
    [SerializeField] private Vector2 fieldSize = new Vector2(14, 10);

    [Header("флаг, мы делаем отдельную центральную вертикаль для симметрирочности или нет")]
    [SerializeField] private bool IsHaveCentralVertical = true;

    [Header("сюда кладем ссылки на префабы из которых строится уровень")]
    [SerializeField] private LevelPrefabsReference[] levelPrefabsReferences = default;

    [Header("шаг с которым строим сетку")]
    [SerializeField] private float step = 1;

    [Header("Высота по Y для мобов")]
    [SerializeField] private float spawnActorsHeight = 1.5f;

    public float Step => step;
    public float SpawnActorsHeight => spawnActorsHeight;


    public void GetLevelPrfb(LevelPrefabType levelPrefabType, Vector3 coord, Transform parent)
    {
        var prfb = PrefabUtility.InstantiatePrefab(levelPrefabsReferences.FirstOrDefault(x => x.LevelPrefabType == levelPrefabType).assetReference, parent);
        (prfb as GameObject).transform.position = coord;
    }

    //тут проверяем размер карты и отдаем с учётом вертикальной линии для персонажа 
    public Vector2 GetLevelSize()
    {
        if (IsHaveCentralVertical)
        {
            if (fieldSize.x % 2 == 0)
                return new Vector2(fieldSize.x+1, fieldSize.y);
        }

        return fieldSize;
    }
}

[System.Serializable]
public struct LevelPrefabsReference
{
    public string Name;
    public LevelPrefabType LevelPrefabType;
    public GameObject assetReference;
}

public enum LevelPrefabType
{
    DEFAULT = 0,
    FLOOR = 1,
    OBSTACLE = 2,
    WALL = 3,
    EXIT = 4,
}