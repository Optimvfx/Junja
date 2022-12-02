using Game.Core;
using UnityEngine;

namespace Game.BaseLogic
{
    [CreateAssetMenu(fileName = "MultiConvention", menuName = "ScriptableObjects/Conventions/MultiConvention", order = 1)]
    public class ScriptableMultiConvention : ScriptableConvention
    {
        [SerializeField] private ScriptableConvention[] _scriptableConventions;

        [SerializeField] private MultiConvention.ConventionsToPerfomed _conventionsToPerfomed;

        public override IConvention GetConvention()
        {
            return new MultiConvention(_scriptableConventions, _conventionsToPerfomed);
        }
    }
}