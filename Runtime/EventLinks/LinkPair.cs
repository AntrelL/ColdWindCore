using System;

namespace ColdWind.Core.EventLinks
{
    public class LinkPair
    {
        public LinkPair(Action subscription, Action unsubscription)
        {
            Subscription = subscription;
            Unsubscription = unsubscription;
        }

        public Action Subscription { get; private set; }

        public Action Unsubscription { get; private set; }
    }
}
