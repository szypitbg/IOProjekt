using System;
using System.IO;
using System.Windows.Forms;

namespace Notepad_W59276
{
    public partial class NotepadForm : Form
    {

        private bool isFileAlreadySaved;
        private bool isFileDirty;
        private string currOpenFileName;
        public NotepadForm()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
   
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileMenu();
        }

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
                else
                {
                    ClearScreen();
                }
            }
        }

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

        private void SaveAsFileMenu()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt) |*.txt|Rich Text Format (*.rtf)|*.rtf";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".txt")
                    MainRichTextBox.LoadFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(saveFileDialog.FileName) == ".rtf")
                    MainRichTextBox.LoadFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
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

