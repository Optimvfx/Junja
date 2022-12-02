using Game.BaseLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Debuging
{
    public class ConventionDebug : MonoBehaviour
    {
        [SerializeField] private ScriptableConvention _scriptableConvention;

        private IConvention _convention;

        private void Awake()
        {
            _convention = _scriptableConvention.GetConvention();
        }

        private void Update()
        {
            Debug.Log("Convention is performed : " + _convention.IsPerformed);
        }
    }
}