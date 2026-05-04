using UnityEngine;
using UnityEngine.InputSystem;

public class Projecttile2D : MonoBehaviour
{
    [SerializeField] Transform ShootPoint;
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D bulletPrefab;

    void Update()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {

                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log($"Hit{hit.collider.gameObject.name}");

                Vector2 projectileVelocity = CalculateProjectileVelocity(ShootPoint.position, hit.point, 1f);

                Rigidbody2D ShootBullet = Instantiate(bulletPrefab, ShootPoint.position, Quaternion.identity);

                ShootBullet.linearVelocity = projectileVelocity;
            }
        }


    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float t)
    {
        Vector2 distance = target - origin;
        Vector2 velocity = (distance - 0.5f * Physics2D.gravity * (t * t)) / t;


        return velocity;
    }

}