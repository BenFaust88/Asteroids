using System;
using UnityEngine;

namespace Asteroids {

    public class Asteroid : MonoBehaviour
    {
        //Destroyed Event
        public event Action<Asteroid> OnAsteroidDestroyed;

        private Rigidbody2D _rigidbody;
        private int _size { get; set; } = 1;
        private float _thrust { get; set; }
        private int _health = 5;

        private void Start()
        {
            _health *= _size;

            transform.localScale *= _size;

            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddRelativeForce(Vector2.up * _thrust);
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public float Thrust
        {
            get { return _thrust; }
            set { _thrust = value; }
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
                AsteroidDestroyed();
        }

        private void AsteroidDestroyed()
        {
            OnAsteroidDestroyed?.Invoke(this);

            Destroy(gameObject);
        }
    }
}