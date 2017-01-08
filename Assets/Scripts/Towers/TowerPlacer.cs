using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TowerPlacer : MonoBehaviour
    {
        public GameObject TowerPrefab = null;
        private TowerController _tower = null;
        private SpriteRenderer _renderer;

        // Use this for initialization
        void Start () {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _renderer.enabled = false;
        }
	
        // Update is called once per frame
        void Update () {
            if (LevelController.instance.PlaceTower && _tower == null)
            {
                _renderer.enabled = true;
            }
            else
            {
                _renderer.enabled = false;
            }
        }

        public bool CanPlaceTower()
        {
            return _tower == null;
        }

        void OnMouseEnter()
        {
            // Scale if hovered over
            if (_tower == null)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            }
        }

        void OnMouseExit()
        {
            if (_tower == null)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        void OnMouseUp()
        {
            // Make sure the player can place the tower here and has enough money to pay for it
            if (LevelController.instance.PlaceTower && LevelController.instance.Money >= LevelController.instance.TowerPrefab.GetComponent<TowerController>().GetCost() && CanPlaceTower())
            {
//                LevelController.instance.PlaceTower = false;
                _renderer.enabled = false;
                TowerPrefab = LevelController.instance.TowerPrefab;
                _tower = ((GameObject)Instantiate(TowerPrefab, transform.position, Quaternion.identity)).GetComponent<TowerController>();
                _tower.transform.parent = gameObject.transform;
                // Turn off sprite for placer
                // Charge the player for the tower
                LevelController.instance.Money -= _tower.GetCost();
            }
        }
    }
}
