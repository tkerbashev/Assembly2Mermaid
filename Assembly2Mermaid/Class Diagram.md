
-----------------------------------------------

```mermaid
classDiagram
    class Assembly2MermaidForm {
        -BtnSource_Click()
        -BtnOutputBrowse_Click()
        -BtnExtract_Click()
        -RbDirectory_CheckedChanged()
        -RbScreenOutput_CheckedChanged()
        -SelectSourceFile()
        -SelectSourceDirectory()
        -SelectTargetFile()
        -ValidateInputs()
        -Dispose()
        -InitializeComponent()
        -_validator: IValidator
        -_extractor: IExtractor
        -components: IContainer
        -lblSource: Label
        -tbSource: TextBox
        -dlgSelectSource: OpenFileDialog
        -btnSource: Button
        -rbDirectory: RadioButton
        -rbFile: RadioButton
        -lblSelectFileOrDirectory: Label
        -lblOutput: Label
        -rbScreenOutput: RadioButton
        -rbFileOutput: RadioButton
        -gbInspect: GroupBox
        -gbOutput: GroupBox
        -lblOutputDestination: Label
        -tbOutput: TextBox
        -btnOutputBrowse: Button
        -btnExtract: Button
        -lblError: Label
    }

    class FrmDiagram {
        -BtnClose_Click()
        -BtnCopy_Click()
        -Dispose()
        -InitializeComponent()
        -components: IContainer
        -tbDiagram: TextBox
        -btnClose: Button
        -btnCopy: Button
    }

    class Program {
    <<>>
    }

    class Diagram {
        -Finish()
        -AddClass()
        -FinishClass()
        -AddMember()
        -AddInheritance()
        -AddAssociations()
        -Result: String
        -writer: StringWriter
    }

    class Extractor {
        +Extract()
        +Messages: String
        +HasProcessedValidFile: Boolean
        -ScanDirectory()
        -CheckIfValidAssembly()
        -ExamineFile()
        -ExamineType()
        -diagram: Diagram
        -assemblyTypes: Types
        -_fileProvider: IFileProvider
        -_directoryProvider: IDirectoryProvider
        -_assemblyProviderFactory: IAssemblyProviderFactory
        -ErrorMessages: List~String~
        -systemMethodNames: List~String~
    }

    class IFileProvider {
        <<Interface>>
        +Exists()
    }

    class IValidator {
        <<Interface>>
        +ValidateDirectoryPath()
        +ValidateFilePath()
    }

    class FileProvider {
    <<>>
    }

    class Types {
        +Add()
        +AddAssemblyName()
        +ListDescendants()
        +ListAssociations()
        -PopulateDescendants()
        -PopulateAssociations()
        -GetAllTypes()
        -GetReferencedTypesNames()
        -GetFieldsReferences()
        -GetMethodsReferences()
        -GetNestedTypesReferences()
        -typerecords: List~TypeRecord~
        -allTypes: Lazy`1
        -_participatingAssemblies: List~String~
        -TypeRecord: Object
    }

    class Validator {
        +ValidateDirectoryPath()
        +ValidateFilePath()
        -_fileProvider: IFileProvider
        -_directoryProvider: IDirectoryProvider
    }

    class AssemblyProvider {
        +LoadFrom()
        +GetTypes()
        +Name: String
        +IsValid: Boolean
        -_assembly: Assembly
    }

    class AssemblyProviderFactory {
        +NewAssemblyProvider: IAssemblyProvider
    }

    class DirectoryProvider {
    <<>>
    }

    class IAssemblyProvider {
        <<Interface>>
        +LoadFrom()
        +GetTypes()
        +Name: String
        +IsValid: Boolean
    }

    class IAssemblyProviderFactory {
        <<Interface>>
        +NewAssemblyProvider: IAssemblyProvider
    }

    class IDirectoryProvider {
        <<Interface>>
        +GetFiles()
        +Exists()
    }

    class IExtractor {
        <<Interface>>
        +Extract()
        +Messages: String
        +HasProcessedValidFile: Boolean
    }

    class TypeRecord {
        +AssemblyType: Type
        +Descendants: List~String~
        +Associates: List~String~
    }


    FileProvider --|> IFileProvider
    Validator --|> IValidator
    AssemblyProvider --|> IAssemblyProvider
    AssemblyProviderFactory --|> IAssemblyProviderFactory
    DirectoryProvider --|> IDirectoryProvider
    Extractor --|> IExtractor
    Assembly2MermaidForm --> IValidator
    Assembly2MermaidForm --> IExtractor
    Diagram --> Types
    Extractor --> Diagram
    Extractor --> Types
    Extractor --> IFileProvider
    Extractor --> IDirectoryProvider
    Extractor --> IAssemblyProviderFactory
    Types --> TypeRecord
    Validator --> IFileProvider
    Validator --> IDirectoryProvider
    AssemblyProviderFactory --> IAssemblyProvider
    IAssemblyProviderFactory --> IAssemblyProvider

```

-----------------------------------------------
