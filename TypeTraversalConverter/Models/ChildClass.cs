namespace TypeTraversalConverter.Models
{
    public class ChildClass : MiddleClass
    {
        public ChildClass()
        {
            Hierarchy = "BaseClass:MiddleClass:ChildClass";
        }
        public string Hierarchy { get; set; }
    }
}