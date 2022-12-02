using System.Reflection;
using UnityEngine;

namespace Game.Core
{
    public static class Validator
    {
        public static void Validate<T>(T validatable)
            where T : IValidatable
        {
            var type = validatable.GetType();

            FieldInfo[] fields = type.GetFields();

            for (int index = 0; index < fields.Length; ++index)
            {
                if (fields[index].FieldType is IValidatable)
                {
                    var validatableObject = fields[index].GetValue(validatable) as IValidatable;
                    validatableObject.Validate();
                }
            }
        }
    }
}