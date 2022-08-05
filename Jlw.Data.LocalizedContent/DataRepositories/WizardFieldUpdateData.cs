namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Class used as a data record for updating a database field for stored procedure [sp_SaveLocalizedContentFieldData]
    /// </summary>
    public class WizardFieldUpdateData
    {
        /// <summary> @id property. Indicates the [LocalizedContentFields] record to update </summary>
        public long Id { get; set; }

        /// <summary> @fieldName property. Indicates which column to update in the table </summary>
        public string FieldName { get; set; }

        /// <summary> @fieldValue property. The value to update the column with </summary>
        public string FieldValue { get; set; }

        /// <summary> @auditchangeby property. The username of the person making the changes. </summary>
        public string AuditChangeBy { get; set; }

        /// <summary>
        /// @groupfilter property.
        /// If this value is set when passing the value to the database, then the [GroupKey]
        /// field of the record will have to match a SQL LIKE pattern, or the update will fail.
        /// </summary>
        public string GroupFilter { get; set; }
    }
}