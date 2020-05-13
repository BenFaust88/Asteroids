using UnityEngine;
using Asteroids.Player.Ships;

namespace Asteroids.PlayerManager  {

    [RequireComponent(typeof(ShipsManager))]
    [RequireComponent(typeof(WeaponController))]
    public class PlayerManager : MonoBehaviour
    {
        //Current Ship
        //Current Lives
        //Current Score
        //Current Level

        public int lives;
        public int score;
        public int level;

        private ShipsManager _ship;
        private WeaponController _weaponController;

        private void Awake()
        {
            _ship = GetComponent<ShipsManager>();
            _weaponController = GetComponent<WeaponController>();
        }

        void Start()
        {
            //ship = Instantiate(Resources.Load("Prefab/DefaultShipPrefab") as GameObject, this.transform);
            //ship.AddComponent<Ship>();

            lives = 3;
            score = 0;
            level = 1;
        }

        void Update()
        {

        }
    
        public Transform SpawnPlayer(string name, bool isNewPlayer = false)
        {
            GameObject retPlayer = new GameObject(name);

            if(isNewPlayer)
            {
                _ship.GiveShip(ShipType.Default).parent = retPlayer.transform;
            }
            return retPlayer.transform;
        }
    }
}