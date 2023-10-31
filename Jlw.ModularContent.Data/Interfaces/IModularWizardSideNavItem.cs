namespace Jlw.ModularContent
{
    /// <summary>
    /// Interface to define an individual item in the side navigation of a wizard.
    /// </summary>
    public interface IModularWizardSideNavItem
    {
        /// <summary>FieldKey for the screen to display</summary>
        string Screen { get; }

        /// <summary>GroupKey for the wizard to display</summary>
        string Wizard { get; }

        /// <summary>Section number to display (legacy/deprecated)</summary>
        int Section { get; }

        /// <summary>Step number to display (legacy/deprecated)</summary>
        int Step { get; }

        /// <summary>Label to display on wizard navigation</summary>
        string Label { get; }

        /// <summary>Field Key for this element/screen/wizard</summary>
        string FieldKey { get; }

        /// <summary>Field Type for this element</summary>
        string FieldType { get; }

        /// <summary>Parent Key for this element</summary>
        string ParentKey { get; }

        /// <summary>Additional Field Data for this element</summary>
        string FieldData { get; }

        /// <summary>Json Data for the side navigation ("sideNav" JSON property in FieldData)</summary>
        string JsonData { get; }

        /// <summary>Additional CSS class(es) to apply the the navigation item ("class" JSON property in JsonData)</summary>
        string ClassName { get; }

        /// <summary>Flag to determine if this element should be shown ("hidden" JSON property in JsonData)</summary>
        bool IsHidden { get; }

        /// <summary>Name of the parent Screen to highlight in the side navigation ("parent" JSON property in JsonData)</summary>
        string Parent { get; }
    }
}