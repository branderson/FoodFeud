using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaypointController : MonoBehaviour
    {
        // Cached instance of WaypointController for lookup
        private static WaypointController instanceRef = null;

        [SerializeField] public List<Transform> nePath;
        [SerializeField] public List<Transform> ePath;
        [SerializeField] public List<Transform> nwPath;
        [SerializeField] public List<Transform> sePath;
        [SerializeField] public List<Transform> swPath;

        // Instantiate singleton at start of scene
        public static WaypointController instance {
            get {
                if (instanceRef == null) {
                    // This is where the magic happens.
                    //  FindObjectOfType(...) returns the first WaypointController object in the scene.
                    instanceRef =  FindObjectOfType(typeof (WaypointController)) as WaypointController;
                }
 
                // If it is still null, create a new instance
                if (instanceRef == null) {
                    GameObject obj = new GameObject("WaypointController");
                    instanceRef = obj.AddComponent(typeof (WaypointController)) as WaypointController;
                }
 
                return instanceRef;
            }
        }

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public Queue<Transform> GetPath(int pathNum)
        {
            switch (pathNum)
            {
                case 0:
                    return new Queue<Transform>(nePath);
                case 1:
                    return new Queue<Transform>(ePath);
                case 2:
                    return new Queue<Transform>(nwPath);
                case 3:
                    return new Queue<Transform>(sePath);
                case 4:
                    return new Queue<Transform>(swPath);
            }
            return new Queue<Transform>();
        } 
    }
}
