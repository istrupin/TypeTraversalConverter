using System;
using Newtonsoft.Json;
using Shouldly;
using TypeTraversalConverter.Models;
using Xunit;

namespace TypeTraversalConverter.Test
{
    public class ConverterShould
    {
        private ChildClass childClass;
        private string json;
        public ConverterShould()
        {
            var child = new ChildClass { Id = 1, MidId = 2 };
            var json = JsonConvert.SerializeObject(childClass);

        }
        [Fact]
        public void DeserializeHierarchyToNextAvailableParentClass()
        {
            var serializedObject = JsonConvert.DeserializeObject<BaseClass>(json, new TypeTraversalConverter());
            serializedObject.GetType().ShouldBe(typeof(MiddleClass));
        }

        [Fact]
        public void MapProperties()
        {
            var serializedObject = JsonConvert.DeserializeObject<BaseClass>(json, new TypeTraversalConverter());
            var middleClass = (MiddleClass)serializedObject;
            middleClass.Id.ShouldBe(1);
            middleClass.MidId.ShouldBe(2);
        }
    }
}
