using UnityEngine;

namespace Chess.Scripts.Presentations
{
    public class UiViewBase : MonoBehaviour
    {
        private Canvas _canvas;

        protected Canvas Canvas
        {
            get
            {
                if (_canvas == null) _canvas = GetComponent<Canvas>();
                return _canvas;
            }
        }
    }
}
