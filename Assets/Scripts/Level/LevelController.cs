using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GenerateLevelConfig generateLevelConfig = default;
    private List<GameObject> floor;

    public async void Start()
    {
        Assert.IsNotNull(generateLevelConfig, "нет конфига генерации уровня");
        floor = new List<GameObject>((int)(generateLevelConfig.GetLevelSize().x * generateLevelConfig.GetLevelSize().y));
        await GenerateLevel();
        Debug.Log(generateLevelConfig.GetLevelSize());
        Debug.Log("загрузились");
    }

    public async Task GenerateLevel( )
    {
        float width = 0;
        float height = 0;

        //расставляем пол
        for (int i = 0; i < generateLevelConfig.GetLevelSize().x; i++)
        {
            for (int j = 0; j < generateLevelConfig.GetLevelSize().y; j++)
            {
                var prfb = await generateLevelConfig.GetLevelPrfb(LevelPrefabType.FLOOR, new Vector3(width, 0, height), transform);
                floor.Add(prfb);
                height += generateLevelConfig.Step;
            }         

            width += generateLevelConfig.Step;
            height = 0;
        }

        //расставляем 
        for
    }
}
