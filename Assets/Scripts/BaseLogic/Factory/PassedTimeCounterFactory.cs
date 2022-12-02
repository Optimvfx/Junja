using Game.Core;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.BaseLogic
{
    [System.Serializable]
    public class PassedTimeCounterFactory
    {
        private static Dictionary<PassedTimeCounterType, IPassedTimeCounter> _typeToObjectConvetor = GenerateConvertor();

        [SerializeField] PassedTimeCounterType _passedTimeCounterType;

        public static IPassedTimeCounter ConvertTypeToObject(PassedTimeCounterType type)
        {
            if (_typeToObjectConvetor.ContainsKey(type))
                return _typeToObjectConvetor[type];

            throw new NullReferenceException();
        }

        private static Dictionary<PassedTimeCounterType, IPassedTimeCounter> GenerateConvertor()
        {
            var typeToObjectConvetor = new Dictionary<PassedTimeCounterType, IPassedTimeCounter>();

            return typeToObjectConvetor;
        }

        public IPassedTimeCounter GetPassedTimeCounter() => ConvertTypeToObject(_passedTimeCounterType);
    }

    public enum PassedTimeCounterType
    {

    }
}
