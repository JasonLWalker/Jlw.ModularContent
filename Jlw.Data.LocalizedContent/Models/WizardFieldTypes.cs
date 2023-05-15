// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="WizardFieldTypes.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Field types for the [FieldType] column of the [LocalizedContentFields]
    /// </summary>
    public enum WizardFieldTypes
    {
        /// <summary>Field is a Wizard</summary>
        WIZARD,

        /// <summary>Field is a Screen</summary>
        SCREEN,

        /// <summary>Field is an Email</summary>
        EMAIL,

        /// <summary>Field is an Email Subject</summary>
        SUBJECT,

        /// <summary>Field is an Email or Screen Body</summary>
        BODY,

        /// <summary>Field is a Screen heading</summary>
        HEADING,

        /// <summary>Field is an HTML element</summary>
        HTML,

        /// <summary>Field is a form container</summary>
        FORM,

        /// <summary>Field is an embedded form</summary>
        EMBED,

        /// <summary>Field is a Button</summary>
        BUTTON,

        /// <summary>Field is an Input type form element</summary>
        INPUT,

        /// <summary>Field is a Select form element</summary>

        SELECT,

        /// <summary>Field is a TextArea form element</summary>
        TEXTAREA,

        /// <summary>Field is an HR Html element</summary>
        SEPARATOR,

        /// <summary>Field is a Vertical Separator bar (bootstrap 5)</summary>
        VSEPARATOR,

        /// <summary>Field is a Custom form element</summary>
        CUSTOM
    }
}
