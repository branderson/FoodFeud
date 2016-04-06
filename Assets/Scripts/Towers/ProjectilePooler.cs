using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class ProjectilePooler : MonoBehaviour
    {
        // Cached instance of LevelController for lookup
        private static ProjectilePooler instanceRef = null;

        [SerializeField] private GameObject sodaProjectileObject;
        private static List<GameObject> sodaProjectilePool;
        [SerializeField] private static int startingPoolSize;

        // Instantiate singleton at start of scene
        public static ProjectilePooler instance {
            get {
                if (instanceRef == null) {
                    // This is where the magic happens.
                    //  FindObjectOfType(...) returns the first ProjectilePooler object in the scene.
                    instanceRef =  FindObjectOfType(typeof (ProjectilePooler)) as ProjectilePooler;
                }
 
                // If it is still null, create a new instance
                if (instanceRef == null) {
                    GameObject obj = new GameObject("ProjectilePooler");
                    instanceRef = obj.AddComponent(typeof (ProjectilePooler)) as ProjectilePooler;
                }
 
                return instanceRef;
            }
        }

        // Use this for initialization
        void Start () {
            // Initialize pool
            sodaProjectilePool = new List<GameObject>();
            for (int i = 0; i < startingPoolSize; i++)
            {
                GameObject sodaProjectile = (GameObject) Instantiate(sodaProjectileObject);
                sodaProjectile.SetActive(false);
                sodaProjectilePool.Add(sodaProjectile);
            }
        }

        public GameObject GetSodaProjectile()
        {
            foreach (GameObject splat in sodaProjectilePool.Where(splat => !splat.activeInHierarchy))
            {
                return splat;
            }

            // If everything is in use, expand pool
            GameObject sodaProjectile = (GameObject) Instantiate(sodaProjectileObject);
            sodaProjectilePool.Add(sodaProjectile);
            return sodaProjectile;
        }

        public List<GameObject> GetSodaProjectiles(int count)
        {
            List<GameObject> projectiles = new List<GameObject>();
            for (int i = 0; i < count; i++)
            {
                projectiles.Add(GetSodaProjectile());
            }
            return projectiles;
        }
    }
}