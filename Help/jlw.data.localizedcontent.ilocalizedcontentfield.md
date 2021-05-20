[`< Back`](./)

---

# ILocalizedContentField

Namespace: Jlw.Data.LocalizedContent

Class to encapsulate a row from the LocalizedContentFields database table

```csharp
public interface ILocalizedContentField
```

## Properties

### **Id**

Returns the unique Id of the record. Matches the [Id] column of the [LocalizedContentFields] table in the database.

```csharp
public abstract long Id { get; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **GroupKey**



```csharp
public abstract string GroupKey { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FieldKey**



```csharp
public abstract string FieldKey { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FieldType**



```csharp
public abstract string FieldType { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FieldData**



```csharp
public abstract string FieldData { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FieldClass**



```csharp
public abstract string FieldClass { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ParentKey**



```csharp
public abstract string ParentKey { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DefaultLabel**



```csharp
public abstract string DefaultLabel { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **WrapperClass**



```csharp
public abstract string WrapperClass { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **WrapperHtmlStart**



```csharp
public abstract string WrapperHtmlStart { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **WrapperHtmlEnd**



```csharp
public abstract string WrapperHtmlEnd { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AuditChangeType**



```csharp
public abstract string AuditChangeType { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AuditChangeBy**



```csharp
public abstract string AuditChangeBy { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AuditChangeDate**



```csharp
public abstract DateTime AuditChangeDate { get; }
```

#### Property Value

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **Order**



```csharp
public abstract int Order { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

---

[`< Back`](./)
