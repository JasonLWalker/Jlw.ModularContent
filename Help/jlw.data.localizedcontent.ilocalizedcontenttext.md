[`< Back`](./)

---

# ILocalizedContentText

Namespace: Jlw.Data.LocalizedContent

Class to encapsulate a row from the LocalizedContentText database table

```csharp
public interface ILocalizedContentText
```

## Properties

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

### **Language**



```csharp
public abstract string Language { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Text**



```csharp
public abstract string Text { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AuditChangeType**

The type of change that was last made to the record data.

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
