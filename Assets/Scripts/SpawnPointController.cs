using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnPointController : MonoBehaviour
    {
        [SerializeField] private int pathNum;
        [SerializeField] private float rate = 1;
        public Queue<Transform> Path;
        private int spawnCounter;
        private int spawned;

        // Use this for initialization
        void Start ()
        {
            Path = WaypointController.instance.GetPath(pathNum);
            spawnCounter = 0;
            spawned = 0;
        }
	
        // Update is called once per frame
        void Update ()
        {
            if (spawnCounter-- <= 0)
            {
                spawned++;
                GameObject tempTaco = (GameObject)Instantiate(LevelController.instance.TacoPrefab, transform.position, Quaternion.identity);
                EnemyController tacoController = tempTaco.GetComponent<EnemyController>();
                tacoController.Path = new Queue<Transform>(Path);
                tacoController.Speed *= spawned/30f + 1;
                tempTaco.transform.parent = gameObject.transform;
                spawnCounter = (int) (600/(rate) - Time.timeSinceLevelLoad);
                if (spawnCounter < 7)
                {
                    spawnCounter = 7;
                }
                LevelController.instance.Enemies++;
            }
        }
    }
}
