using Components;
using Configs;
using GlobalCommander;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LoadLevelConfig loadLevelConfig = default;
    [SerializeField] private float spawnActorsHeight = 1.5f;

    private int currentLvlIndex = 0;
    private LevelComponent currentLevel;

    public FloorComponent[] Floor => currentLevel.floorComponents;

    public async void Awake()
    {
        Commander.RegisterInject<LevelController>(this);
        Assert.IsNotNull(loadLevelConfig, "нет конфига загрузки уровней");
        currentLevel = await loadLevelConfig.GetLevelByIndex(currentLvlIndex);
        Commander.Invoke(new LevelReadyGlobalCommand());
    }

    public Vector3 GetRandomAvailablePosition()
    {
        if (Floor == null || Floor.Length == 0)
        {
            Debug.LogError("нет доступных тайлов в уровне");
            return Vector3.zero;
        }

        var rnd = Random.Range(0, Floor.Length);
        return new Vector3(Floor[rnd].transform.position.x, spawnActorsHeight, Floor[rnd].transform.position.z);
    }
}

public struct LevelReadyGlobalCommand { }
