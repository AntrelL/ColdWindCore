using ColdWind.Core.EventLinks;
using ColdWind.Core.ModularCompositeRoot;
using TMPro;
using UnityEngine;

namespace ColdWind.Core.UniversalUI
{
    [RequireComponent(typeof(TMP_Text))]
    public class BaseLinkedText : MonoScript<LinkPair>, ILinkedTextData
    {
        private LinkPair _linkPair;

        protected override void Constructor(LinkPair linkPair)
        {
            _linkPair = linkPair;

            TMPText = GetComponent<TMP_Text>();
            BaseText = TMPText.text;
        }

        public TMP_Text TMPText { get; private set; }

        public string BaseText { get; set; }

        private void OnEnable() => _linkPair.Subscription.Invoke();

        private void OnDisable() => _linkPair.Unsubscription.Invoke();
    }
}
