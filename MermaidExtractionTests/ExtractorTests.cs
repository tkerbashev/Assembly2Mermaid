using MermaidExtractionEngine;
using MermaidExtractionTests.Providers;

namespace MermaidExtractionTests;

public class ExtractorTests
{
    private Extractor _extractor;

    [SetUp]
    public void Setup( )
    {
    FakeFileProvider fakeFileProvider = new ( );
    FakeAssemblyProviderFactory fakeAssemblyProviderFactory = new( );
    FakeDirectoryProvider fakeDirectoryProvider = new( );

    _extractor = new Extractor( fakeFileProvider, fakeDirectoryProvider, fakeAssemblyProviderFactory );
    }

    [Test]
    public void ExtractThrowsWhenDirectoryPathIsInvalid( )
    {
        Assert.That( ( ) => _extractor.Extract( true, "invalid directory path" ), Throws.Exception );
    }

    [Test]
    public void ExtractThrowsWhenFilePathIsInvalid( )
    {
        Assert.That( ( ) => _extractor.Extract( false, "invalid file path" ), Throws.Exception );
    }

    [Test]
    public void ExtractPassesWhenDirectoryPathIsValid( )
    {
        var result = _extractor.Extract( true, "valid path to directory with dll" );

        var expectedResult = @"```mermaid
classDiagram
    class String {
    <<>>
    }



```
";

        Assert.That( result, Is.EqualTo( expectedResult ) );
    }

    [Test]
    public void ExtractPassesWhenFilePathIsValid( )
    {
        var result = _extractor.Extract( false, "valid path to dll" );

        var expectedResult = @"```mermaid
classDiagram
    class String {
    <<>>
    }



```
";

        Assert.That( result, Is.EqualTo( expectedResult ) );
    }

    [Test]
    public void NoErrorMessageForValidFile()
    {
        _ = _extractor.Extract( false, "valid path to file1.dll" );
        Assert.That( _extractor.Messages, Is.Empty );
    }

    [Test]
    public void ReturnsErrorMessageForMultipleFilesDirectory( )
    {
        _ = _extractor.Extract( true, "valid path to directory with exe" );
        Assert.That( _extractor.Messages, Is.EqualTo( "Skipping the file file1.exe, it is not in a valid format!" ) );
    }

    [Test]
    public void ProcessesFileWhenOneIsFound( )
    {
        _ = _extractor.Extract( false, "valid path to file1.dll" );
        Assert.That( _extractor.HasProcessedValidFile, Is.True );
    }

    [Test]
    public void NoProcessesedFileWhenNoneAreFound( )
    {
        _ = _extractor.Extract( false, "valid path to file1.exe" );
        Assert.That( _extractor.HasProcessedValidFile, Is.False );
    }

}
