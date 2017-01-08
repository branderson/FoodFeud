using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Towers
{
    public class TowerSelector : MonoBehaviour
    {
        [SerializeField] private int _index = 0;
        private bool _selected = false;
        private Image _image;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                if (_selected) Select();
                else Deselect();
            }
        }

        private void Start ()
        {
            _image = GetComponent<Image>();
        }
	
        private void Update ()
        {
        }

        public void SetBuildable()
        {
            Color color = _image.color;
            color.r = 1f;
            color.b = 1f;
            color.g = 1f;
            _image.color = color;
        }

        public void SetUnbuildable()
        {
            Color color = _image.color;
            color.r = 1f;
            color.b = 0f;
            color.g = 0f;
            _image.color = color;
        }

        private void Select()
        {
            _image.rectTransform.localScale = new Vector3(1.3f, 1.3f, 1f);
            Color color = _image.color;
            color.a = 1f;
            _image.color = color;
            _selected = true;
        }

        private void Deselect()
        {
            _image.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            Color color = _image.color;
            color.a = .5f;
            _image.color = color;
            _selected = false;
        }

        private void Hover()
        {
            _image.rectTransform.localScale = new Vector3(1.3f, 1.3f, 1f);
            Color color = _image.color;
            color.a = .5f;
            _image.color = color;
            _selected = false;
        }

        private void Unhover()
        {
            Deselect();
        }

        public void OnMouseEnter()
        {
            if (!_selected)
            {
                Hover();
            }
        }

        public void OnMouseExit()
        {
            if (!_selected)
            {
                Unhover();
            }
        }

        public void OnMouseUp()
        {
            TowerSelectorController.instance.SetSelection(_index);
        }
    }
}
