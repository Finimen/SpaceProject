using UnityEngine;

namespace Assets.Scripts.CameraSystem
{
    public class PlayerCamera : MonoBehaviour, IInitializable
    {
        [SerializeField] private bool _isMobile;

        [Space(25)]
        [SerializeField] private float _multiplier = .5f;
        [SerializeField] private float _scrollMultiplier = 5;
        [SerializeField] private float _lerpTime = .5f;

        [SerializeField, Space(25)] private float _minScroll = -15;
        [SerializeField] private float _maxScroll = 15;

        private Vector3 _startPosition;
        private Vector3 _scroll;

        private Vector3 _destination;

        [field: SerializeField] public bool EnableInput { get; set; } = true;

        void IInitializable.Initialize()
        {
            SetDestination(transform.position);
        }

        public void SetDestination(Vector3 destination)
        {
            _destination = destination;
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

            _scroll -= (Vector3)Input.mouseScrollDelta * _scrollMultiplier;
            _scroll = new Vector3(_scroll.x, Mathf.Clamp(_scroll.y, _minScroll, _maxScroll), _scroll.z);
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

                _scroll += Vector3.up * (distanceBetwenTouchesDirections - distanceBetwenTouchesPosition) * .01f;
                _scroll = new Vector3(_scroll.x, Mathf.Clamp(_scroll.y, _minScroll, _maxScroll), _scroll.z);
            }
        }

        private void MoveCamera()
        {
            transform.position = Vector3.Lerp(transform.position, _destination, _lerpTime * Time.deltaTime);
        }
    }
}