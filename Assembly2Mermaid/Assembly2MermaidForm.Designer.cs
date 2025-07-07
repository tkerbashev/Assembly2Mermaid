namespace Assembly2Mermaid
{
    partial class Assembly2MermaidForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            lblSource = new Label( );
            tbSource = new TextBox( );
            dlgSelectSource = new OpenFileDialog( );
            btnSource = new Button( );
            rbDirectory = new RadioButton( );
            rbFile = new RadioButton( );
            lblSelectFileOrDirectory = new Label( );
            lblOutput = new Label( );
            rbScreenOutput = new RadioButton( );
            rbFileOutput = new RadioButton( );
            gbInspect = new GroupBox( );
            gbOutput = new GroupBox( );
            lblOutputDestination = new Label( );
            tbOutput = new TextBox( );
            btnOutputBrowse = new Button( );
            btnExtract = new Button( );
            lblError = new Label( );
            gbInspect.SuspendLayout( );
            gbOutput.SuspendLayout( );
            SuspendLayout( );
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Location = new Point( 18, 86 );
            lblSource.Name = "lblSource";
            lblSource.Size = new Size( 97, 15 );
            lblSource.TabIndex = 0;
            lblSource.Text = "Source Directory:";
            // 
            // tbSource
            // 
            tbSource.Location = new Point( 121, 83 );
            tbSource.Name = "tbSource";
            tbSource.Size = new Size( 559, 23 );
            tbSource.TabIndex = 1;
            // 
            // dlgSelectSource
            // 
            dlgSelectSource.FileName = "openFileDialog1";
            // 
            // btnSource
            // 
            btnSource.Location = new Point( 696, 83 );
            btnSource.Name = "btnSource";
            btnSource.Size = new Size( 75, 23 );
            btnSource.TabIndex = 2;
            btnSource.Text = "Browse";
            btnSource.UseVisualStyleBackColor = true;
            btnSource.Click += BtnSource_Click;
            // 
            // rbDirectory
            // 
            rbDirectory.AutoSize = true;
            rbDirectory.Checked = true;
            rbDirectory.Location = new Point( 72, 22 );
            rbDirectory.Name = "rbDirectory";
            rbDirectory.Size = new Size( 87, 19 );
            rbDirectory.TabIndex = 3;
            rbDirectory.TabStop = true;
            rbDirectory.Text = "Directory or";
            rbDirectory.UseVisualStyleBackColor = true;
            rbDirectory.CheckedChanged += RbDirectory_CheckedChanged;
            // 
            // rbFile
            // 
            rbFile.AutoSize = true;
            rbFile.Location = new Point( 165, 22 );
            rbFile.Name = "rbFile";
            rbFile.Size = new Size( 86, 19 );
            rbFile.TabIndex = 4;
            rbFile.Text = "a single File";
            rbFile.UseVisualStyleBackColor = true;
            // 
            // lblSelectFileOrDirectory
            // 
            lblSelectFileOrDirectory.AutoSize = true;
            lblSelectFileOrDirectory.Location = new Point( 12, 24 );
            lblSelectFileOrDirectory.Name = "lblSelectFileOrDirectory";
            lblSelectFileOrDirectory.Size = new Size( 54, 15 );
            lblSelectFileOrDirectory.TabIndex = 5;
            lblSelectFileOrDirectory.Text = "Inspect a";
            // 
            // lblOutput
            // 
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point( 12, 18 );
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size( 59, 15 );
            lblOutput.TabIndex = 6;
            lblOutput.Text = "Output to";
            // 
            // rbScreenOutput
            // 
            rbScreenOutput.AutoSize = true;
            rbScreenOutput.Checked = true;
            rbScreenOutput.Location = new Point( 85, 18 );
            rbScreenOutput.Name = "rbScreenOutput";
            rbScreenOutput.Size = new Size( 97, 19 );
            rbScreenOutput.TabIndex = 7;
            rbScreenOutput.TabStop = true;
            rbScreenOutput.Text = "the Screen, or";
            rbScreenOutput.UseVisualStyleBackColor = true;
            rbScreenOutput.CheckedChanged += RbScreenOutput_CheckedChanged;
            // 
            // rbFileOutput
            // 
            rbFileOutput.AutoSize = true;
            rbFileOutput.Location = new Point( 202, 18 );
            rbFileOutput.Name = "rbFileOutput";
            rbFileOutput.Size = new Size( 52, 19 );
            rbFileOutput.TabIndex = 8;
            rbFileOutput.Text = "a File";
            rbFileOutput.UseVisualStyleBackColor = true;
            // 
            // gbInspect
            // 
            gbInspect.Controls.Add( rbDirectory );
            gbInspect.Controls.Add( rbFile );
            gbInspect.Controls.Add( lblSelectFileOrDirectory );
            gbInspect.Location = new Point( 7, 15 );
            gbInspect.Name = "gbInspect";
            gbInspect.Size = new Size( 263, 49 );
            gbInspect.TabIndex = 9;
            gbInspect.TabStop = false;
            // 
            // gbOutput
            // 
            gbOutput.Controls.Add( lblOutputDestination );
            gbOutput.Controls.Add( tbOutput );
            gbOutput.Controls.Add( btnOutputBrowse );
            gbOutput.Controls.Add( rbScreenOutput );
            gbOutput.Controls.Add( lblOutput );
            gbOutput.Controls.Add( rbFileOutput );
            gbOutput.Location = new Point( 7, 124 );
            gbOutput.Name = "gbOutput";
            gbOutput.Size = new Size( 688, 92 );
            gbOutput.TabIndex = 10;
            gbOutput.TabStop = false;
            // 
            // lblOutputDestination
            // 
            lblOutputDestination.AutoSize = true;
            lblOutputDestination.Location = new Point( 12, 62 );
            lblOutputDestination.Name = "lblOutputDestination";
            lblOutputDestination.Size = new Size( 89, 15 );
            lblOutputDestination.TabIndex = 11;
            lblOutputDestination.Text = "Save Output to:";
            lblOutputDestination.Visible = false;
            // 
            // tbOutput
            // 
            tbOutput.Location = new Point( 114, 59 );
            tbOutput.Name = "tbOutput";
            tbOutput.Size = new Size( 559, 23 );
            tbOutput.TabIndex = 10;
            tbOutput.Visible = false;
            // 
            // btnOutputBrowse
            // 
            btnOutputBrowse.Location = new Point( 269, 16 );
            btnOutputBrowse.Name = "btnOutputBrowse";
            btnOutputBrowse.Size = new Size( 75, 23 );
            btnOutputBrowse.TabIndex = 9;
            btnOutputBrowse.Text = "Browse";
            btnOutputBrowse.UseVisualStyleBackColor = true;
            btnOutputBrowse.Visible = false;
            btnOutputBrowse.Click += BtnOutputBrowse_Click;
            // 
            // btnExtract
            // 
            btnExtract.Location = new Point( 19, 244 );
            btnExtract.Name = "btnExtract";
            btnExtract.Size = new Size( 75, 23 );
            btnExtract.TabIndex = 11;
            btnExtract.Text = "Extract";
            btnExtract.UseVisualStyleBackColor = true;
            btnExtract.Click += BtnExtract_Click;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Location = new Point( 19, 290 );
            lblError.Name = "lblError";
            lblError.Size = new Size( 0, 15 );
            lblError.TabIndex = 12;
            // 
            // Assembly2MermaidForm
            // 
            AutoScaleDimensions = new SizeF( 7F, 15F );
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size( 800, 314 );
            Controls.Add( lblError );
            Controls.Add( btnExtract );
            Controls.Add( gbOutput );
            Controls.Add( gbInspect );
            Controls.Add( btnSource );
            Controls.Add( tbSource );
            Controls.Add( lblSource );
            Name = "Assembly2MermaidForm";
            Text = ".Net Assembly To Mermaid Diagram Extractor";
            gbInspect.ResumeLayout( false );
            gbInspect.PerformLayout( );
            gbOutput.ResumeLayout( false );
            gbOutput.PerformLayout( );
            ResumeLayout( false );
            PerformLayout( );
        }

        #endregion

        private Label lblSource;
        private TextBox tbSource;
        private OpenFileDialog dlgSelectSource;
        private Button btnSource;
        private RadioButton rbDirectory;
        private RadioButton rbFile;
        private Label lblSelectFileOrDirectory;
        private Label lblOutput;
        private RadioButton rbScreenOutput;
        private RadioButton rbFileOutput;
        private GroupBox gbInspect;
        private GroupBox gbOutput;
        private Label lblOutputDestination;
        private TextBox tbOutput;
        private Button btnOutputBrowse;
        private Button btnExtract;
        private Label lblError;
    }
}
