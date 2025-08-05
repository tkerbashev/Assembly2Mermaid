namespace Assembly2Mermaid
{
    public partial class FrmDiagram : Form
    {
        public FrmDiagram( string text )
        {
            InitializeComponent( );
            tbDiagram.Text = text;
            tbDiagram.Select(0, 0);
        }

        private void BtnClose_Click( object sender, EventArgs e )
        {
            Close( );
        }

        private void BtnCopy_Click( object sender, EventArgs e )
        {
            Clipboard.SetText( tbDiagram.Text );
        }
    }
}
