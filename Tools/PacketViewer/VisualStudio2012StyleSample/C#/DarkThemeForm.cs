using System;
using System.Drawing;
using System.Text;
using DevComponents.DotNetBar.Metro;
using System.Linq;
using Be.Windows.Forms;
using FastColoredTextBoxNS;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using VisualStudio2012Style.PacketsEnum;
using SharpPcap.LibPcap;

namespace VisualStudio2012Style
{
    public partial class DarkThemeForm : MetroForm
    {
        #region Log Styles

        TextStyle infoStyle = new TextStyle(Brushes.Black, null, FontStyle.Regular);
        TextStyle warningStyle = new TextStyle(Brushes.BurlyWood, null, FontStyle.Regular);
        TextStyle errorStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);

        #endregion

        #region Private Fields

        private DynamicByteProvider byteProviderDecrypted;
        private DynamicByteProvider byteProviderCrypted;

        #endregion

        private void menu_edit_copyBytes_Click(object sender, EventArgs e)
        {
            var frm = new FrmCopyFormater(hexDecrypted);
            frm.ShowDialog();
        }

        private void DarkThemeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}