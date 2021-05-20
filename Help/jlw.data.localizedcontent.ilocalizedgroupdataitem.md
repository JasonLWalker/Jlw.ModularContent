[`< Back`](./)

---

# ILocalizedGroupDataItem

Namespace: Jlw.Data.LocalizedContent

Class to encapsulate a row from the LocalizedGroupDataItems database table

```csharp
public interface ILocalizedGroupDataItem
```

## Properties

### **Id**

Member for Id Database Column

```csharp
public abstract long Id { get; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Language**



```csharp
public abstract string Language { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GroupKey**



```csharp
public abstract string GroupKey { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Key**



```csharp
public abstract string Key { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Value**



```csharp
public abstract string Value { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Order**



```csharp
public abstract int Order { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Description**



```csharp
public abstract string Description { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**



```csharp
public abstract string Data { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AuditChangeType**

The type of the change that was last made to the record data.

```csharp
public abstract string AuditChangeType { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

            Values for this field are usually one of the following:IMPORTINSERTUPDATEDELETE

### **AuditChangeBy**

The username of the user that last made changes to the record data.

```csharp
public abstract string AuditChangeBy { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AuditChangeDate**

The date and time of the last change made to this data.

```csharp
public abstract DateTime AuditChangeDate { get; }
```

#### Property Value

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

---

[`< Back`](./)
