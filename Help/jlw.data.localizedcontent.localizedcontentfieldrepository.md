[`< Back`](./)

---

# LocalizedContentFieldRepository

Namespace: Jlw.Data.LocalizedContent



```csharp
public class LocalizedContentFieldRepository : Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase`2[[Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null],[Jlw.Data.LocalizedContent.LocalizedContentField, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null]], Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase`2[[Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null],[Jlw.Data.LocalizedContent.LocalizedContentField, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null]], ILocalizedContentFieldRepository
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ModularDataRepositoryBase&lt;ILocalizedContentField, LocalizedContentField&gt; → [LocalizedContentFieldRepository](./jlw.data.localizedcontent.localizedcontentfieldrepository)<br>
Implements IModularDataRepositoryBase&lt;ILocalizedContentField, LocalizedContentField&gt;, [ILocalizedContentFieldRepository](./jlw.data.localizedcontent.ilocalizedcontentfieldrepository)

## Constructors

### **LocalizedContentFieldRepository(IModularDbClient, String)**



```csharp
public LocalizedContentFieldRepository(IModularDbClient dbClient, string connString)
```

#### Parameters

`dbClient` IModularDbClient<br>

`connString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **GetParamsForSql(ILocalizedContentField, String)**



```csharp
protected IEnumerable<KeyValuePair<string, object>> GetParamsForSql(ILocalizedContentField o, string sSql)
```

#### Parameters

`o` [ILocalizedContentField](./jlw.data.localizedcontent.ilocalizedcontentfield)<br>

`sSql` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[IEnumerable&lt;KeyValuePair&lt;String, Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **GetDataTableList(LocalizedContentFieldDataTablesInput)**



```csharp
public object GetDataTableList(LocalizedContentFieldDataTablesInput o)
```

#### Parameters

`o` [LocalizedContentFieldDataTablesInput](./jlw.data.localizedcontent.localizedcontentfielddatatablesinput)<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

---

[`< Back`](./)
