using LearningAttributesAndReflection;
using System.Reflection;

void Display(object obj, string prefix="")
{
    Type objType = obj.GetType();
    PropertyInfo[] props = objType.GetProperties();

    Console.WriteLine($"{prefix}{objType.Name}:");
    foreach(var prop in props)
    {
        var value = prop.GetValue(obj);
        Type valType = value.GetType();
        
        if(valType.IsPrimitive || valType == typeof(string))
            Console.WriteLine($"{prefix}  {prop.Name}: {value}");
        else
            Display(value, $"{prefix}  ");
    }
    Console.WriteLine("");
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

Display(add1);
Display(user1);
