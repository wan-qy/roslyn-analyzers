// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.QualityGuidelines.Analyzers;

namespace Microsoft.QualityGuidelines.CSharp.Analyzers
{
    /// <summary>
    /// CA2109: Review visible event handlers
    /// </summary>
    [ExportCodeFixProvider(LanguageNames.CSharp), Shared]
    public sealed class CSharpReviewVisibleEventHandlersFixer : ReviewVisibleEventHandlersFixer
    {
    }
}