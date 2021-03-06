﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeFixes;
using Test.Utilities;
using Xunit;

namespace Microsoft.ApiDesignGuidelines.Analyzers.UnitTests
{
    public partial class DefineAccessorsForAttributeArgumentsTests : CodeFixTestBase
    {
        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new DefineAccessorsForAttributeArgumentsFixer();
        }

        protected override CodeFixProvider GetBasicCodeFixProvider()
        {
            return new DefineAccessorsForAttributeArgumentsFixer();
        }

        [Fact]
        public void CSharp_CA1019_AddAccessor()
        {
            VerifyCSharpFix(@"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class NoAccessorTestAttribute : Attribute
{
    private string m_name;

    public NoAccessorTestAttribute(string name)
    {
        m_name = name;
    }
}", @"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class NoAccessorTestAttribute : Attribute
{
    private string m_name;

    public NoAccessorTestAttribute(string name)
    {
        m_name = name;
    }

    public string Name { get; }
}");
        }

        [Fact]
        public void CSharp_CA1019_AddAccessor1()
        {
            VerifyCSharpFix(@"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class SetterOnlyTestAttribute : Attribute
{
    private string m_name;

    public SetterOnlyTestAttribute(string name)
    {
        m_name = name;
    }

    public string Name 
    { 
        set { m_name = value; }
    }
}", @"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class SetterOnlyTestAttribute : Attribute
{
    private string m_name;

    public SetterOnlyTestAttribute(string name)
    {
        m_name = name;
    }

    public string Name 
    {
        internal set { m_name = value; }

        get
        {
            throw new NotImplementedException();
        }
    }
}", allowNewCompilerDiagnostics: true);
        }

        [Fact]
        public void CSharp_CA1019_MakeGetterPublic()
        {
            VerifyCSharpFix(@"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class InternalGetterTestAttribute : Attribute
{
    private string m_name;

    public InternalGetterTestAttribute(string name)
    {
        m_name = name;
    }

    internal string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }
}", @"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class InternalGetterTestAttribute : Attribute
{
    private string m_name;

    public InternalGetterTestAttribute(string name)
    {
        m_name = name;
    }

    public string Name
    {
        get { return m_name; }

        internal set { m_name = value; }
    }
}");
        }

        [Fact]
        public void CSharp_CA1019_MakeGetterPublic2()
        {
            VerifyCSharpFix(@"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class InternalGetterTestAttribute : Attribute
{
    private string m_name;

    public InternalGetterTestAttribute(string name)
    {
        m_name = name;
    }

    internal string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }
}", @"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class InternalGetterTestAttribute : Attribute
{
    private string m_name;

    public InternalGetterTestAttribute(string name)
    {
        m_name = name;
    }

    public string Name
    {
        get { return m_name; }

        internal set { m_name = value; }
    }
}");
        }

        [Fact]
        public void CSharp_CA1019_MakeGetterPublic3()
        {
            VerifyCSharpFix(@"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class InternalGetterTestAttribute : Attribute
{
    private string m_name;

    public InternalGetterTestAttribute(string name)
    {
        m_name = name;
    }

    internal string Name
    {
        get { return m_name; }
    }
}", @"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class InternalGetterTestAttribute : Attribute
{
    private string m_name;

    public InternalGetterTestAttribute(string name)
    {
        m_name = name;
    }

    public string Name
    {
        get { return m_name; }
    }
}");
        }

        [Fact]
        public void CSharp_CA1019_MakeSetterInternal()
        {
            VerifyCSharpFix(@"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class PublicSetterTestAttribute : Attribute
{
    private string m_name;

    public PublicSetterTestAttribute(string name)
    {
        m_name = name;
    }

    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }
}", @"
using System;

[AttributeUsage(AttributeTargets.All)]
public sealed class PublicSetterTestAttribute : Attribute
{
    private string m_name;

    public PublicSetterTestAttribute(string name)
    {
        m_name = name;
    }

    public string Name
    {
        get { return m_name; }

        internal set { m_name = value; }
    }
}");
        }

        [Fact]
        public void VisualBasic_CA1019_AddAccessor()
        {
            VerifyBasicFix(@"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class NoAccessorTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub
End Class", @"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class NoAccessorTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Public ReadOnly Property Name As String
        Get
        End Get
    End Property
End Class", allowNewCompilerDiagnostics: true);
        }

        [Fact]
        public void VisualBasic_CA1019_AddAccessor2()
        {
            VerifyBasicFix(@"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class SetterOnlyTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Friend WriteOnly Property Name() As String
        Set
            m_name = value
        End Set
    End Property
End Class", @"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class SetterOnlyTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Public Property Name() As String
        Friend Set
            m_name = value
        End Set
        Get
            Throw New NotImplementedException()
        End Get
    End Property
End Class", allowNewCompilerDiagnostics: true);
        }

        [Fact]
        public void VisualBasic_CA1019_MakeGetterPublic()
        {
            VerifyBasicFix(@"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class InternalGetterTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Public Property Name() As String
        Friend Get
            Return m_name
        End Get
        Set
            m_name = value
        End Set
    End Property
End Class", @"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class InternalGetterTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Public Property Name() As String
        Get
            Return m_name
        End Get
        Friend Set
            m_name = value
        End Set
    End Property
End Class", allowNewCompilerDiagnostics: true);
        }

        [Fact]
        public void VisualBasic_CA1019_MakeGetterPublic2()
        {
            VerifyBasicFix(@"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class InternalGetterTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Friend Property Name() As String
        Get
            Return m_name
        End Get
        Set
            m_name = value
        End Set
    End Property
End Class", @"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class InternalGetterTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Public Property Name() As String
        Get
            Return m_name
        End Get
        Friend Set
            m_name = value
        End Set
    End Property
End Class", allowNewCompilerDiagnostics: true);
        }

        [Fact]
        public void VisualBasic_CA1019_MakeGetterPublic3()
        {
            VerifyBasicFix(@"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class InternalGetterTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Friend ReadOnly Property Name() As String
        Get
            Return m_name
        End Get
    End Property
End Class", @"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class InternalGetterTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Public ReadOnly Property Name() As String
        Get
            Return m_name
        End Get
    End Property
End Class", allowNewCompilerDiagnostics: true);
        }

        [Fact]
        public void VisualBasic_CA1019_MakeSetterInternal()
        {
            VerifyBasicFix(@"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class PublicSetterTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Public Property Name() As String
        Get
            Return m_name
        End Get
        Set
            m_name = value
        End Set
    End Property
End Class", @"
Imports System

<AttributeUsage(AttributeTargets.All)> _
Public NotInheritable Class PublicSetterTestAttribute
    Inherits Attribute
    Private m_name As String
    
    Public Sub New(name As String)
        m_name = name
    End Sub

    Public Property Name() As String
        Get
            Return m_name
        End Get
        Friend Set
            m_name = value
        End Set
    End Property
End Class", allowNewCompilerDiagnostics: true);
        }
    }
}
