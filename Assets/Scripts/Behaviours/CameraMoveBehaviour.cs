using Actors;
using UnityEngine;

namespace Behaviours
{
    class CameraMoveBehaviour : MonoBehaviour 
    {
        [SerializeField] private Vector2 offset = Vector2.zero;
        [SerializeField] private float followSpeed = 2;
        private Player player;

        private void Awake()
        {
            GlobalCommander.Commander.Inject((Player pl) => player = pl);
        }

        public void FixedUpdate()
        {
            if (player == null)
                return;
            Vector3 playerCorrectedPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerCorrectedPos, followSpeed * Time.fixedDeltaTime);
            transform.position += new Vector3(offset.x, 0, offset.y);
        }
    }
}
