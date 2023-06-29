using Assets.Scripts.GeneratorSystem;
using Assets.Scripts.TreadingSystem;
using UnityEngine;

namespace Assets.Scripts.TrainingSystem
{
    public class Training : MonoBehaviour
    {
        public enum TrainingState
        {
            Start,
            MovingInfo,
            MovingStarted,
            ResourcesInfo,
            ResourcesStarted,
            TreadingIndo,
            TreadingStarted,
            WeaponInstallationInfo,
            WeaponInstallationStarted,
            RadarAndEnemyInfo,
            TrainingCompleted
        }

        [SerializeField] private TrainingState _currentState;

        [Space(25)]
        [SerializeField] private TrainingModule _moving;
        [SerializeField] private TrainingModule _resourcesCollecting;
        [SerializeField] private TrainingModule _training;
        [SerializeField] private TrainingModule _weaponInstallation;
        
        [Space(25)]
        [SerializeField] private GameObject _startInfo;
        [SerializeField] private GameObject _movingInfo;
        [SerializeField] private GameObject _resourcesInfo;
        [SerializeField] private GameObject _treadingInfo;
        [SerializeField] private GameObject _weaponInstallationInfo;
        [SerializeField] private GameObject _radarAndEnemyInfo;
        [SerializeField] private GameObject _radarCanvas;

        public void SetState(TrainingState state)
        {
            _currentState = state;

            UpdateStateLogic();
        }

        public void NextState()
        {
            _currentState = (TrainingState)((int)(_currentState) + 1);

            UpdateStateLogic();
        }

        private void UpdateStateLogic()
        {
            switch (_currentState)
            {
                case TrainingState.Start:
                    _startInfo.SetActive(true); 
                    break;

                case TrainingState.MovingInfo:
                    _startInfo.SetActive(false);
                    _movingInfo.SetActive(true);
                    break;

                case TrainingState.MovingStarted:
                    _movingInfo.SetActive(false);
                    _moving.Enable();
                    break;

                case TrainingState.ResourcesInfo:
                    _resourcesInfo.SetActive(true);
                    break;

                case TrainingState.ResourcesStarted:
                    _resourcesInfo.SetActive(false);
                    _resourcesCollecting.Enable();
                    break;

                case TrainingState.TreadingIndo:
                    _treadingInfo.SetActive(true);
                    break;

                case TrainingState.TreadingStarted:
                    _treadingInfo.SetActive(false);
                    _training.Enable();
                    break;

                case TrainingState.WeaponInstallationInfo:
                    _weaponInstallationInfo.SetActive(true);
                    break;

                case TrainingState.WeaponInstallationStarted:
                    _weaponInstallationInfo.SetActive(false);
                    _weaponInstallation.Enable();
                    break;

                case TrainingState.RadarAndEnemyInfo:
                    _radarAndEnemyInfo.SetActive(true);
                    break;

                case TrainingState.TrainingCompleted:
                    _radarAndEnemyInfo.SetActive(false);
                    gameObject.SetActive(false);
                    _radarCanvas.SetActive(true);

                    FindObjectOfType<WorldGenerator>().StartGenerating();
                    break;
            }
        }

        private void OnEnable()
        {
            UpdateStateLogic();
        }
    }
}