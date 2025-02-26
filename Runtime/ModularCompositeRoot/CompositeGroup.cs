using UnityEngine;

namespace ColdWind.Core.ModularCompositeRoot
{
    public abstract class CompositeGroup : MonoBehaviour
    {
        public virtual void LoadResources() { }

        public virtual void Construct() { }
    }
}
