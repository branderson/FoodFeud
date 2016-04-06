using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Towers
{
    public class TowerSelectorController : MonoBehaviour
    {
        // Cached instance of TowerSelectorController for lookup
        private static TowerSelectorController instanceRef = null;

        private Image sodaImage;
        private Image friesImage;
        private Image nuggetImage;
        private int selection = 0;

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
            sodaImage = GameObject.FindGameObjectWithTag("SodaImage").GetComponent<Image>();
            friesImage = GameObject.FindGameObjectWithTag("FriesImage").GetComponent<Image>();
            nuggetImage = GameObject.FindGameObjectWithTag("NuggetImage").GetComponent<Image>();
            SetSelection(0);
            LevelController.instance.Money = LevelController.instance.Money;
        }
	
        // Update is called once per frame
        void Update () {
        }

        public void SetSelection(int tower)
        {
            switch (tower)
            {
                case 1:
                    selection = 1;
                    break;
                case 2:
                    selection = 2;
                    break;
                case 3:
                    selection = 3;
                    break;
                default:
                    selection = 0;
                    break;
            }
            if (selection == 1)
            {
                Select(sodaImage);
            }
            else
            {
                Deselect(sodaImage);
            }
            if (selection == 2)
            {
                Select(friesImage);
            }
            else
            {
                Deselect(friesImage);
            }
            if (selection == 3)
            {
                Select(nuggetImage);
            }
            else
            {
                Deselect(nuggetImage);
            }
        }

        public void SetUnbuildable(int tower)
        {
            switch (tower)
            {
                case 1:
                    SetUnbuildable(sodaImage);
                    break;
                case 2:
                    SetUnbuildable(friesImage);
                    break;
                case 3:
                    SetUnbuildable(nuggetImage);
                    break;
            }
        }

        private void SetUnbuildable(Image image)
        {
            Color color = image.color;
            color.r = 1f;
            color.b = 0f;
            color.g = 0f;
            image.color = color;
        }

        public void SetBuildable(int tower)
        {
            switch (tower)
            {
                case 1:
                    SetBuildable(sodaImage);
                    break;
                case 2:
                    SetBuildable(friesImage);
                    break;
                case 3:
                    SetBuildable(nuggetImage);
                    break;
            }
        }

        private void SetBuildable(Image image)
        {
            Color color = image.color;
            color.r = 1f;
            color.b = 1f;
            color.g = 1f;
            image.color = color;
        }

        private void Select(Image image)
        {
            image.rectTransform.localScale = new Vector3(1.3f, 1.3f, 1f);
            Color color = image.color;
            color.a = 1f;
            image.color = color;
        }

        private void Deselect(Image image)
        {
            image.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            Color color = image.color;
            color.a = .5f;
            image.color = color;
        }
    }
}
