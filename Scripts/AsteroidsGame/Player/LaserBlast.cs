using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids { 

    public class LaserBlast : MonoBehaviour
    {
        public float velocity = 2500f;
        public int maxRange = 100;

        public Rigidbody2D laserRB;
        public Collider2D laserCollider;
        public Vector2 startPos, currentPos;

        // Start is called before the first frame update
        void Start()
        {
            laserRB = this.GetComponent<Rigidbody2D>();
            laserCollider = this.GetComponent<Collider2D>();
            startPos = this.transform.position;

            laserRB.AddRelativeForce(Vector2.up * velocity);
        }

        void FixedUpdate()
        {
            currentPos = this.transform.position;

            if(Vector2.Distance(startPos, currentPos) > maxRange)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.name == "ScreenBoundry" || collider.tag == "Player")
                return;

            collider.gameObject.GetComponent<Asteroid>()?.TakeDamage(5);

            Destroy(this.gameObject);
        }
    }
}