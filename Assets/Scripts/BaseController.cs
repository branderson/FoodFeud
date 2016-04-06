using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class BaseController : MonoBehaviour
    {
        private int health = 20;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyController enemy;
            enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                health -= enemy.Damage;
                if (health <= 0)
                {
                    SceneManager.LoadScene("GameOverScene");
                }
                Destroy(collision.gameObject);
                LevelController.instance.Health = health;
                LevelController.instance.Enemies--;
            }
        }
    }
}
