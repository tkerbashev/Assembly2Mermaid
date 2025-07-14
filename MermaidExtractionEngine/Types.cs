using System.Reflection;

namespace MermaidExtractionEngine;

/// <summary>
/// Represents the collection of the types in the assembly and their relations.
/// </summary>
internal class Types
{
    private record TypeRecord( Type AssemblyType, List<string> Descendants, List<string> Associates );

    private readonly List<TypeRecord> typerecords = [];
    private readonly Lazy<List<Type>> allTypes;
    private readonly List<string> _participatingAssemblies = [];

    /// <summary>
    /// Constructor. Prepares for the collection of the types data.
    /// </summary>
    public Types( )
    {
        allTypes = new Lazy<List<Type>>( GetAllTypes );
    }

    /// <summary>
    /// Adds a new type to the collection.
    /// </summary>
    /// <param name="type">Type information to be added</param>
    public void Add(Type type)
    {
        typerecords.Add(new TypeRecord(type, [], []));
    }

    public void AddAssemblyName(string assemblyName)
    {
        if (string.IsNullOrWhiteSpace( assemblyName ) || _participatingAssemblies.Contains( assemblyName ))
        {
            return;
        }
        _participatingAssemblies.Add( assemblyName );
    }

    /// <summary>
    /// Lists the descendants of each type in the collection
    /// </summary>
    /// <returns>List of pairs of a type name and a descendant type name</returns>
    public List<(string type,string descendant)> ListDescendants()
    {
        List <(string type, string descendant)> result = [];
        PopulateDescendants();
        foreach (var typerecord in typerecords)
            foreach (var descendant in typerecord.Descendants)
            {
                result.Add( (typerecord.AssemblyType.Name, descendant) );
            }

        return result;
    }

    /// <summary>
    /// Lists the associates of each type in the collection
    /// </summary>
    /// <returns>List of pairs of a type name and an associated type name</returns>
    public List<(string type, string associate)> ListAssociations( )
    {
        List<(string type, string associate)> result = [ ];
        PopulateAssociations( );
        foreach (var typerecord in typerecords)
            foreach (var associate in typerecord.Associates)
            {
                var candidate = (typerecord.AssemblyType.Name, associate);
                if (!result.Contains(candidate))
                {
                    result.Add(candidate);
                }
            }

        return result;
    }


    private void PopulateDescendants()
    {
        foreach (var typerecord in typerecords)
            foreach (var otherType in allTypes.Value)
            {
                if (typerecord.AssemblyType.Name != otherType.Name &&
                    typerecord.AssemblyType.IsAssignableFrom( otherType ))
                {
                    typerecord.Descendants.Add(otherType.Name);
                }
            }
    }

    private void PopulateAssociations()
    {
        foreach (var typerecord in typerecords)
        {
            typerecord.Associates.Clear();
            typerecord.Associates.AddRange( GetReferencedTypesNames( typerecord.AssemblyType ) );
        }
    }

    private List<Type> GetAllTypes() 
    {
        var allTypes = new List<Type>( );
        foreach (var typerecord in typerecords)
        {
            allTypes.Add( typerecord.AssemblyType );
            typerecord.Descendants.Clear( );
        }

        return allTypes;
    }

    private List<string> GetReferencedTypesNames( Type type )
    {
        var result = new List<string>();

        GetFieldsReferences( type, ref result );
        GetMethodsReferences( type, ref result );
        GetNestedTypesReferences( type, ref result );

        return result;
    }

    private void GetFieldsReferences( Type type, ref List<string> typeReferences )
    {
        var fields = type.GetFields( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
        foreach (var field in fields)
        {
            //First check the field type
            if (!IsSystemType( field.FieldType ))
            {
                var fieldTypeName = field.FieldType.IsArray ? field.FieldType.GetElementType( )!.Name : field.FieldType.Name;
                if (_participatingAssemblies.Contains( field.FieldType.Assembly.ManifestModule.Name ) &&
                    !typeReferences.Contains( fieldTypeName ) && fieldTypeName != type.Name)
                {
                    typeReferences.Add( fieldTypeName );
                }
            }

            //Second, check the reference types, if any
            var referencedTypes = field.FieldType.GenericTypeArguments;
            foreach (var referencedType in referencedTypes)
            {
                if (IsSystemType( referencedType ))
                { continue; }

                var pureReferencedType = referencedType.IsArray ? referencedType.GetElementType( ) : referencedType;
                if (pureReferencedType is null)
                { continue; }
                var referencedTypeAssemblyName = referencedType.Assembly.ManifestModule.Name;
                if (_participatingAssemblies.Contains( referencedTypeAssemblyName ) && referencedType.FullName != null &&
                    !typeReferences.Contains( pureReferencedType.Name ) && pureReferencedType.Name != type.Name)
                {
                    typeReferences.Add( pureReferencedType.Name );
                }
            }
        }
    }

    private void GetMethodsReferences( Type type, ref List<string> typeReferences )
    {
        var methods = type.GetMethods( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
        foreach (var method in methods)
        {
            // First check the return type

            if (!IsSystemType( method.ReturnParameter.ParameterType ))
            {
                var pureReturnType = method.ReturnParameter.ParameterType.IsArray ?
                    method.ReturnParameter.ParameterType.GetElementType( ) : method.ReturnParameter.ParameterType;
                var returnTypeName = pureReturnType?.Name;
                var returnTypeAssemblyName = method.ReturnType.Assembly.ManifestModule.Name;
                if (_participatingAssemblies.Contains( returnTypeAssemblyName ) && returnTypeName != null &&
                    !typeReferences.Contains( returnTypeName ) && returnTypeName != type.Name)
                {
                    typeReferences.Add( returnTypeName );
                }
            }

            //Second, check the parameters
            var methodParameters = method.GetParameters( );
            foreach (var parameter in methodParameters)
            {
                if (IsSystemType( parameter.ParameterType ))
                { continue; }

                var pureParameterType = parameter.ParameterType.IsArray ?
                    parameter.ParameterType.GetElementType( ) : parameter.ParameterType;
                var parameterTypeName = pureParameterType?.Name;
                var parameterTypeAssemblyName = parameter.ParameterType.Assembly.ManifestModule.Name;
                if (_participatingAssemblies.Contains( parameterTypeAssemblyName ) && parameterTypeName != null &&
                    !typeReferences.Contains( parameterTypeName ) && parameterTypeName != type.Name)
                {
                    typeReferences.Add( parameterTypeName );
                }
            }
        }
    }

    private void GetNestedTypesReferences( Type type, ref List<string> typeReferences )
    {
        var nestedTypes = type.GetNestedTypes( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
        foreach (var nestedType in nestedTypes)
        {
            if (IsSystemType( nestedType ))
            { continue; }

            var pureNestedType = nestedType.IsArray ? nestedType.GetElementType( ) : nestedType;
            var nestedTypeAssemblyName = nestedType.Assembly.ManifestModule.Name;
            if (_participatingAssemblies.Contains( nestedTypeAssemblyName ) && pureNestedType?.Name != null &&
                !typeReferences.Contains( pureNestedType.Name ) && pureNestedType.Name != type.Name)
            {
                typeReferences.Add( pureNestedType.Name );
            }
        }
    }


    private static bool IsSystemType(Type type) => type.GetCustomAttribute<System.Runtime.CompilerServices.CompilerGeneratedAttribute>( ) != null;
}
