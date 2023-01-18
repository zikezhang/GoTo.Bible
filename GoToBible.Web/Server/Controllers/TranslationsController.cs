﻿// -----------------------------------------------------------------------
// <copyright file="TranslationsController.cs" company="Conglomo">
// Copyright 2020-2023 Conglomo Limited. Please see LICENSE.md for license details.
// </copyright>
// -----------------------------------------------------------------------

namespace GoToBible.Web.Server.Controllers;

using System.Collections.Generic;
using GoToBible.Model;
using GoToBible.Providers;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// The translations controller.
/// </summary>
/// <seealso cref="ControllerBase" />
[ApiController]
[Route("v1/[controller]")]
[Route("[controller]")]
public class TranslationsController : ControllerBase
{
    /// <summary>
    /// The providers.
    /// </summary>
    private readonly IEnumerable<IProvider> providers;

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslationsController" /> class.
    /// </summary>
    /// <param name="providers">The providers.</param>
    public TranslationsController(IEnumerable<IProvider> providers) => this.providers = providers;

    /// <summary>
    /// GET: <c>/v1/Translations</c>.
    /// </summary>
    /// <returns>
    /// The list of available translations.
    /// </returns>
    [HttpGet]
    public async IAsyncEnumerable<Translation> Get()
    {
        foreach (IProvider provider in this.providers)
        {
            await foreach (Translation translation in provider.GetTranslationsAsync())
            {
                // Clean up any names we are displaying
                if (ApiProvider.NameSubstitutions.TryGetValue(translation.Name, out string? translationName))
                {
                    translation.Name = translationName;
                }

                // Make sure this isn't a blocked translation
                if (!ApiProvider.BlockedTranslations.Contains($"{translation.Provider}-{translation.Code}"))
                {
                    yield return translation;
                }
            }
        }
    }
}
