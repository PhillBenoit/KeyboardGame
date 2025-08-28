using System.Reflection;
using System.Text;

namespace KeyboardGameV2
{
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE1006 // Naming Styles
    public partial class frmDictTools : Form
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore IDE0079 // Remove unnecessary suppression
    {

        //loaded word lists from files
        private List<string> baseWords = [];
        private List<string> excludeWords = [];

        //sending form
        private readonly frmGame parremt;

        //where to write files
        private readonly string outputPath;

        public frmDictTools(frmGame parrent)
        {
            parremt = parrent;
            InitializeComponent();
            //output same directory as executable
            outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new Exception("problem with working path");

        }

        //return to parent
        private void Click_mnuBackToGame(object sender, EventArgs e)
        {
            parremt.Show(); this.Close();
        }

        //other return to parent
        private void FormClosed_frmDictTools(object sender, FormClosedEventArgs e)
        {
            parremt.Show();
        }

        //enables exclude file filtering
        private void CheckedChanged_chkExclude(object sender, EventArgs e)
        {
            btnExclude.Enabled = chkExclude.Checked;
            lblExclude.Text = "";
        }

        //loads the primary word list
        private void Click_btnLoad(object sender, EventArgs e)
        {
            OpenFile(ref baseWords, lblLoad);
            btnOutput.Enabled = !lblLoad.Text.Equals("");
        }

        //loads the exclude list
        private void Click_btnExclude(object sender, EventArgs e)
        {
            OpenFile(ref excludeWords, lblExclude);
        }

        //generic file loader
        private static void OpenFile(ref List<string> words, Label l)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = frmGame.POPMSG_FILE_FILTER;
            openFileDialog.RestoreDirectory = true;
            words.Clear();

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

        //write instructions
        private void Click_btnOutput(object sender, EventArgs e)
        {
            SpellingDictionary dictionary;
            
            //make sure there is data to process
            if (lblLoad.Text.Equals("") ||
                txtOutput.Text.Equals("") ||
                (chkExclude.Checked && lblExclude.Text.Equals("")))
            {
                MessageBox.Show("dictionary opperation failed");
                return;
            }
            else
            {
                CharEncoding.Language language = chkEnye.Checked ?
                    CharEncoding.Languages.ES :
                    CharEncoding.Languages.EN;
                
                //using the exclude list
                if (chkExclude.Checked)
                {
                    List<string> filteredWords = [];
                    uint excludeIndex = 0;
                    
                    //run through the base list
                    for (uint baseIndex = 0; baseIndex < baseWords.Count; baseIndex++)
                    {
                        int compare = baseWords[(int)baseIndex].CompareTo(
                            excludeWords[(int)excludeIndex]);
                        
                        //make sure the filtered list is always ahead of the base list
                        while (compare > 0 && excludeIndex < excludeWords.Count - 1)
                        {
                            excludeIndex++;
                            compare = baseWords[(int)baseIndex].CompareTo(
                                excludeWords[(int)excludeIndex]);
                        }
                        
                        //check for equality to filter
                        if (compare != 0) filteredWords.Add(baseWords[(int)baseIndex]);
                    }
                    
                    //process the final list
                    dictionary = new SpellingDictionary(filteredWords, language);
                }
                else
                {
                    //process just the base list
                    dictionary = new SpellingDictionary(baseWords, language);
                }
            }
            
            //write
            BinaryWriter writer = new(new FileStream(
                outputPath + "\\" + txtOutput.Text + ".trie", FileMode.Create));
            dictionary.Write(writer);
            writer.Close();
            MessageBox.Show("success");
        }
    }
}
