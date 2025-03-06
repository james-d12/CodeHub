using System.Linq.Expressions;
using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.Shared.Validation;

public interface IValidationBuilder<T> where T : Settings, new()
{
    T Build();
    ValidationBuilder<T> SectionExists(string sectionKey);
    ValidationBuilder<T> CheckEnabled(Expression<Func<T, bool>> enabledProperty, string enabledKey);
    ValidationBuilder<T> CheckValue<TProp>(Expression<Func<T, TProp>> property, string valueKey);
}