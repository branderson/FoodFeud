using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] public int Damage;

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
            enemy.Health -= Damage;
        }
    }
}
