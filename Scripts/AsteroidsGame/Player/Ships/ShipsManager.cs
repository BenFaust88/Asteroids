using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Player.Ships {

    public class ShipsManager : MonoBehaviour
    {
        public List<GameObject> ShipType;

        public Transform GiveShip(ShipType shipType)
        {
            return Instantiate(ShipType[(int)shipType]).transform;
        }
    }

    public enum ShipType
    {
        Default
    }
}