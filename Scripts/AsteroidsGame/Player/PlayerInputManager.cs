using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Asteroids.PlayerManager {

    public class PlayerInputManager : MonoBehaviour
    {
        public int thrust = 4000;
        public float torque = 8f;
        public float rotateSpeed = 10f;

        private Rigidbody2D _spaceship;
        private WeaponController _weaponController;

        private Vector2 _joystickPosition;
        private bool _isFiring1 = false;

        private void Start()
        {
            _spaceship = GetComponentInChildren<Rigidbody2D>();
            _weaponController = GetComponentInChildren<WeaponController>();
        }

        private void Update()
        {
            PollInput();
            FireWeapons();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void PollInput()
        {
            _joystickPosition.x = CrossPlatformInputManager.VirtualAxisReference("Vertical").GetValue;
            _joystickPosition.y = CrossPlatformInputManager.VirtualAxisReference("Horizontal").GetValue * -1f;

            if (CrossPlatformInputManager.GetButtonDown("Fire1") || CrossPlatformInputManager.GetButton("Fire1"))
                _isFiring1 = true;

            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
                _isFiring1 = false;
        }

        private void MovePlayer()
        {
            float joystickAngle = Vector2.SignedAngle(Vector2.right, _joystickPosition);

            if (_joystickPosition.magnitude > 0)
            {
                _spaceship.transform.rotation = Quaternion.Lerp(_spaceship.transform.rotation, 
                                                                Quaternion.Euler(0f, 0f, joystickAngle),
                                                                rotateSpeed * Time.fixedDeltaTime);

                _spaceship.AddRelativeForce(Vector2.up * _joystickPosition.magnitude * thrust * Time.fixedDeltaTime);
            }
        }

        private void FireWeapons()
        {
            if (_isFiring1)
                _weaponController.FirePrimary();
        }
    }
}