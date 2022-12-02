using UnityEngine;

namespace Game.BaseLogic
{
    public abstract class ScriptableConvention : ScriptableObject, IConventionFactiory
    {
        public abstract IConvention GetConvention();
    }
}
