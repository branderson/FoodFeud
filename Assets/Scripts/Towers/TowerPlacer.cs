using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TowerPlacer : MonoBehaviour
    {
        public GameObject TowerPrefab = null;
        private TowerController tower = null;
        new private SpriteRenderer renderer;

        // Use this for initialization
        void Start () {
            renderer = gameObject.GetComponent<SpriteRenderer>();
            renderer.enabled = false;
        }
	
        // Update is called once per frame
        void Update () {
            if (LevelController.instance.PlaceTower && tower == null)
            {
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
        }

        public bool canPlaceTower()
        {
            return tower == null;
        }

        void OnMouseEnter()
        {
            // Scale if hovered over
            if (tower == null)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            }
        }

        void OnMouseExit()
        {
            if (tower == null)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        void OnMouseUp()
        {
            // Make sure the player can place the tower here and has enough money to pay for it
            if (LevelController.instance.PlaceTower && LevelController.instance.Money >= LevelController.instance.TowerPrefab.GetComponent<TowerController>().GetCost() && canPlaceTower())
            {
                LevelController.instance.PlaceTower = false;
                renderer.enabled = false;
                TowerPrefab = LevelController.instance.TowerPrefab;
                tower = ((GameObject)Instantiate(TowerPrefab, transform.position, Quaternion.identity)).GetComponent<TowerController>();
                tower.transform.parent = gameObject.transform;
                // Turn off sprite for placer
                // Charge the player for the tower
                LevelController.instance.Money -= tower.GetCost();
            }
        }
    }
}
