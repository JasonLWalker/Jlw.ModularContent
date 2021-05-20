[`< Back`](./)

---

# LocalizedContentField

Namespace: Jlw.Data.LocalizedContent

Class to encapsulate a row from the [LocalizedContentFields] database table.
 This class is used as a structure to represent a single container, field, or attribute in a Form, Wizard, or Email.

```csharp
public class LocalizedContentField : ILocalizedContentField
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [LocalizedContentField](./jlw.data.localizedcontent.localizedcontentfield)<br>
Implements [ILocalizedContentField](./jlw.data.localizedcontent.ilocalizedcontentfield)

## Properties

### **Id**



```csharp
public long Id { get; protected set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **GroupKey**



```csharp
public string GroupKey { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FieldKey**



```csharp
public string FieldKey { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FieldType**



```csharp
public string FieldType { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FieldData**



```csharp
public string FieldData { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FieldClass**



```csharp
public string FieldClass { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ParentKey**



```csharp
public string ParentKey { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DefaultLabel**



```csharp
public string DefaultLabel { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **WrapperClass**



```csharp
public string WrapperClass { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **WrapperHtmlStart**



```csharp
public string WrapperHtmlStart { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **WrapperHtmlEnd**



```csharp
public string WrapperHtmlEnd { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AuditChangeType**



```csharp
public string AuditChangeType { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AuditChangeBy**



```csharp
public string AuditChangeBy { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AuditChangeDate**



```csharp
public DateTime AuditChangeDate { get; protected set; }
```

#### Property Value

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **Order**



```csharp
public int Order { get; protected set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **LocalizedContentField()**

Initializes a new instance of the [LocalizedContentField](./jlw.data.localizedcontent.localizedcontentfield) class. Members are set to their default values.

```csharp
public LocalizedContentField()
```

### **LocalizedContentField(Object)**

Initializes a new instance of the [LocalizedContentField](./jlw.data.localizedcontent.localizedcontentfield) class.

```csharp
public LocalizedContentField(object o)
```

#### Parameters

`o` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
Object used to initialize the class members.

## Methods

### **Initialize(Object)**

Initializes and sets the properties of the [LocalizedContentField](./jlw.data.localizedcontent.localizedcontentfield) class.

```csharp
public void Initialize(object o)
```

#### Parameters

`o` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
Object used to initialize the class members.

---

[`< Back`](./)
