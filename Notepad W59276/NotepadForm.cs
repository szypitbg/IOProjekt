using System;
using System.IO;
using System.Windows.Forms;

namespace Notepad_W59276
{
    public partial class NotepadForm : Form
    {
        #region fields
        private bool isFileAlreadySaved;
        private bool isFileDirty;
        private string currOpenFileName;
        #endregion
        public NotepadForm()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
   
        }
        /// <summary>
        /// Exit application code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Open file menu code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt) |*.txt|Rich Text Format (*.rtf)|*.rtf";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName) == ".txt")
                    MainRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(openFileDialog.FileName) == ".rtf")
                    MainRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
            }
            this.Text = Path.GetFileName(openFileDialog.FileName) + " - Notepad W59276";

            isFileAlreadySaved = true;
            isFileDirty = false;
            currOpenFileName = openFileDialog.FileName;
        }
        /// <summary>
        /// Save file menu code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileMenu();
        }
        /// <summary>
        /// Implementation of SaveFileMenu funkction
        /// </summary>
        private void SaveFileMenu()
        {
            if (isFileAlreadySaved)
            {
                if (Path.GetExtension(currOpenFileName) == ".txt")
                    MainRichTextBox.SaveFile(currOpenFileName, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(currOpenFileName) == ".rtf")
                    MainRichTextBox.SaveFile(currOpenFileName, RichTextBoxStreamType.RichText);
                isFileDirty = false;
            }
            else
            {
                if (isFileDirty)
                {
                    SaveAsFileMenu();
                }

                ClearScreen();
                isFileAlreadySaved = false;
                currOpenFileName = "";
            }
        }
        /// <summary>
        /// Clear the screen and default the variable
        /// </summary>
        private void ClearScreen()
        {
            MainRichTextBox.Clear();
            this.Text = "Untitled - Notepad W59276";
            isFileDirty = false;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFileMenu();
        }
        /// <summary>
        /// SaveAsFileMenu Function Code
        /// </summary>
        private void SaveAsFileMenu()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt) |*.txt|Rich Text Format (*.rtf)|*.rtf";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".txt")
                    MainRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(saveFileDialog.FileName) == ".rtf")
                    MainRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
            }
            this.Text = Path.GetFileName(saveFileDialog.FileName) + " - Notepad W59276";

            isFileAlreadySaved = true;
            isFileDirty = false;
            currOpenFileName = saveFileDialog.FileName;
        }

        private void NotepadForm_Load(object sender, EventArgs e)
        {
            isFileAlreadySaved = false;
            isFileDirty = false;
            currOpenFileName = "";
        }
        /// <summary>
        /// Text box, text changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            isFileDirty = true;
        }

        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (isFileDirty)
            {
                DialogResult result = MessageBox.Show("Czy Chcesz zapisać zmiany?", "Zapisz...",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFileMenu();
                        ClearScreen();
                        break;
                    case DialogResult.No:
                        ClearScreen();
                        break;
                }
            }
                ClearScreen();
                isFileAlreadySaved = false;
        }
    }
}

