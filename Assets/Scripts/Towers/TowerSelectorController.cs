using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Towers
{
    public class TowerSelectorController : MonoBehaviour
    {
        // Cached instance of TowerSelectorController for lookup
        private static TowerSelectorController instanceRef = null;

        private TowerSelector _soda;
        private TowerSelector _fries;
        private TowerSelector _nuggets;
        private int _selection = 0;

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
            SetSelection(0);
            LevelController.instance.Money = LevelController.instance.Money;
        }
	
        // Update is called once per frame
        void Update ()
        {
            if (Input.GetButtonDown("Tower1"))
            {
                SetSelection(1);
            }
            else if (Input.GetButtonDown("Tower2"))
            {
                SetSelection(2);
            }
            else if (Input.GetButtonDown("Tower3"))
            {
                SetSelection(3);
            }
            else if (Input.GetButtonDown("Deselect"))
            {
                SetSelection(0);
            }
        }

        public void SetSelection(int tower)
        {
            switch (tower)
            {
                case 1:
                    _selection = 1;
                    break;
                case 2:
                    _selection = 2;
                    break;
                case 3:
                    _selection = 3;
                    break;
                default:
                    _selection = 0;
                    break;
            }
            _soda.Selected = _selection == 1;
            _fries.Selected = _selection == 2;
            _nuggets.Selected = _selection == 3;
            LevelController.instance.SetSelection(_selection);
        }

        public void SetUnbuildable(int tower)
        {
            switch (tower)
            {
                case 1:
                    _soda.SetUnbuildable();
                    break;
                case 2:
                    _fries.SetUnbuildable();
                    break;
                case 3:
                    _nuggets.SetUnbuildable();
                    break;
            }
            // Deselect towers we cannot build
            if (tower <= _selection)
            {
                SetSelection(0);
            }
        }

        public void SetBuildable(int tower)
        {
            switch (tower)
            {
                case 1:
                    _soda.SetBuildable();
                    break;
                case 2:
                    _fries.SetBuildable();
                    break;
                case 3:
                    _nuggets.SetBuildable();
                    break;
            }
        }
    }
}
