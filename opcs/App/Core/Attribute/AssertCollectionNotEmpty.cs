using System.ComponentModel.DataAnnotations;

namespace opcs.App.Core.Attribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public class AssertCollectionNotEmpty : ValidationAttribute
{
}