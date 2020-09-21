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
            OpenFile();
        }

        private void OpenFile()
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
            undoToolStripMenuItem.Enabled = true;
            toolStripButton10.Enabled = true;
            toolStripButton11.Enabled = false;
        }

        /// <summary>
        /// New function with exit dialog and switch case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NewFileMenu();
        }

        private void NewFileMenu()
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

        /// <summary>
        /// Undo tool strip menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoEditMenu();
        }

        private void UndoEditMenu()
        {
            MainRichTextBox.Undo();
            redoToolStripMenuItem.Enabled = true;
            toolStripButton11.Enabled = true;
            toolStripButton10.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Redo tool strip menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RedoEditMenu();
        }

        private void RedoEditMenu()
        {
            MainRichTextBox.Redo();
            redoToolStripMenuItem.Enabled = false;
            toolStripButton11.Enabled = false;
            toolStripButton10.Enabled = true;
            undoToolStripMenuItem.Enabled = true;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectAll();
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new System.Drawing.Font(MainRichTextBox.Font, System.Drawing.FontStyle.Bold);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new System.Drawing.Font(MainRichTextBox.Font, System.Drawing.FontStyle.Italic);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new System.Drawing.Font(MainRichTextBox.Font, System.Drawing.FontStyle.Underline);
        }

        private void strikethroughToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new System.Drawing.Font(MainRichTextBox.Font, System.Drawing.FontStyle.Strikeout);
        }

        private void formatFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog()
            {
                ShowColor = true,
                ShowApply = true
            };

            fontDialog.ShowColor = true;

            DialogResult result = fontDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                if (MainRichTextBox.SelectionLength > 0)
                MainRichTextBox.SelectionFont = fontDialog.Font;
                MainRichTextBox.SelectionColor = fontDialog.Color;
            }
        }

        private void changeTextColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            DialogResult result = colorDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (MainRichTextBox.SelectionLength > 0)
                    MainRichTextBox.SelectionColor = colorDialog.Color;
            }
        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFileMenu();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileMenu();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SaveFileMenu();
        }

        private void MainMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            UndoEditMenu();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            RedoEditMenu();
        }
    }
}

