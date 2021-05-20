[`< Back`](./)

---

# LocalizedGroupDataItemRepository

Namespace: Jlw.Data.LocalizedContent



```csharp
public class LocalizedGroupDataItemRepository : Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase`2[[Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null],[Jlw.Data.LocalizedContent.LocalizedGroupDataItem, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null]], Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase`2[[Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null],[Jlw.Data.LocalizedContent.LocalizedGroupDataItem, Jlw.Data.LocalizedContent, Version=0.1.7810.21078, Culture=neutral, PublicKeyToken=null]], ILocalizedGroupDataItemRepository
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ModularDataRepositoryBase&lt;ILocalizedGroupDataItem, LocalizedGroupDataItem&gt; → [LocalizedGroupDataItemRepository](./jlw.data.localizedcontent.localizedgroupdataitemrepository)<br>
Implements IModularDataRepositoryBase&lt;ILocalizedGroupDataItem, LocalizedGroupDataItem&gt;, [ILocalizedGroupDataItemRepository](./jlw.data.localizedcontent.ilocalizedgroupdataitemrepository)

## Constructors

### **LocalizedGroupDataItemRepository(IModularDbClient, String)**



```csharp
public LocalizedGroupDataItemRepository(IModularDbClient dbClient, string connString)
```

#### Parameters

`dbClient` IModularDbClient<br>

`connString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **GetParamsForSql(ILocalizedGroupDataItem, String)**



```csharp
protected IEnumerable<KeyValuePair<string, object>> GetParamsForSql(ILocalizedGroupDataItem o, string sSql)
```

#### Parameters

`o` [ILocalizedGroupDataItem](./jlw.data.localizedcontent.ilocalizedgroupdataitem)<br>

`sSql` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[IEnumerable&lt;KeyValuePair&lt;String, Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **GetDataTableList(LocalizedGroupDataItemDataTablesInput)**



```csharp
public object GetDataTableList(LocalizedGroupDataItemDataTablesInput o)
```

#### Parameters

`o` [LocalizedGroupDataItemDataTablesInput](./jlw.data.localizedcontent.localizedgroupdataitemdatatablesinput)<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

---

[`< Back`](./)
