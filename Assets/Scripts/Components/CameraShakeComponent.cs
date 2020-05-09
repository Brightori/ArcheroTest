using DG.Tweening;
using System;
using UnityEngine;

namespace Components
{
    public class CameraShakeComponent : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GlobalCommander.Commander.AddListener<ShakeCameraGlobalCommand>(this, ShakeCameraReact);
        }

        private void ShakeCameraReact(ShakeCameraGlobalCommand obj)
        {
            transform.DOShakePosition(0.5f, 1, 5);
        }
    }

    public struct ShakeCameraGlobalCommand { }
}