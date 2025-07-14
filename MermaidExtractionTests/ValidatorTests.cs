using MermaidExtractionEngine;
using MermaidExtractionTests.Providers;

namespace MermaidExtractionTests;

public class ValidatorTests
{
    private Validator _validator;

    [SetUp]
    public void Setup( )
    {
        FakeFileProvider fakeFileProvider = new( );
        FakeAssemblyProviderFactory fakeAssemblyProviderFactory = new( );
        FakeDirectoryProvider fakeDirectoryProvider = new( );

        _validator = new Validator( fakeFileProvider, fakeDirectoryProvider );
    }

    [Test]
    public void FailsForMissingDirectoryPath()
    {
        (bool success, string message) result = _validator.ValidateDirectoryPath( string.Empty );
        Assert.That( result.success, Is.False );
        Assert.That( result.message, Is.EqualTo( "Please provide input directory!" ) );
    }

    [Test]
    public void FailsForInvalidDirectoryPath( )
    {
        (bool success, string message) result = _validator.ValidateDirectoryPath( "invalid directory path" );
        Assert.That( result.success, Is.False );
        Assert.That( result.message, Is.EqualTo( "Please provide a valid input directory!" ) );
    }

    [Test]
    public void SucceedsForValidDirectoryPath( )
    {
        (bool success, string message) result = _validator.ValidateDirectoryPath( "valid path" );
        Assert.That( result.success, Is.True );
        Assert.That( result.message, Is.EqualTo( string.Empty ) );
    }

    [Test]
    public void FailsForMissingFilePath( )
    {
        (bool success, string message) result = _validator.ValidateFilePath( string.Empty );
        Assert.That( result.success, Is.False );
        Assert.That( result.message, Is.EqualTo( "Please provide a file name!" ) );
    }

    [Test]
    public void FailsForStrictInvalidFilePath( )
    {
        (bool success, string message) result = _validator.ValidateFilePath( "invalid file path" );
        Assert.That( result.success, Is.False );
        Assert.That( result.message, Is.EqualTo( "Please provide a valid path to a .Net executable file!" ) );
    }

    [Test]
    public void FailsForNonStrictInvalidFilePath( )
    {
        (bool success, string message) result = _validator.ValidateFilePath( "invalid file path", false );
        Assert.That( result.success, Is.False );
        Assert.That( result.message, Is.EqualTo( "Please provide a path containing a valid directory!" ) );
    }

    [Test]
    public void SucceedsForValidFilePath( )
    {
        (bool success, string message) result = _validator.ValidateFilePath( "valid path" );
        Assert.That( result.success, Is.True );
        Assert.That( result.message, Is.EqualTo( string.Empty ) );
    }

}
