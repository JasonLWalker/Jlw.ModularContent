[`< Back`](./)

---

# LocalizedContentFieldExtensions

Namespace: Microsoft.Extensions.DependencyInjection



```csharp
public static class LocalizedContentFieldExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [LocalizedContentFieldExtensions](./microsoft.extensions.dependencyinjection.localizedcontentfieldextensions)

## Methods

### **AddLocalizedContentFieldRepository(IServiceCollection, Action&lt;LocalizedContentFieldRepositoryOptions&gt;)**

Adds the localized content field repository to the service collection as a singleton instance.

```csharp
public static IServiceCollection AddLocalizedContentFieldRepository(IServiceCollection services, Action<LocalizedContentFieldRepositoryOptions> setupAction)
```

#### Parameters

`services` IServiceCollection<br>
Service collection instance that this extension will act upon

`setupAction` [Action&lt;LocalizedContentFieldRepositoryOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>
The setup action options used to initialize the repository singleton.

#### Returns

IServiceCollection<br>
Returns the services service collection to allow for method chaining.

---

[`< Back`](./)
