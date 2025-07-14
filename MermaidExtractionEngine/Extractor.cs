using MermaidExtractionEngine.Interfaces;
using System.Diagnostics;
using System.Reflection;

namespace MermaidExtractionEngine;

public class Extractor( IFileProvider fileProvider, IDirectoryProvider directoryProvider, IAssemblyProviderFactory assemblyProviderFactory ) : IExtractor
{
    private Diagram? diagram;
    private Types? assemblyTypes;
    private readonly IFileProvider _fileProvider = fileProvider;
    private readonly IDirectoryProvider _directoryProvider = directoryProvider;
    private readonly IAssemblyProviderFactory _assemblyProviderFactory = assemblyProviderFactory;
    private List<string> ErrorMessages = [];

    public string Messages => string.Join( Environment.NewLine, ErrorMessages );

    public bool HasProcessedValidFile { get; private set; }

    public string Extract( bool isInputDirectory, string path )
    {
        ErrorMessages = [];
        diagram = new();
        assemblyTypes = new();
        HasProcessedValidFile = false;

        if (isInputDirectory ) 
        {
            Debug.Assert( _directoryProvider.Exists( path ) );
            ScanDirectory( path);
        }
        else 
        {
            Debug.Assert( _fileProvider.Exists( path ) );
            ExamineFile(path, isExaminingSingleFile: true );
        }

        diagram.Finish(assemblyTypes);

        return diagram.Result;
    }

    private void ScanDirectory(string path)
    {
        var files = new List<string>();
        files.AddRange(_directoryProvider.GetFiles(path, "*.exe"));
        files.AddRange(_directoryProvider.GetFiles( path, "*.dll" ));

        List<string> validFileNames = [];
        foreach (var file in files)
        {
            CheckIfValidAssembly(file, validFileNames);
        }

        foreach (var file in validFileNames) 
        {
            ExamineFile(file, isExaminingSingleFile : false);
        }
    }

    private bool CheckIfValidAssembly( string filePath, List<string> validFileNames )
    {
        var assemblyProvider = _assemblyProviderFactory.NewAssemblyProvider;
        assemblyProvider.LoadFrom( filePath );
        if (assemblyProvider.IsValid)
        {
            assemblyTypes?.AddAssemblyName( assemblyProvider.Name );
            validFileNames.Add( filePath );
            return true;
        }
        else
        {
            ErrorMessages.Add( $"Skipping the file {Path.GetFileName( filePath )}, it is not in a valid format!" );
        }

        return true;
    }

    // We collect the list of the participating assemblies in order to filter the types we discover.
    // In the case of multiple files we loop through all the files before the actual processing.
    // In the case of a single file we still need the assembly name, hence the boolean flag and the check below.
    private void ExamineFile(string filePath, bool isExaminingSingleFile = false)
    {
        var assemblyProvider = _assemblyProviderFactory.NewAssemblyProvider;
        assemblyProvider.LoadFrom( filePath );
        HasProcessedValidFile = assemblyProvider.IsValid;
        if (isExaminingSingleFile) 
        {
            assemblyTypes?.AddAssemblyName( assemblyProvider.Name );
        }
        var types = assemblyProvider.GetTypes( );
        foreach (var type in types.Where( t => !t.CustomAttributes.Where( a => a.AttributeType.Name == "CompilerGeneratedAttribute" ).Any( ) ))
        {
            assemblyTypes?.Add( type );
            ExamineType( type, assemblyProvider.Name );
        }
    }

    private readonly List<string> systemMethodNames = new( [".ctor", ".cctor"] );

    private void ExamineType(Type type, string moduleName )
    {
        diagram!.AddClass( type );

        if (type.IsEnum)
        {
            // Assuming that Enum has no members we'll skip the examination
            diagram.FinishClass( true );
            return;
        }

        var membersAdded = false;
        foreach (var member in type.GetMembers( BindingFlags.Instance | BindingFlags.Public ).Where( m => !m.CustomAttributes.Where( a => a.AttributeType.Name == "CompilerGeneratedAttribute" ).Any( ) ))
        {
            if (member.Module.Name == moduleName && !systemMethodNames.Contains( member.Name ) && !member.Name.StartsWith( "get_" ))
            {
                diagram.AddMember( member, true );
                membersAdded = true;
            }
        }

        foreach (var member in type.GetMembers( BindingFlags.Instance | BindingFlags.NonPublic ).Where( m => !m.CustomAttributes.Where( a => a.AttributeType.Name == "CompilerGeneratedAttribute" ).Any( ) ))
        {
            if (member.Module.Name == moduleName && !systemMethodNames.Contains( member.Name ) && !member.Name.StartsWith( "get_" ))
            {
                diagram.AddMember( member, false );
                membersAdded = true;
            }
        }

        diagram.FinishClass( membersAdded );
    }


}
