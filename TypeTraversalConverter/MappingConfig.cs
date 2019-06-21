using System;
using System.Collections.Generic;

namespace TypeTraversalConverter
{
    public static class MappingConfig
    {
        static MappingConfig()
        {
            SerializationMap = new Dictionary<ClassTypeEnum, Type>
            {
                {ClassTypeEnum.MiddleClass, typeof(Models.MiddleClass)},
                {ClassTypeEnum.BaseClass, typeof(Models.BaseClass)},
            };
        }
        public static Dictionary<ClassTypeEnum, Type> SerializationMap;

        public static Type GetType(ClassTypeEnum classType)
        {
            return SerializationMap[classType];
        }
    }
}