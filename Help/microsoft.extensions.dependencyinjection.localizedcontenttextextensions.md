[`< Back`](./)

---

# LocalizedContentTextExtensions

Namespace: Microsoft.Extensions.DependencyInjection



```csharp
public static class LocalizedContentTextExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [LocalizedContentTextExtensions](./microsoft.extensions.dependencyinjection.localizedcontenttextextensions)

## Methods

### **AddLocalizedContentTextRepository(IServiceCollection, Action&lt;LocalizedContentTextRepositoryOptions&gt;)**

Adds the [LocalizedContentTextRepository](./jlw.data.localizedcontent.localizedcontenttextrepository) to the service collection as a singleton instance.

```csharp
public static IServiceCollection AddLocalizedContentTextRepository(IServiceCollection services, Action<LocalizedContentTextRepositoryOptions> setupAction)
```

#### Parameters

`services` IServiceCollection<br>
Service collection instance that this extension will act upon

`setupAction` [Action&lt;LocalizedContentTextRepositoryOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>
The setup action options used to initialize the repository singleton.

#### Returns

IServiceCollection<br>
Returns the services service collection to allow for method chaining.

---

[`< Back`](./)
