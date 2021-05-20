[`< Back`](./)

---

# LocalizedContentTextRepository

Namespace: Jlw.Data.LocalizedContent



```csharp
public class LocalizedContentTextRepository : Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase`2[[Jlw.Data.LocalizedContent.ILocalizedContentText, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null],[Jlw.Data.LocalizedContent.LocalizedContentText, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null]], Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase`2[[Jlw.Data.LocalizedContent.ILocalizedContentText, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null],[Jlw.Data.LocalizedContent.LocalizedContentText, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null]], ILocalizedContentTextRepository
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ModularDataRepositoryBase&lt;ILocalizedContentText, LocalizedContentText&gt; → [LocalizedContentTextRepository](./jlw.data.localizedcontent.localizedcontenttextrepository)<br>
Implements IModularDataRepositoryBase&lt;ILocalizedContentText, LocalizedContentText&gt;, [ILocalizedContentTextRepository](./jlw.data.localizedcontent.ilocalizedcontenttextrepository)

## Constructors

### **LocalizedContentTextRepository(IModularDbClient, String)**



```csharp
public LocalizedContentTextRepository(IModularDbClient dbClient, string connString)
```

#### Parameters

`dbClient` IModularDbClient<br>

`connString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **GetParamsForSql(ILocalizedContentText, String)**



```csharp
protected IEnumerable<KeyValuePair<string, object>> GetParamsForSql(ILocalizedContentText o, string sSql)
```

#### Parameters

`o` [ILocalizedContentText](./jlw.data.localizedcontent.ilocalizedcontenttext)<br>

`sSql` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[IEnumerable&lt;KeyValuePair&lt;String, Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **GetDataTableList(LocalizedContentTextDataTablesInput)**



```csharp
public object GetDataTableList(LocalizedContentTextDataTablesInput o)
```

#### Parameters

`o` [LocalizedContentTextDataTablesInput](./jlw.data.localizedcontent.localizedcontenttextdatatablesinput)<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

---

[`< Back`](./)
