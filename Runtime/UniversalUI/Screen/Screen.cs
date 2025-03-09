using ColdWind.Core.AdvancedDebugModule;
using ColdWind.Core.ModularCompositeRoot;
using UnityEngine;

namespace ColdWind.Core.UniversalUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Screen : MonoScript<bool>, IReadOnlyScreen
    {
        private CanvasGroup _canvasGroup;

        protected override void Constructor(bool displayed)
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            if (displayed)
                Show(true);
            else
                Hide(true);
        }

        public bool Displayed { get; private set; }

        public void Show(bool force = false)
        {
            if (Displayed && force == false)
            {
                AdvancedDebug.LogError<Screen>(
                    "The screen cannot be shown because it is already being displayed");

                return;
            }

            SetState(true);
        }

        public void Hide(bool force = false)
        {
            if (Displayed == false && force == false)
            {
                AdvancedDebug.LogError<Screen>(
                    "The screen cannot be hidden because it is not displayed");

                return;
            }

            SetState(false);
        }

        public void SwitchTo(Screen screen, bool force = false)
        {
            Hide(force);
            screen.Show(force);
        }

        public void SetState(bool displayed)
        {
            _canvasGroup.alpha = displayed ? 1 : 0;
            _canvasGroup.interactable = displayed;
            _canvasGroup.blocksRaycasts = displayed;

            Displayed = displayed;
        }
    }
}
