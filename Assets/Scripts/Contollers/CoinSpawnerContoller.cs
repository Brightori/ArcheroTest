using Actors;
using Commands;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CoinSpawnerContoller : MonoBehaviour
{
    [SerializeField] private AssetReference coin = default;

    private void Awake()
    {
        coin.LoadAssetAsync<GameObject>();
        GlobalCommander.Commander.AddListener<SpawnCoinGlobalCommand>(this, React);
    }

    private void React(SpawnCoinGlobalCommand obj)
    {
        for (int i = 0; i < obj.CoinCount; i++)
            coin.InstantiateAsync(obj.SpawnPosition, Quaternion.identity).Completed += CoinSpawn;
    }

    private void CoinSpawn(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Result.TryGetComponent<IActor>(out var actor))
            actor.Command(new AddRandomDirectionCommand());
    }

    public struct SpawnCoinGlobalCommand
    {
        public Vector3 SpawnPosition;
        public int CoinCount;
    }
}