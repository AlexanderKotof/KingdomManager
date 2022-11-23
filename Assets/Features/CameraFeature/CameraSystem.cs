using KM.Startup;
using UnityEngine;

namespace KM.Features.CameraFeature
{
    public class CameraSystem : ISystem, ISystemUpdate
    {
        public Camera MainCamera { get; private set; }
        public Transform CameraParentTransform { get; private set; }

        private GameObject _cameraPrefab;
        private Vector3 _targetPosition;
        private float _moveSpeed;

        public CameraSystem(GameObject cameraPrefab, float moveSpeed)
        {
            _cameraPrefab = cameraPrefab;
            _moveSpeed = moveSpeed;
        }

        public void Initialize()
        {
            CameraParentTransform = GameObject.Instantiate(_cameraPrefab).transform;
            MainCamera = CameraParentTransform.GetComponentInChildren<Camera>();

            _targetPosition = CameraParentTransform.position;
        }

        public void Destroy()
        {

        }

        public void MoveCamera(Vector3 delta)
        {
            _targetPosition += delta * 0.1f;
        }

        public void Update()
        {
            if (CameraParentTransform.position != _targetPosition)
            {
                CameraParentTransform.position = Vector3.Lerp(CameraParentTransform.position, _targetPosition, _moveSpeed * Time.deltaTime);
            }
        }
    }
}