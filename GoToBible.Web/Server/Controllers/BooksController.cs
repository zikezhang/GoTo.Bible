﻿// -----------------------------------------------------------------------
// <copyright file="BooksController.cs" company="Conglomo">
// Copyright 2020-2024 Conglomo Limited. Please see LICENSE.md for license details.
// </copyright>
// -----------------------------------------------------------------------

namespace GoToBible.Web.Server.Controllers;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using GoToBible.Model;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// The books controller.
/// </summary>
/// <seealso cref="ControllerBase" />
[ApiController]
[Route("v1/[controller]")]
public class BooksController(IEnumerable<IProvider> providers) : ControllerBase
{
    /// <summary>
    /// The providers.
    /// </summary>
    private readonly IEnumerable<IProvider> providers = providers;

    /// <summary>
    /// GET: <c>/v1/Books?translation={translation_id}&amp;provider={provider_id}&amp;includeChapters={true_or_false}</c>.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="translation">The translation.</param>
    /// <param name="includeChapters">If set to <c>true</c>, include chapters. This can include significantly more data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The list of the books in the translation.
    /// </returns>
    [HttpGet]
    public async IAsyncEnumerable<Book> Get(
        string provider,
        string translation,
        bool includeChapters,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        IProvider? bookProvider = this.providers.SingleOrDefault(p => p.Id == provider);
        if (bookProvider is null)
        {
            yield break;
        }

        await foreach (
            Book book in bookProvider.GetBooksAsync(translation, includeChapters, cancellationToken)
        )
        {
            yield return book;
        }
    }
}
