using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] public GameObject DestroyObjectPrefab = null;
        public Vector3 Target;
        public float Speed;
        public int Damage;

        // Use this for initialization
        protected void Start () {
        }
	
        // Update is called once per frame
        protected void Update () {
            transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
            Vector3 moveDirection = Target - gameObject.transform.position; 

            // Rotate projectile
            if (moveDirection != Vector3.zero) 
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            if (transform.position == Target)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyController enemy;
            enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Health -= Damage;
                if (DestroyObjectPrefab != null)
                {
                    Instantiate(DestroyObjectPrefab, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }
}
