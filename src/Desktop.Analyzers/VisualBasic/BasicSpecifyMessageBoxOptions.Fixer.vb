' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports System.Composition
Imports Desktop.Analyzers
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.CodeFixes

Namespace Desktop.VisualBasic.Analyzers
    ''' <summary>
    ''' CA1300: Specify MessageBoxOptions
    ''' </summary>
    <ExportCodeFixProvider(LanguageNames.VisualBasic), [Shared]>
    Public NotInheritable Class BasicSpecifyMessageBoxOptionsFixer
        Inherits SpecifyMessageBoxOptionsFixer

    End Class
End Namespace
