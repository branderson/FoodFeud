using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class EnemyController : MonoBehaviour
    {
        public int Health;
        public int Damage;
        public int Value;
        public float Speed;
        private Queue<Transform> path;
        private Transform currentWaypoint;
        private Transform spriteTransform;

        public Queue<Transform> Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
                currentWaypoint = path.Dequeue();
            }
        }

        // Use this for initialization
        void Start ()
        {
        }
	
        // Update is called once per frame
        protected void Update ()
        {
            if (spriteTransform == null)
            {
                spriteTransform = gameObject.GetComponentInChildren<SpriteRenderer>().transform;
            }
            spriteTransform.localRotation = Quaternion.Euler(0, 0, (Speed / 3) * 10 * Mathf.Sin(Time.time / .2f));
            if (Health <= 0)
            {
                LevelController.instance.Score += Value;
                LevelController.instance.Money += Value/4;
                LevelController.instance.Enemies--;
                Destroy(gameObject);
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, Speed * Time.deltaTime);

            // Check if should move to next waypoint
            if (transform.position == currentWaypoint.position)
            {
                if (path.Count != 0)
                {
                    currentWaypoint = path.Dequeue();
                }
            }
        }
    }
}
