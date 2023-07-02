using UnityEngine;

namespace Assets.Scripts.CameraSystem
{
    public class CameraSwitcher : MonoBehaviour
    {
        private CameraFollower _follow;
        private PlayerCamera _free;

        public void EnableFreeCamera()
        {
            _free.enabled = true;
            _follow.enabled = false;
        }

        public void DisableFreeCamera()
        {
            _free.enabled = false;
            _follow.enabled = true;
        }

        private void OnEnable()
        {
            _follow = FindObjectOfType<CameraFollower>(true);
            _free = FindObjectOfType<PlayerCamera>(true);
        }
    }
}