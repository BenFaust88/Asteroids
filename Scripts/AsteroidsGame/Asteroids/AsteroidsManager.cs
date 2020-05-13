using UnityEngine;
using Asteroids.AsteroidsFactory;

namespace Asteroids.AsteroidsManager {

    public class AsteroidsManager : MonoBehaviour
    {
        [SerializeField] private int _maxNumAsteroids = 10;
        [SerializeField] private float _breakAwayAngle = 45f;

        private AsteroidFactory _asteroidFactory;
        private Asteroid _asteroid;
        private int _numOfCurrentAsteroids = 0;
        private int _asteroidLayer = 10;
        private bool _isInitial = true;

        private void Awake()
        {
            _asteroidFactory = GetComponent<AsteroidFactory>();
            Physics2D.IgnoreLayerCollision(_asteroidLayer, _asteroidLayer);
        }
        private void Start()
        {
            SpawnStartingAsteroid();
        }

        private void Update()
        {
            _numOfCurrentAsteroids = transform.childCount;

            if (_numOfCurrentAsteroids < _maxNumAsteroids)
            {
                Asteroid newAsteroid = _asteroidFactory.SpawnAsteroid(!_isInitial);
                newAsteroid.OnAsteroidDestroyed += BreakUpAsteroid;
            }
        }

        private void BreakUpAsteroid(Asteroid asteroid)
        {
            if (asteroid.Size == 1) return;

            Vector2 asteroidPosition = asteroid.transform.position;
            float asteroidRotationAngle = asteroid.transform.rotation.eulerAngles.z;

            Asteroid newAsteroidA = _asteroidFactory.SpawnAsteroid(asteroid, asteroidRotationAngle - _breakAwayAngle);
            Asteroid newAsteroidB = _asteroidFactory.SpawnAsteroid(asteroid, asteroidRotationAngle + _breakAwayAngle);

            asteroid.OnAsteroidDestroyed -= BreakUpAsteroid;
            newAsteroidA.OnAsteroidDestroyed += BreakUpAsteroid;
            newAsteroidB.OnAsteroidDestroyed += BreakUpAsteroid;
        }

        private void SpawnStartingAsteroid()
        {
            while (_numOfCurrentAsteroids < _maxNumAsteroids)
            {
                _asteroid = _asteroidFactory.SpawnAsteroid(_isInitial);
                _asteroid.OnAsteroidDestroyed += BreakUpAsteroid;

                _numOfCurrentAsteroids = transform.childCount;
            }

            _isInitial = false;
        }
    }
} 