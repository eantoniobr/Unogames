namespace VisualStudio2012Style
{
    partial class FrmCopyFormater
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCopyFormater));
            this.txtCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.hex = new Be.Windows.Forms.HexBox();
            this.context_CopyMethods = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_ToCsharp = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_ToByteWithoutSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_ToByteWithSpace = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).BeginInit();
            this.context_CopyMethods.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.AutoCompleteBracketsList = new char[] {
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
            this.txtCode.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.txtCode.AutoScrollMinSize = new System.Drawing.Size(0, 60);
            this.txtCode.BackBrush = null;
            this.txtCode.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.txtCode.CharHeight = 15;
            this.txtCode.CharWidth = 7;
            this.txtCode.ContextMenuStrip = this.context_CopyMethods;
            this.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtCode.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.IsReplaceMode = false;
            this.txtCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtCode.LeftBracket = '(';
            this.txtCode.LeftBracket2 = '{';
            this.txtCode.Location = new System.Drawing.Point(518, 0);
            this.txtCode.Name = "txtCode";
            this.txtCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCode.RightBracket = ')';
            this.txtCode.RightBracket2 = '}';
            this.txtCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtCode.ServiceColors")));
            this.txtCode.Size = new System.Drawing.Size(457, 407);
            this.txtCode.TabIndex = 7;
            this.txtCode.Text = "   /// <summary>\r\n   /// Meu Editor Embutido ;)\r\n   /// </summary>\r\n   {code}";
            this.txtCode.WordWrap = true;
            this.txtCode.Zoom = 100;
            // 
            // hex
            // 
            // 
            // 
            // 
            this.hex.BuiltInContextMenu.CopyMenuItemText = "Copy";
            this.hex.BuiltInContextMenu.CutMenuItemText = "Cut";
            this.hex.BuiltInContextMenu.PasteMenuItemText = "Paste";
            this.hex.BuiltInContextMenu.SelectAllMenuItemText = "Select All";
            this.hex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hex.Font = new System.Drawing.Font("Consolas", 9F);
            this.hex.InfoForeColor = System.Drawing.Color.Empty;
            this.hex.Location = new System.Drawing.Point(0, 0);
            this.hex.Name = "hex";
            this.hex.ReadOnly = true;
            this.hex.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hex.Size = new System.Drawing.Size(518, 407);
            this.hex.StringViewVisible = true;
            this.hex.TabIndex = 22;
            this.hex.UseFixedBytesPerLine = true;
            this.hex.VScrollBarVisible = true;
            // 
            // context_CopyMethods
            // 
            this.context_CopyMethods.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_ToCsharp,
            this.btn_ToByteWithoutSpace,
            this.btn_ToByteWithSpace});
            this.context_CopyMethods.Name = "context_CopyMethods";
            this.context_CopyMethods.Size = new System.Drawing.Size(194, 92);
            // 
            // btn_ToCsharp
            // 
            this.btn_ToCsharp.Name = "btn_ToCsharp";
            this.btn_ToCsharp.Size = new System.Drawing.Size(193, 22);
            this.btn_ToCsharp.Text = "To C# Byte Array";
            this.btn_ToCsharp.Click += new System.EventHandler(this.btn_ToCsharp_Click);
            // 
            // btn_ToByteWithoutSpace
            // 
            this.btn_ToByteWithoutSpace.Name = "btn_ToByteWithoutSpace";
            this.btn_ToByteWithoutSpace.Size = new System.Drawing.Size(193, 22);
            this.btn_ToByteWithoutSpace.Text = "To Byte Without Space";
            this.btn_ToByteWithoutSpace.Click += new System.EventHandler(this.btn_ToByteWithoutSpace_Click);
            // 
            // btn_ToByteWithSpace
            // 
            this.btn_ToByteWithSpace.Name = "btn_ToByteWithSpace";
            this.btn_ToByteWithSpace.Size = new System.Drawing.Size(193, 22);
            this.btn_ToByteWithSpace.Text = "To Byte With Space";
            this.btn_ToByteWithSpace.Click += new System.EventHandler(this.btn_ToByteWithSpace_Click);
            // 
            // FrmCopyFormater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 407);
            this.Controls.Add(this.hex);
            this.Controls.Add(this.txtCode);
            this.Name = "FrmCopyFormater";
            this.Text = "FrmCopyFormater";
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).EndInit();
            this.context_CopyMethods.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox txtCode;
        private Be.Windows.Forms.HexBox hex;
        private System.Windows.Forms.ContextMenuStrip context_CopyMethods;
        private System.Windows.Forms.ToolStripMenuItem btn_ToCsharp;
        private System.Windows.Forms.ToolStripMenuItem btn_ToByteWithoutSpace;
        private System.Windows.Forms.ToolStripMenuItem btn_ToByteWithSpace;
    }
}