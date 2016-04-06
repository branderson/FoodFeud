using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class TacoController : EnemyController
    {
//        [SerializeField] private float speed = 1f;
//        [SerializeField] private int damage = 1;
//        [SerializeField] private int value = 100;

        // Use this for initialization
        void Start ()
        {
//            base.moveSpeed = speed;
//            base.Damage = damage;
//            base.Value = value;
        }
	
        // Update is called once per frame
        new void Update ()
        {
            base.Update();
        }
    }
}
