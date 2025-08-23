using System.Reflection;
using System.Text;

namespace KeyboardGameV2
{
    public partial class frmDictTools : Form
    {

        private List<string> baseWords = [];
        private List<string> excludeWords = [];

        private readonly frmGame parremt;

        private readonly string outputPath;

        public frmDictTools(frmGame parrent)
        {
            parremt = parrent;
            InitializeComponent();
            outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new Exception("problem with working path");

        }

        private void Click_mnuBackToGame(object sender, EventArgs e)
        {
            parremt.Show(); this.Close();
        }

        private void FormClosed_frmDictTools(object sender, FormClosedEventArgs e)
        {
            parremt.Show();
        }

        private void CheckedChanged_chkExclude(object sender, EventArgs e)
        {
            btnExclude.Enabled = chkExclude.Checked;
            lblExclude.Text = "";
        }

        private void Click_btnLoad(object sender, EventArgs e)
        {
            OpenFile(ref baseWords, lblLoad);
            btnOutput.Enabled = !lblLoad.Text.Equals("");
        }

        private void Click_btnExclude(object sender, EventArgs e)
        {
            OpenFile(ref excludeWords, lblExclude);
        }

        private static void OpenFile(ref List<string> words, Label l)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = frmGame.POPMSG_FILE_FILTER;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                l.Text = openFileDialog.FileName;
                StreamReader sr = new(openFileDialog.OpenFile());
                string? wordFromFile = sr.ReadLine();
                while (wordFromFile != null)
                {
                    words.Add(wordFromFile);
                    wordFromFile = sr.ReadLine();
                }
                sr.Close();
                words.Sort();
            }
        }

        private void Click_btnOutput(object sender, EventArgs e)
        {
            EnglishDictionary dictionary;
            if (lblLoad.Text.Equals("") ||
                txtOutput.Text.Equals("") ||
                (chkExclude.Checked && lblExclude.Text.Equals("")))
            {
                //bad
                MessageBox.Show("dictionary opperation failed");
                return;
            }
            else
            {
                //good
                if (chkExclude.Checked)
                {
                    List<string> filteredWords = [];
                    uint excludeIndex = 0;
                    for (uint baseIndex = 0; baseIndex < baseWords.Count; baseIndex++)
                    {
                        int compare = baseWords[(int)baseIndex].CompareTo(
                            excludeWords[(int)excludeIndex]);
                        while (compare > 0 && excludeIndex < excludeWords.Count - 1)
                        {
                            excludeIndex++;
                            compare = baseWords[(int)baseIndex].CompareTo(
                                excludeWords[(int)excludeIndex]);
                        }
                        if (compare != 0) filteredWords.Add(baseWords[(int)baseIndex]);
                    }
                    dictionary = new EnglishDictionary(filteredWords);
                }
                else
                {
                    dictionary = new EnglishDictionary(baseWords);
                }
            }
            BinaryWriter writer = new(new FileStream(
                outputPath + "\\" + txtOutput.Text + ".trie", FileMode.Create));
            dictionary.Write(writer);
            writer.Close();
            MessageBox.Show("success");
        }
    }
}
