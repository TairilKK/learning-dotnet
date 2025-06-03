using LearningAttributesAndReflection;
using LearningAttributesAndReflection.Attributes;
using System.Reflection;
using System.Runtime.CompilerServices;

void Display(object obj, string prefix = "")
{
    Type objType = obj.GetType();
    PropertyInfo[] props = objType.GetProperties();
    var showOnlyPrimitive = objType.GetCustomAttribute<DisplayOnlyPrimitiveAttribute>() is null;
    if (objType.GetCustomAttribute<DisplayObjectNameAttribute>() is not null)
    {
        Console.WriteLine($"{prefix}{objType.Name}:");
    }

    foreach(var prop in props)
    {
        var value = prop.GetValue(obj);
        Type valType = value.GetType();
        if (valType.BaseType == typeof(Attribute))
            continue;

        if(!valType.IsPrimitive && valType != typeof(string))
        {
            var indent = "  ";
            if(showOnlyPrimitive) Display(value, $"{prefix}{indent}");
            continue;
        }

        var attribute = prop.GetCustomAttribute<DisplayNameAttribute>();
        var name = attribute is null ? prop.Name : attribute.Name;
        Console.WriteLine($"{prefix}{name}: {value}");
    }
}

var add1 = new Address()
{
    City = "Warsaw",
    Street = "Długa"
};

var user1 = new User()
{
    Address = add1,
    FirstName = "Jack",
    LastName = "Sparrow"
};

Display(user1);
