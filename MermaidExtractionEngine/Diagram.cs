using System.Reflection;

namespace MermaidExtractionEngine;

/// <summary>
/// Represents the Mermaid class diagram. Facilitates the gathering of
/// the information and the arranging the elements of the diagram.
/// </summary>
internal class Diagram
{
    private readonly StringWriter? writer;

    private static readonly List<string> knownGenericTypes = [ "List", "IEnumerable", "Queue", "Stack", "Dictionary",
                                                        "SortedList", "HashSet", "ValueTuple", "Tuple"];

    private static readonly Dictionary<string, string> typesWithShortcuts = new() { { "MulticastDelegate", "Delegate" }, { "EventHandler", "Event" } };


    /// <summary>
    /// Constructor. Performs initialisation and marks it as a clas diagram.
    /// </summary>
    public Diagram( )
    {
        writer = new( );
        writer.WriteLine( "```mermaid" );
        writer?.WriteLine( "classDiagram" );
    }

    /// <summary>
    /// Adds the details and finishes the diagram.
    /// </summary>
    /// <param name="assemblyTypes"></param>
    internal void Finish( Types? assemblyTypes )
    {
        if ( assemblyTypes != null ) 
        {
            AddInheritance( assemblyTypes );
            AddAssociations( assemblyTypes );
        }

        writer?.WriteLine( "```" );
        writer?.Flush( );
    }

    /// <summary>
    /// Exposes the Mermaid diagram as a string.
    /// </summary>
    internal string Result 
    { 
        get => writer?.ToString() ?? string.Empty; 
    }

    /// <summary>
    /// Starts adding a new type to the diagram.
    /// </summary>
    /// <param name="type">The new type to be added</param>
    internal void AddClass( Type type )
    {
        writer?.WriteLine( $"    class {type.Name} {{" );

        if ( type.IsInterface)
        {
            writer?.WriteLine( "        <<Interface>>" );
        } else if (type.IsEnum)
        {
            writer?.WriteLine( "        <<Enum>>" );
        }

    }

    /// <summary>
    /// Finalises the adding of a new type to the diagram.
    /// </summary>
    /// <param name="hasMembers">True if at least one member has been added, false otherwise</param>
    internal void FinishClass ( bool hasMembers)
    {
        if (!hasMembers)
        {
            writer?.WriteLine( "    <<>>" );
        }
        writer?.WriteLine("    }");
        writer?.WriteLine();
    }

    /// <summary>
    /// Adds a new member to the current type description.
    /// </summary>
    /// <param name="member">The type member to be added</param>
    /// <param name="isPublic">Trus if public, false otherwise</param>
    internal void AddMember(MemberInfo member, bool isPublic)
    {
        var prefix = isPublic ? "+" : "-";
        var fieldType = GetFieldType( member );

        if (member.MemberType == MemberTypes.Method)
        {
            writer?.WriteLine( $"        {prefix}{member.Name}()" );
        }
        else
        {
            writer?.WriteLine( $"        {prefix}{member.Name}: {fieldType}" );
        }
    }

    private static string GetFieldType( MemberInfo member )
    {
        var memberType = member switch
        {
            FieldInfo fm => fm.FieldType,
            PropertyInfo pm => pm.PropertyType,
            EventInfo em => em.EventHandlerType,
            Type tm => tm.BaseType,
            _ => null
        };

        if (memberType is null)
        {
            return "unknown";
        }

        return memberType.IsGenericType ? UpdateGenericTypeName( memberType ) : UpdateRegularTypeName( memberType );
    }

    private static string UpdateGenericTypeName( Type mtype )
    {
        var matchingTypeName = knownGenericTypes.FirstOrDefault( predicate: mtype.Name.StartsWith );

        if (matchingTypeName is null)
        {
            return mtype.Name;
        }

        var arguments = new List<string>( );
        foreach (var arg in mtype.GetGenericArguments( ))
        {
            arguments.Add( arg.Name );
        }
        return $"{matchingTypeName}~{string.Join( ",", arguments )}~";
    }

    private static string UpdateRegularTypeName( Type mtype )
    {
        if (typesWithShortcuts.TryGetValue( mtype.Name, out string? value )) 
        {
            return value;
        }

        return mtype.Name;
    }

    private void AddInheritance( Types assemblyTypes )
    {
        writer?.WriteLine( );

        foreach ( var (type, descendant) in assemblyTypes.ListDescendants() ) 
        {
            writer?.WriteLine( $"    {descendant} --|> {type}" );
        }
    }

    private void AddAssociations( Types assemblyTypes )
    {
        foreach (var (type, associate) in assemblyTypes.ListAssociations( ))
        {
            writer?.WriteLine( $"    {type} --> {associate}" );
        }

        writer?.WriteLine( );
    }
}
