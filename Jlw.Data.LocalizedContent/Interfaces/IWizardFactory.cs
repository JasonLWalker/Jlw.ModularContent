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
    }
}