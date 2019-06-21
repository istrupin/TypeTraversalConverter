## TypeTraversalConverter

This is a custom type converter for [Json.net](https://github.com/JamesNK/Newtonsoft.Json) meant to handle custom logic for class hierarchy deserialization.

Essentially the converter is meant to deserialize an object into the the deepest child class known by the calling code.

While generally this is not needed, in a use case where server code is updated with new child types and client code does not receive the update, the goal is to deserialize to the closest parent known by the client.

For example, the server and client both know about "Food", "Fruit", and "Carrot" objects.  Then, the server adds a "Pepper" object to its domain, and the client does not receive the update.  If the client received a "Pepper" object from the server, rather than breaking serialization, it should receive a "Vegetable" object.

This is done through enum to type mapping and a ':' separated hierarchy string.
