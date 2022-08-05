// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="IWizardFactory.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Interface IWizardFactory
    /// </summary>
    /// TODO Edit XML Comment Template for IWizardFactory
    public interface IWizardFactory
    {
        /// <summary>
        /// Creates the content of the wizard.
        /// </summary>
        /// <param name="groupKey">The group key.</param>
        /// <param name="formData">The form data.</param>
        /// <returns>IWizardContent.</returns>
        /// TODO Edit XML Comment Template for CreateWizardContent
        IWizardContent CreateWizardContent(string groupKey, object formData = null);

        /// <summary>
        /// Generates the structure and performs placeholder parsing for a Wizard Screen
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="screenKey"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        IWizardContent CreateWizardScreenContent(string groupKey, string screenKey, object formData = null);

        /// <summary>
        /// Generates the structure and performs placeholder parsing for a Wizard Email 
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="parentKey"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        WizardContentEmail CreateWizardContentEmail(string groupKey, string parentKey, object formData = null);

        /// <summary>
        /// Resolves placeholders in the string using data provided by the replacementObject
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="replacementObject"></param>
        /// <returns></returns>
        string ResolvePlaceholders(string sourceString, object replacementObject = null);

    }
}