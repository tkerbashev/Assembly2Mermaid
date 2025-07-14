using MermaidExtractionEngine.Interfaces;

namespace Assembly2Mermaid
{
    public partial class Assembly2MermaidForm : Form
    {
        private readonly IValidator _validator;
        private readonly IExtractor _extractor;
        public Assembly2MermaidForm(IValidator validator, IExtractor extractor)
        {
            InitializeComponent();
            _validator = validator;
            _extractor = extractor;
        }

        private void BtnSource_Click( object sender, EventArgs e )
        {
            if ( rbDirectory.Checked ) 
            {
                SelectSourceDirectory();
            }
            else
            {
                SelectSourceFile();
            }
        }

        private void BtnOutputBrowse_Click( object sender, EventArgs e )
        {
            SelectTargetFile();
        }

        private void BtnExtract_Click( object sender, EventArgs e )
        {
            (var result, var errorMessage) = ValidateInputs( );
            if (result)
            {
                lblError.Text = string.Empty;
                try
                {
                    var diagram = _extractor.Extract( rbDirectory.Checked, tbSource.Text );
                    // Output to screen if that option is selected,
                    // but if a single file is selected that is not in a valid format, only display a message at the bottom
                    lblError.Text = _extractor.Messages;
                    if (_extractor.HasProcessedValidFile)
                    {
                        if (rbScreenOutput.Checked)
                        {
                            var diagramForm = new FrmDiagram( diagram );
                            diagramForm.ShowDialog( );
                        }
                        else
                        {
                            File.WriteAllText( tbOutput.Text, diagram );
                        }
                    }
                    else
                    {
                        lblError.Text = rbFile.Checked ? "The selected file is not a valid .Net assembly file!" : "No valid files were found to be processed!";
                    }
                }
                catch (Exception ex) 
                {
                    lblError.Text = ex.Message;
                }
            }
            else
            {
                lblError.Text = errorMessage;
            }
        }

        private void RbDirectory_CheckedChanged( object sender, EventArgs e )
        {
            tbSource.Text = string.Empty;

            lblSource.Text = rbDirectory.Checked ? "Source Directory:" : "Source File:";
        }

        private void RbScreenOutput_CheckedChanged( object sender, EventArgs e )
        {
            if ( rbScreenOutput.Checked ) 
            {
                btnOutputBrowse.Visible = false;
                lblOutputDestination.Visible = false;
                tbOutput.Visible = false;
            }
            else 
            {
                btnOutputBrowse.Visible = true;
                lblOutputDestination.Visible = true;
                tbOutput.Visible = true;
            }
        }

        private void SelectSourceFile()
        {
            using var openFileDialog = new OpenFileDialog( );

            openFileDialog.Filter = "exe files (*.exe)|*.exe|dll files (*.dll)|*.dll|executables(*.exe or *.dll)|*.exe;*.dll";
            openFileDialog.FilterIndex = 3;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog( ) == DialogResult.OK)
            {
                //Get the path of specified file
                tbSource.Text = openFileDialog.FileName;
            }
            else
            {
                tbSource.Text = string.Empty;
            }
        }

        private void SelectSourceDirectory( )
        {
            using var folderBrowserDialog = new FolderBrowserDialog( );

            if ( folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified directory
                tbSource.Text = folderBrowserDialog.SelectedPath;
            }
            else
            {
                tbSource.Text = string.Empty;
            }
        }

        private void SelectTargetFile()
        {
            using var saveFileDialog = new SaveFileDialog( );

            saveFileDialog.Filter = "md files (*.md)|*.md|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog( ) == DialogResult.OK)
            {
                //Get the path of specified file
                tbOutput.Text = saveFileDialog.FileName;
            }
            else
            {
                tbOutput.Text = string.Empty;
            }
        }

        private (bool, string) ValidateInputs()
        {
            (bool result, string message) validationResult;
            if (rbDirectory.Checked) 
            {
                validationResult = _validator.ValidateDirectoryPath(tbSource.Text);
            }
            else 
            {
                validationResult = _validator.ValidateFilePath(tbSource.Text);
            }

            if (validationResult.result && rbFileOutput.Checked)
            {
                validationResult = _validator.ValidateFilePath(tbOutput.Text, strict: false);
            }

            return validationResult;
        }

    }
}
