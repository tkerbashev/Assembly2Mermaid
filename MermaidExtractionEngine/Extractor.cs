using System.Reflection;

namespace MermaidExtractionEngine
{
    public class Extractor: IExtractor
    {
        private Diagram? diagram;
        private Types? assemblyTypes;

        public string Messages {get; private set;} = string.Empty;

        public string Extract( bool isInputDirectory, string path )
        {
            Messages = string.Empty;
            diagram = new();
            assemblyTypes = new();

            if (isInputDirectory ) 
            {
                ScanDirectory(path);
            }
            else 
            {
                ExamineFile(path);
            }

            diagram.Finish(assemblyTypes);

            return diagram.Result;
        }

        private void ScanDirectory(string path)
        {
            var files = new List<string>();
            files.AddRange(Directory.GetFiles(path, "*.exe"));
            files.AddRange(Directory.GetFiles( path, "*.dll" ));

            foreach (var file in files) 
            {
                ExamineFile(file);
            }
        }

        private void ExamineFile(string path)
        {
            try
            {
                var assembly = Assembly.LoadFrom( path );
                var types = assembly.GetTypes( );
                foreach (var type in types.Where( t => !t.CustomAttributes.Where( a => a.AttributeType.Name == "CompilerGeneratedAttribute" ).Any() && t.Name != "Program"))
                {
                    assemblyTypes?.Add(type);
                    ExamineType( type, assembly.ManifestModule.Name );
                }
            }
            catch (BadImageFormatException)
            {
                var potentialNewLine = string.IsNullOrEmpty(Messages) ? string.Empty : Environment.NewLine;
                Messages = $"{Messages}{potentialNewLine}Skipping the file {Path.GetFileName( path )}, it is not in a valid format!";
            }
        }

        private readonly List<string> systemMethodNames = new( [".ctor", ".cctor"] );

        private void ExamineType(Type type, string moduleName)
        {
            diagram!.AddClass( type );

            if (type.IsEnum)
            {
                // Assuming that Enum has no members
                diagram.FinishClass( );
                return;
            }

            foreach (var member in type.GetMembers( BindingFlags.Instance | BindingFlags.Public ).Where( m => !m.CustomAttributes.Where( a => a.AttributeType.Name == "CompilerGeneratedAttribute" ).Any( ) ))
            {
                if (member.Module.Name == moduleName && !systemMethodNames.Contains( member.Name ) && !member.Name.StartsWith( "get_" ))
                {
                    diagram.AddMember( member, true );
                }
            }

            foreach (var member in type.GetMembers( BindingFlags.Instance | BindingFlags.NonPublic ).Where( m => !m.CustomAttributes.Where( a => a.AttributeType.Name == "CompilerGeneratedAttribute" ).Any( ) ))
            {
                if (member.Module.Name == moduleName && !systemMethodNames.Contains( member.Name ) && !member.Name.StartsWith( "get_" ))
                {
                    diagram.AddMember( member, false );
                }
            }

            diagram.FinishClass();
        }


    }
}
