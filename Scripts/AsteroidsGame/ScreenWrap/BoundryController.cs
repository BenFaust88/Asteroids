using UnityEngine;


namespace Asteroids.AsteroidsScreenWrap {

    public class BoundryController : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<ScreenWrapper>() == null)
                return;
            
            Vector2 collisionPosition = collision.transform.position;
            float absCollisionX = Mathf.Abs(collisionPosition.x);
            float absCollisionY = Mathf.Abs(collisionPosition.y);

            if (absCollisionX >= ScreenWrapManager.screenBounds.x)
            {
                if (absCollisionY >= ScreenWrapManager.screenBounds.y)
                {
                    collision.transform.position = new Vector2(collisionPosition.x * -1, collisionPosition.y * -1);
                    return;
                }

                collision.transform.position = new Vector2(collisionPosition.x * -1, collisionPosition.y);
            }
            else
            {
                collision.transform.position = new Vector2(collisionPosition.x, collisionPosition.y * -1);
            }
        }
    }
}