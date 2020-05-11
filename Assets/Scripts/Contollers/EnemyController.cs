using Configs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

[DefaultExecutionOrder(-1)]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemySpawnConfig spawnConfig = default;
    private LevelController levelController { get; set; } = default;
    public List<GameObject> enemys = new List<GameObject>(10);

    public IEnumerable<GameObject> sorted = new List<GameObject>(100);

    private void Awake()
    {
        GlobalCommander.Commander.RegisterInject<EnemyController>(this);
        GlobalCommander.Commander.Inject((LevelController ctr) => levelController = ctr);
        GlobalCommander.Commander.AddListener<LevelReadyGlobalCommand>(this, LevelReadyReact);
        GlobalCommander.Commander.RecieveRegisterObject(this, enemys);
    }

    public bool TryGetClosestEnemy(Vector3 from, out Vector3 closestEnemyCoord ) //в принципе ближайший враг может быть интересен не только игроку, но и пету на его стороне
    {
        closestEnemyCoord = Vector3.zero;

        if (enemys.Count == 0)
            return false;

        sorted = enemys.OrderBy(x => Vector3.Distance(from, x.transform.position));

        var closestEnemy = sorted.FirstOrDefault();

        if (closestEnemy != null)
        {
            closestEnemyCoord = closestEnemy.transform.position;
            return true;
        }

        return false;
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