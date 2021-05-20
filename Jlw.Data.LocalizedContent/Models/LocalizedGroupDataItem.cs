using System;
using Jlw.Utilities.Data;

namespace Jlw.Data.LocalizedContent 
{ 
	/// <summary> 
	/// Class to encapsulate a row from the [LocalizedGroupDataItems] database table 
	/// </summary> 
	public class LocalizedGroupDataItem : ILocalizedGroupDataItem 
	{
        /// Member for [Id] Database Column 
		public long Id { get; protected set; }      
 
		/// Member for [Language] Database Column 
		public string Language { get; protected set; }      
 
		/// Member for [GroupKey] Database Column 
		public string GroupKey { get; protected set; }      
 
		/// Member for [Key] Database Column 
		public string Key { get; protected set; }      
 
		/// Member for [Value] Database Column 
		public string Value { get; protected set; }      
 
		/// Member for [Order] Database Column 
		public int Order { get; protected set; }      
 
		/// Member for [Description] Database Column 
		public string Description { get; protected set; }      
 
		/// Member for [Data] Database Column 
		public string Data { get; protected set; }      
 
        public string AuditChangeType { get; protected set; }
        public string AuditChangeBy { get; protected set; }
        public DateTime AuditChangeDate { get; protected set; }
        
        public LocalizedGroupDataItem() => Initialize(null);

        public LocalizedGroupDataItem (object o) => Initialize(o); 

        public void Initialize(object o)
        {
            Id = DataUtility.Parse<long>(o, "Id");
			Language = DataUtility.Parse<string>(o, "Language");
			GroupKey = DataUtility.Parse<string>(o, "GroupKey");
			Key = DataUtility.Parse<string>(o, "Key");
			Value = DataUtility.Parse<string>(o, "Value");
			Order = DataUtility.Parse<int>(o, "Order");
			Description = DataUtility.Parse<string>(o, "Description");
			Data = DataUtility.Parse<string>(o, "Data");
			AuditChangeType = DataUtility.Parse<string>(o, "AuditChangeType");
			AuditChangeBy = DataUtility.Parse<string>(o, "AuditChangeBy");
			AuditChangeDate = DataUtility.ParseDateTime(o, "AuditChangeDate");
        }
	}
}

