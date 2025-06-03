using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningAttributesAndReflection.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DisplayOnlyPrimitiveAttribute : Attribute
{
}
