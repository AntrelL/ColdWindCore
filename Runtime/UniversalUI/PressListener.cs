using ColdWind.Core.ModularCompositeRoot;
using UnityEngine.EventSystems;

namespace ColdWind.Core.UniversalUI
{
    public class PressListener : MonoScript, IPointerDownHandler, IPointerUpHandler
    {
        public bool Pressed { get; private set; } = false;

        public void OnPointerDown(PointerEventData eventData) => Pressed = true;

        public void OnPointerUp(PointerEventData eventData) => Pressed = false;
    }
}
