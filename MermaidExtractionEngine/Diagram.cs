using System.Reflection;

namespace MermaidExtractionEngine
{
    /// <summary>
    /// Represents the Mermaid class diagram. Facilitates the gathering of
    /// the information and the arranging the elements of the diagram.
    /// </summary>
    internal class Diagram
    {
        private readonly StringWriter? writer;

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
        internal void FinishClass ()
        {
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
                _ => null
            };

            var fieldType = string.Empty;

            if (IsTypeMatching( memberType, typeof( List<> ) , "List", ref fieldType)) return fieldType;
            if (IsTypeMatching( memberType, typeof( IEnumerable<> ), "IEnumerable", ref fieldType )) return fieldType;
            if (IsTypeMatching( memberType, typeof( Queue<> ), "Queue", ref fieldType )) return fieldType;
            if (IsTypeMatching( memberType, typeof( Stack<> ), "Stack", ref fieldType )) return fieldType;
            if (IsTypeMatching( memberType, typeof( Dictionary<object,object> ), "Dictionary", ref fieldType )) return fieldType;
            if (IsTypeMatching( memberType, typeof( SortedList<object,object> ), "SortedList", ref fieldType )) return fieldType;
            if (IsTypeMatching( memberType, typeof( HashSet<> ), "HashSet", ref fieldType )) return fieldType;
            if (IsTupleMatching( memberType, typeof( ValueTuple<object, object> ), "ValueTuple", ref fieldType )) return fieldType;
            if (IsTupleMatching( memberType, typeof( Tuple<object, object> ), "Tuple", ref fieldType )) return fieldType;

            return memberType?.Name ?? "unknown";
        }

        private static bool IsTypeMatching( Type? ptype, Type knownType, string prefix, ref string fieldNamesList )
        {
            if ( ptype == null )
            {
                return false;
            }

            if (ptype.IsGenericType && ptype.GetGenericTypeDefinition( ).Name == knownType.Name)
            {
                var arguments = new List<string>();
                foreach ( var arg in ptype.GetGenericArguments() ) 
                {
                    arguments.Add( arg.Name );
                }
                fieldNamesList = $"{prefix}~{string.Join(",", arguments)}~";
                return true ;
            }

            return false;
        }

        private static bool IsTupleMatching( Type? ptype, Type knownType, string prefix, ref string fieldNamesList )
        {
            if (ptype == null)
            {
                return false;
            }

            if (ptype.IsGenericType)
            {
                var nParameters = ptype.GetGenericArguments( ).Length;
                //For tuples with more than two elements the number is included in the name
                var knownTypeName = knownType.Name[ ..(knownType.Name.IndexOf( '`' ) + 1) ] + nParameters.ToString();

                if (ptype.GetGenericTypeDefinition( ).Name == knownTypeName)
                {
                    var arguments = new List<string>( );
                    foreach (var arg in ptype.GetGenericArguments( ))
                    {
                        arguments.Add( arg.Name );
                    }
                    fieldNamesList = $"{prefix}~{string.Join( ",", arguments )}~";
                    return true;
                }
            }



            return false;
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
}
