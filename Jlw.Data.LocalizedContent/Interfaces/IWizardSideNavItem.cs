namespace Jlw.Data.LocalizedContent
{
    public interface IWizardSideNavItem
    {
        string Screen { get; }
        string Wizard { get; }
        int Section { get; }
        int Step { get; }
        string Label { get; }
        string FieldKey { get; }
        string FieldType { get; }
        string ParentKey { get; }
        string FieldData { get; }
        string JsonData { get; }
        string ClassName { get; }
        bool IsHidden { get; }
        string Parent { get; }
    }
}