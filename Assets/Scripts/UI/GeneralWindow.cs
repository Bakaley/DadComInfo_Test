using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class GeneralWindow : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;

        public virtual void Open()
        {
            canvasGroup.alpha = 1;
        }

        public virtual void Close()
        {
            canvasGroup.alpha = 0;
        }
    }
}
