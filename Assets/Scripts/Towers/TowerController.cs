using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TowerController : MonoBehaviour
    {
        public float Range = 15f;
        public float FireRate;
        private int countdown;
        public GameObject ProjectilePrefab;
        public int Cost = 100;

        // Use this for initialization
        public void Start ()
        {
            countdown = (int) (60f/FireRate);
        }
	
        // Update is called once per frame
        public void Update ()
        {
            GameObject nearestEnemy = FindClosestEnemy();
            if (countdown-- <= 0 && nearestEnemy != null)
            {
                // Workaround for weird bug where towers shoot at origin
                if (nearestEnemy.transform.position != Vector3.zero)
                {
                    countdown = (int) (60f/FireRate);
                    GameObject tempProjectile = (GameObject) Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
                    ProjectileController tempController = tempProjectile.GetComponent<ProjectileController>();
                    Vector3 pos = new Vector3(nearestEnemy.transform.position.x, nearestEnemy.transform.position.y);
                    tempController.Target = pos;
                }
            }
        }

        private GameObject FindClosestEnemy() {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos) {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance && curDistance < Range) {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest;
        }

        public int GetCost()
        {
            return Cost;
        }
    }
}
