using System.Linq;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;

namespace Game.BaseLogic
{
    public class MultiConvention : IConvention
    {
        private ConventionsToPerfomed _conventionsToPerfomed;

        private IConvention[] _conventions;

        public bool IsPerformed
        {
            get
            {
                if (_conventionsToPerfomed == ConventionsToPerfomed.Any)
                    return _conventions.Any(convention => convention.IsPerformed);

                if (_conventionsToPerfomed == ConventionsToPerfomed.All)
                    return _conventions.All(convention => convention.IsPerformed);

                return false;
            }
        }

        public MultiConvention(IEnumerable<IConventionFactiory> coventionFactorys, ConventionsToPerfomed conventionsToPerfomed)
        {
            _conventionsToPerfomed = conventionsToPerfomed;

            _conventions = coventionFactorys.Select(conventionFactiory => conventionFactiory.GetConvention()).ToArray();

            foreach (var convention in _conventions)
            {
                if (convention == this)
                    throw new TimeoutException("Convention have self link!");
            }
        }

        public enum ConventionsToPerfomed
        {
            Any,
            All
        }
    }
}
