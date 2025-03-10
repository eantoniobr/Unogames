namespace VisualStudio2012Style
{
    partial class FrmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.txtCSharpCode = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtCSharpCode)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCSharpCode
            // 
            this.txtCSharpCode.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtCSharpCode.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.txtCSharpCode.AutoScrollMinSize = new System.Drawing.Size(0, 532);
            this.txtCSharpCode.BackBrush = null;
            this.txtCSharpCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtCSharpCode.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.txtCSharpCode.CharHeight = 19;
            this.txtCSharpCode.CharWidth = 9;
            this.txtCSharpCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCSharpCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCSharpCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCSharpCode.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtCSharpCode.ForeColor = System.Drawing.Color.Black;
            this.txtCSharpCode.IsReplaceMode = false;
            this.txtCSharpCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtCSharpCode.LeftBracket = '(';
            this.txtCSharpCode.LeftBracket2 = '{';
            this.txtCSharpCode.Location = new System.Drawing.Point(0, 0);
            this.txtCSharpCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCSharpCode.Name = "txtCSharpCode";
            this.txtCSharpCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCSharpCode.RightBracket = ')';
            this.txtCSharpCode.RightBracket2 = '}';
            this.txtCSharpCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtCSharpCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtCSharpCode.ServiceColors")));
            this.txtCSharpCode.Size = new System.Drawing.Size(760, 427);
            this.txtCSharpCode.TabIndex = 7;
            this.txtCSharpCode.Text = resources.GetString("txtCSharpCode.Text");
            this.txtCSharpCode.WordWrap = true;
            this.txtCSharpCode.Zoom = 100;
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 427);
            this.Controls.Add(this.txtCSharpCode);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmAbout";
            ((System.ComponentModel.ISupportInitialize)(this.txtCSharpCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox txtCSharpCode;
    }
}
