using UnityEngine;
using Asteroids.AsteroidsScreenWrap;

namespace Asteroids.AsteroidsFactory {

    public class AsteroidFactory : MonoBehaviour
    {
        //[SerializeField] private float _asteroidSizeScale = 0.75f;
        [SerializeField] private float _minSpawnRange = 10;
        [SerializeField] private float _maxSpawnRangeOffset = 5;
        [SerializeField] private float _offsetFromScreenEdge = 2f;
        [SerializeField] private int[] _asteroidSizeRng = { 1, 3 };
        [SerializeField] private int[] _trustRng = { 300, 600 };

        //public GameObject asteroidSpawnPoint;
        public GameObject _asteroidPrefab;

        private Transform _parentTransform;
        private Vector2 _asteriodPosition;
        private Vector2 _contactPoint;

        private void Awake()
        {
            _parentTransform = transform;
        }

        public Asteroid SpawnAsteroid(bool isInitial)
        {
            int size = Random.Range(_asteroidSizeRng[0], _asteroidSizeRng[1] + 1);
            int thrust = Random.Range(_trustRng[0] * size, _trustRng[1] * size);

            Vector2 position = SpawnPosition(isInitial);
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

            Transform tempTransform = Instantiate(_asteroidPrefab, position, rotation, _parentTransform).transform;
            Asteroid newAsteroid = tempTransform.GetComponent<Asteroid>();
            
            newAsteroid.Size = size;
            newAsteroid.Thrust = thrust;

            return newAsteroid;
        }

        public Asteroid SpawnAsteroid(Asteroid asteroid, float breakAwayAngle)
        {
            int size = asteroid.Size - 1;
            int thrust = Random.Range(_trustRng[0] * size, _trustRng[1] * size);

            Vector2 position = asteroid.transform.position;
            Quaternion rotation = Quaternion.Euler(0f, 0f, breakAwayAngle);

            Transform tempTransform = Instantiate(_asteroidPrefab, position, rotation, _parentTransform).transform;

            Asteroid newAsteroid = tempTransform.GetComponent<Asteroid>();
            
            newAsteroid.Size = size;
            newAsteroid.Thrust = thrust;

            return newAsteroid;
        }

        private Vector2 SpawnPosition(bool isInitial = false)
        {
            Vector2 screenBounds = ScreenWrapManager.screenBounds;

            float screenBoundDistance = Vector2.Distance(Vector2.zero, screenBounds);
            float maxSpawnDistance = screenBoundDistance + _maxSpawnRangeOffset;
            float randomAngle = Random.Range(0f, 360f);
            Vector3 eulerFromRandomAngle = new Vector3(0f, 0f, randomAngle);

            float distanceToSpawnPoint;
            Vector2 clampRange;
            Vector2 retVector;

            //Initial spawn points are minSpawnRange away from center to screenbounds
            if (isInitial)
            {
                distanceToSpawnPoint = Random.Range(_minSpawnRange, maxSpawnDistance);

                clampRange.x = screenBounds.x - _offsetFromScreenEdge;
                clampRange.y = screenBounds.y - _offsetFromScreenEdge;
            }
            //Additional spawn points are beyond the screen edge*
            else
            {
                distanceToSpawnPoint = Random.Range(screenBoundDistance, maxSpawnDistance);

                clampRange.x = screenBounds.x - _offsetFromScreenEdge; // + _maxSpawnRangeOffset;
                clampRange.y = screenBounds.y - _offsetFromScreenEdge; // + _maxSpawnRangeOffset;
            }

            retVector.x = Mathf.Clamp(Mathf.Sin(randomAngle) * distanceToSpawnPoint, -clampRange.x, clampRange.x);
            retVector.y = Mathf.Clamp(Mathf.Cos(randomAngle) * distanceToSpawnPoint, -clampRange.y, clampRange.y);

            return retVector;
        }
    }
}