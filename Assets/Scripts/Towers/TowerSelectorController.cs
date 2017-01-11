using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Towers
{
    public enum Tower
    {
        SodaTower = 1,
        FriesTower = 2,
        NuggetTower = 3,
        NoTower = 0,
        SellTower = -1
    }

    public class TowerSelectorController : MonoBehaviour
    {
        // Cached instance of TowerSelectorController for lookup
        private static TowerSelectorController instanceRef = null;

        public bool Selling = false;

        private TowerSelector _soda;
        private TowerSelector _fries;
        private TowerSelector _nuggets;
        private TowerSelector _sell;
        [SerializeField] private Text _sellText;
        private Tower _selection = Tower.NoTower;
        private TowerController _hoverTower;

        // Instantiate singleton at start of scene
        public static TowerSelectorController instance {
            get {
                if (instanceRef == null) {
                    // This is where the magic happens.
                    //  FindObjectOfType(...) returns the first TowerSelectorController object in the scene.
                    instanceRef =  FindObjectOfType(typeof (TowerSelectorController)) as TowerSelectorController;
                }
 
                // If it is still null, create a new instance
                if (instanceRef == null) {
                    GameObject obj = new GameObject("TowerSelectorController");
                    instanceRef = obj.AddComponent(typeof (TowerSelectorController)) as TowerSelectorController;
                }
 
                return instanceRef;
            }
        }

        // Use this for initialization
        void Start ()
        {
            _soda = GameObject.FindGameObjectWithTag("SodaImage").GetComponent<TowerSelector>();
            _fries = GameObject.FindGameObjectWithTag("FriesImage").GetComponent<TowerSelector>();
            _nuggets = GameObject.FindGameObjectWithTag("NuggetImage").GetComponent<TowerSelector>();
            _sell = GameObject.FindGameObjectWithTag("SellImage").GetComponent<TowerSelector>();
            SetSelection(Tower.NoTower);
            LevelController.instance.Money = LevelController.instance.Money;
        }
	
        // Update is called once per frame
        void Update ()
        {
            if (Input.GetButtonDown("Tower1"))
            {
                SetSelection(Tower.SodaTower);
            }
            else if (Input.GetButtonDown("Tower2"))
            {
                SetSelection(Tower.FriesTower);
            }
            else if (Input.GetButtonDown("Tower3"))
            {
                SetSelection(Tower.NuggetTower);
            }
            else if (Input.GetButtonDown("Sell"))
            {
                SetSelection(Tower.SellTower);
            }
            else if (Input.GetButtonDown("Deselect"))
            {
                SetSelection(Tower.NoTower);
            }
            if (_selection == Tower.SellTower)
            {
                if (_hoverTower != null) _sellText.text = "$" + (_hoverTower.Cost/2);
                else _sellText.text = "";
            }
            else
            {
                _sellText.text = "";
            }
        }

        public void SetSelection(Tower tower)
        {
            _selection = tower;
            _soda.Selected = _selection == Tower.SodaTower;
            _fries.Selected = _selection == Tower.FriesTower;
            _nuggets.Selected = _selection == Tower.NuggetTower;
            _sell.Selected = _selection == Tower.SellTower;
            Selling = _selection == Tower.SellTower;
            LevelController.instance.SetSelection(_selection);
        }

        public void SetUnbuildable(Tower tower)
        {
            switch (tower)
            {
                case Tower.SodaTower:
                    _soda.SetUnbuildable();
                    break;
                case Tower.FriesTower:
                    _fries.SetUnbuildable();
                    break;
                case Tower.NuggetTower:
                    _nuggets.SetUnbuildable();
                    break;
            }
            // Deselect towers we cannot build
//            if (tower <= _selection)
//            {
//                SetSelection(0);
//            }
        }

        public void SetBuildable(Tower tower)
        {
            switch (tower)
            {
                case Tower.SodaTower:
                    _soda.SetBuildable();
                    break;
                case Tower.FriesTower:
                    _fries.SetBuildable();
                    break;
                case Tower.NuggetTower:
                    _nuggets.SetBuildable();
                    break;
            }
        }

        public void HoverTower(TowerController tower)
        {
            _hoverTower = tower;
        }

        public void UnhoverTower(TowerController tower)
        {
            if (_hoverTower == tower) _hoverTower = null;
        }

        public void SellTower(TowerController tower)
        {
            if (_hoverTower == tower) _hoverTower = null;
            LevelController.instance.Money += tower.Cost/2;
            tower.Placer.DestroyTower();
        }
    }
}
