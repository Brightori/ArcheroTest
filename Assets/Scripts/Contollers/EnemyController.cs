using Boo.Lang;
using Configs;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemySpawnConfig spawnConfig = default;
    private LevelController levelController { get; set; } = default;
    public List<GameObject> enemys = new List<GameObject>(10);

    private void Awake()
    {
        GlobalCommander.Commander.RegisterInject<EnemyController>(this);
        GlobalCommander.Commander.Inject((LevelController ctr) => levelController = ctr);
        GlobalCommander.Commander.AddListener<LevelReadyGlobalCommand>(this, LevelReadyReact);
    }

    public Vector3 GetClosestEnemy(Vector3 playerSide) //в принципе ближайший враг может быть интересен не только игроку, но и пету на его стороне
    {

        return default;
    }

    private async void LevelReadyReact(LevelReadyGlobalCommand obj)
    {
        for (int i = 0; i < spawnConfig.EnemysCount; i++)
        {
            var enemy = await spawnConfig.GetRandomEnemy(levelController.GetRandomAvailablePosition());
            enemys.Add(enemy);
        }
    }
}