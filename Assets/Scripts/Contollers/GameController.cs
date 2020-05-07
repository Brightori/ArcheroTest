using Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    [DefaultExecutionOrder(-1000)]
    public class GameController : MonoBehaviour
    {
        private List<ICanBePaused> canBePausedObjects = new List<ICanBePaused>(50);

        private void Awake()
        {
            GlobalCommander.Commander.RecieveRegisterObject(this, canBePausedObjects);
            GlobalCommander.Commander.AddListener<SetPauseGlobalCommand>(this, SetPauseReact);
        }

        private void SetPauseReact(SetPauseGlobalCommand obj) => SetPause(obj.State);

        public void SetPause(bool state)
        {
            foreach (var item in canBePausedObjects)
                item.SetPause(state);
        }
    }
}

public class SetPauseGlobalCommand
{
    public bool State;
}