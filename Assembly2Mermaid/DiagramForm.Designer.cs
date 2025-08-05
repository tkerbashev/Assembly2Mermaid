namespace Assembly2Mermaid
{
    partial class FrmDiagram
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            tbDiagram = new TextBox( );
            btnClose = new Button( );
            btnCopy = new Button( );
            SuspendLayout( );
            // 
            // tbDiagram
            // 
            tbDiagram.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbDiagram.Location = new Point( 12, 12 );
            tbDiagram.Multiline = true;
            tbDiagram.Name = "tbDiagram";
            tbDiagram.ReadOnly = true;
            tbDiagram.ScrollBars = ScrollBars.Vertical;
            tbDiagram.Size = new Size( 694, 426 );
            tbDiagram.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point( 713, 415 );
            btnClose.Name = "btnClose";
            btnClose.Size = new Size( 75, 23 );
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            // 
            // btnCopy
            // 
            btnCopy.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCopy.Location = new Point( 713, 366 );
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size( 75, 23 );
            btnCopy.TabIndex = 2;
            btnCopy.Text = "Copy";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += BtnCopy_Click;
            // 
            // frmDiagram
            // 
            AutoScaleDimensions = new SizeF( 7F, 15F );
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size( 800, 450 );
            Controls.Add( btnCopy );
            Controls.Add( btnClose );
            Controls.Add( tbDiagram );
            MinimizeBox = false;
            MinimumSize = new Size( 400, 400 );
            Name = "frmDiagram";
            Text = "Mermaid Diagram";
            TopMost = true;
            ResumeLayout( false );
            PerformLayout( );
        }

        #endregion

        private TextBox tbDiagram;
        private Button btnClose;
        private Button btnCopy;
    }
}