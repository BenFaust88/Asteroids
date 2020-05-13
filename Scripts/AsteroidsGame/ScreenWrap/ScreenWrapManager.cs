using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.AsteroidsScreenWrap {

    [RequireComponent(typeof(Camera))]
    public class ScreenWrapManager : MonoBehaviour
    {
        public static Vector2 screenBounds;
        public GameObject boundryGO;

        private Camera _camera;
        private int _currentWidth, _currentHeight;

        private void Awake()
        {
            _camera = GetComponent<Camera>();

            CheckScreenSize();
        }

        private void FixedUpdate()
        {
            CheckScreenSize();
        }

        void CheckScreenSize()
        {
            if (Screen.width != _currentWidth || Screen.height != _currentHeight)
            {
                _currentWidth = Screen.width;
                _currentHeight = Screen.height;

                screenBounds = _camera.ScreenToWorldPoint(new Vector3(_currentWidth, _currentHeight, _camera.transform.position.z));
            }
            SetBoundry();
        }

        void SetBoundry()
        {
            Transform screenBoundryTransform = boundryGO.transform;

            screenBoundryTransform.localScale = (new Vector2(screenBounds.x, screenBounds.y) * 2);
        }
    }
}