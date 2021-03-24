﻿// -----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Conglomo">
// Copyright 2020-2021 Conglomo Limited. Please see LICENSE for license details.
// </copyright>
// -----------------------------------------------------------------------

namespace GoToBible.Windows
{
    using System.Drawing;
    using GoToBible.Model;

    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns a <see cref="RenderFont" /> as a <see cref="Font" />.
        /// </summary>
        /// <param name="renderFont">The render font.</param>
        /// <returns>
        /// The <see cref="Font" /> corresponding to the <see cref="RenderFont" />.
        /// </returns>
        public static Font AsFont(this RenderFont renderFont)
            => new Font(
                renderFont.FamilyName,
                renderFont.SizeInPoints,
                (renderFont.Bold ? FontStyle.Bold : FontStyle.Regular) & (renderFont.Italic ? FontStyle.Italic : FontStyle.Regular) & (renderFont.Strikeout ? FontStyle.Strikeout : FontStyle.Regular) & (renderFont.Underline ? FontStyle.Underline : FontStyle.Regular),
                GraphicsUnit.Point);

        /// <summary>
        /// Returns a <see cref="Font" /> as a <see cref="RenderFont" />.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <returns>
        /// The <see cref="RenderFont" /> corresponding to the <see cref="Font" />.
        /// </returns>
        public static RenderFont AsRenderFont(this Font font) => new RenderFont
        {
            FamilyName = font.FontFamily.Name,
            Bold = font.Bold,
            Italic = font.Italic,
            SizeInPoints = font.SizeInPoints,
            Strikeout = font.Strikeout,
            Underline = font.Underline,
        };

        /// <summary>
        /// Gets the unique key for the translation.
        /// </summary>
        /// <param name="translation">The translation.</param>
        /// <returns>
        /// The unique key.
        /// </returns>
        public static string UniqueKey(this Translation translation) => $"{translation.Provider}-{translation.Code}";
    }
}
