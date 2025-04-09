using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Module.Shared.Validation;

public sealed class ValidationBuilder<T> where T : Settings, new()
{
    private readonly IConfiguration _configuration;
    private IConfigurationSection? _settingsSection;
    private readonly T _settings = new();

    public ValidationBuilder(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public T Build() => _settings;

    public ValidationBuilder<T> SectionExists(string sectionKey)
    {
        _settingsSection = _configuration.GetSection(sectionKey);

        if (!_settingsSection.Exists())
        {
            throw new InvalidOperationException($"{sectionKey} settings section is missing.");
        }

        return this;
    }

    public ValidationBuilder<T> CheckEnabled(Expression<Func<T, bool>> enabledProperty, string enabledKey)
    {
        ArgumentNullException.ThrowIfNull(_settingsSection, "Settings Section is missing.");

        var isEnabledSection = _settingsSection.GetSection(enabledKey);
        var isEnabled = _settingsSection.GetValue<bool>(enabledKey);

        if (!isEnabledSection.Exists() || !isEnabled)
        {
            SetProperty(enabledProperty, false);
        }
        else
        {
            SetProperty(enabledProperty, true);
        }

        return this;
    }

    public ValidationBuilder<T> CheckValue<TProp>(Expression<Func<T, TProp>> property, string valueKey)
    {
        ArgumentNullException.ThrowIfNull(_settingsSection, "Settings Section is missing.");

        if (!_settings.IsEnabled)
        {
            return this;
        }

        var value = _settingsSection.GetValue<TProp>(valueKey);

        if (value == null || (value is string str && string.IsNullOrEmpty(str)))
        {
            throw new InvalidOperationException($"{valueKey} configuration is missing");
        }

        SetProperty(property, value);
        return this;
    }

    public ValidationBuilder<T> CheckValue<TProp>(Expression<Func<T, List<TProp>>> property, string valueKey)
    {
        ArgumentNullException.ThrowIfNull(_settingsSection, "Settings Section is missing.");

        if (!_settings.IsEnabled)
        {
            return this;
        }

        var value = _settingsSection.GetSection(valueKey).Get<List<TProp>>();

        if (value == null || value.Count == 0)
        {
            throw new InvalidOperationException($"{valueKey} configuration is missing or empty");
        }

        SetProperty(property, value);
        return this;
    }

    private void SetProperty<TProp>(Expression<Func<T, TProp>> propertyExpression, TProp value)
    {
        if (propertyExpression.Body is MemberExpression { Member: System.Reflection.PropertyInfo propertyInfo })
        {
            propertyInfo.SetValue(_settings, value);
        }
        else
        {
            throw new InvalidOperationException("Invalid property expression");
        }
    }
}