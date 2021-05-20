using System;
using Jlw.Utilities.Data;

namespace Jlw.Data.LocalizedContent 
{ 
	/// <summary> 
	/// Class to encapsulate a row from the [LocalizedContentText] database table 
	/// </summary> 
	public class LocalizedContentText : ILocalizedContentText 
	{ 
 
		/// Member for [GroupKey] Database Column 
		public string GroupKey { get; protected set; }      
 
		/// Member for [FieldKey] Database Column 
		public string FieldKey { get; protected set; }      
 
		/// Member for [Language] Database Column 
		public string Language { get; protected set; }      
 
		/// Member for [Text] Database Column 
		public string Text { get; protected set; }      
 
		/// Member for [AuditChangeType] Database Column 
		public string AuditChangeType { get; protected set; }      
 
		/// Member for [AuditChangeBy] Database Column 
		public string AuditChangeBy { get; protected set; }      
 
		/// Member for [AuditChangeDate] Database Column 
		public DateTime AuditChangeDate { get; protected set; }

		public LocalizedContentText() => Initialize(null);
        public LocalizedContentText (object o) => Initialize(o); 

        public void Initialize(object o)
        {
            GroupKey = DataUtility.Parse<string>(o, "GroupKey");
			FieldKey = DataUtility.Parse<string>(o, "FieldKey");
			Language = DataUtility.Parse<string>(o, "Language");
			Text = DataUtility.Parse<string>(o, "Text");
			AuditChangeType = DataUtility.Parse<string>(o, "AuditChangeType");
			AuditChangeBy = DataUtility.Parse<string>(o, "AuditChangeBy");
			AuditChangeDate = DataUtility.Parse<DateTime>(o, "AuditChangeDate");
        }
	} 
} 
 
