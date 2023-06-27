using UnityEngine;

namespace Assets.Scripts.CameraSystem
{
    [RequireComponent (typeof(Camera))]
    public class PlayerCamera : MonoBehaviour, IInitializable
    {
        [SerializeField] private bool _isMobile;

        [Space(25)]
        [SerializeField] private float _multiplier = .5f;
        [SerializeField] private float _scrollMultiplier = 5;
        [SerializeField] private float _lerpTime = .5f;
        [SerializeField] private float _minimapMultiplier = 2;

        [SerializeField, Space(25)] private float _minScroll = -15;
        [SerializeField] private float _maxScroll = 15;

        [SerializeField] private Camera _minimap;

        private Camera _main;

        private Vector3 _startPosition;

        private Vector3 _destination;

        private float _scroll;

        [field: SerializeField] public bool EnableInput { get; set; } = true;

        public void Initialize()
        {
            SetDestination(transform.position);

            _main = GetComponent<Camera>();
        }

        public void SetDestination(Vector3 destination)
        {
            _destination = new Vector3(destination.x, destination.y, transform.position.z);
        }

        private void Update()
        {
            if (EnableInput)
            {
                ReadInput();
            }

            MoveCamera();
        }

        private void ReadInput()
        {
            if (_isMobile)
            {
                UseMobile();
            }
            else
            {
                UsePC();
            }
        }

        private void UsePC()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - _startPosition;

                _destination += (new Vector3((transform.forward * -delta.y).x, (transform.forward * -delta.y).z) 
                    + transform.right * -delta.x) * _multiplier;

                _startPosition = Input.mousePosition;
            }

            _scroll -= Input.mouseScrollDelta.y * _scrollMultiplier;
            _scroll = Mathf.Clamp(_scroll, _minScroll, _maxScroll);
        }

        private void UseMobile()
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _startPosition = Input.mousePosition;
                }

                if (Input.GetMouseButton(0))
                {
                    Vector3 delta = Input.mousePosition - _startPosition;

                    _destination += new Vector3(delta.x, 0, delta.y);

                    /*
                    destination += (new Vector3((transform.up * -delta.y).x, 0, (transform.forward * -delta.y).z) 
                        + transform.right * -delta.x) * multiplay * Mathf.Clamp(transform.position.y, 0, 95) / 100;
                    */

                    _startPosition = Input.mousePosition;
                }
            }
            else if (Input.touchCount == 2)
            {
                var touchA = Input.GetTouch(0);
                var touchB = Input.GetTouch(1);

                var touchADircetion = touchA.position - touchA.deltaPosition;
                var touchBDircetion = touchB.position - touchB.deltaPosition;

                var distanceBetwenTouchesPosition = Vector2.Distance(touchA.position, touchB.position);
                var distanceBetwenTouchesDirections = Vector2.Distance(touchADircetion, touchBDircetion);
            }
        }

        private void MoveCamera()
        {
            transform.position = Vector3.Lerp(transform.position, _destination, _lerpTime * Time.deltaTime);

            _main.orthographicSize = _scroll;
            _minimap.orthographicSize = _scroll * _minimapMultiplier;
        }
    }
}