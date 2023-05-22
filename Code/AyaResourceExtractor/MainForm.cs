
namespace AYAResourceExtractor
{
    public partial class MainForm : Form
    {
        bool stopExtracting = false;
        string outputFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Battle Engine Aquila Models";
        readonly AyaModelExtractor modelEextator = new();

        public MainForm()
        {
            InitializeComponent();
            outputModelsFolderTextBox.Text = outputFolder;
            Log.Instance.TextBox = logTextBox;
        }

        private void ExtractFile(string file)
        {
            bool folderExists = Directory.Exists(outputFolder);
            if (!folderExists)
            {
                Directory.CreateDirectory(outputFolder);
            }

            string resourcesPath = resourcesTextBox.Text;
            if (resourcesPath == "Not set")
            {
                MessageBox.Show("Battle Engine aquila root path not set");
                return;
            }

            resourcesPath += @"\data\resources";
            modelEextator.ExtractModel(resourcesPath, file, outputModelsFolderTextBox.Text, createBinaryFbxCheckBox.Checked, createAsciiFbxCheckBox.Checked);
        }

        private void ExtractIndividualAyaFileButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openIndividualAyaFileFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = openIndividualAyaFileFileDialog.FileName;

                // set resources if not set
                if (resourcesTextBox.Text == "Not set")
                {
                    string path = Path.GetFullPath(file);
                    path.ToLower();
                    int index = path.IndexOf("data");
                    if (index > 0)
                    {
                        path = path.Substring(0, index);
                        resourcesTextBox.Text = path;
                    }
                }

                ExtractFile(file);
            }
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            Log.Instance.Clear();
        }

        private void ExtractAllButton_Click(object sender, EventArgs e)
        {
            stopExtractingButton.Enabled = true;
            extractAllButton.Enabled = false;
            extractIndividualAyaFileButton.Enabled = false;

            Thread t = new(new ThreadStart(ExtractAll));
            t.Start();
        }

        private void EnableExtractionButtons()
        {
            extractAllButton.Enabled = true;
            extractIndividualAyaFileButton.Enabled = true;
            stopExtractingButton.Enabled = false;
        }

        private void ExtractAll()
        {
            stopExtracting = false;

            try
            {
                string rootpath = resourcesTextBox.Text;
                if (rootpath == "Not set")
                {
                    MessageBox.Show("Battle Engine aquila root path not set");
                    return;
                }

                string resourcespath = rootpath + @"\data\resources";
                string meshesPath = rootpath + @"\data\resources\meshes";

                string[] files = Directory.GetFiles(meshesPath);

                foreach (string file in files)
                {
                    if (this.stopExtracting == true)
                    {
                        break;
                    }
                    ExtractFile(file);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                if (e.StackTrace != null)
                {
                    Log.Error(e.StackTrace);
                }
            }
            finally
            {
                Invoke(new Action(EnableExtractionButtons));
            }  
        }

        private void SetRootPathButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                resourcesTextBox.Text = fbd.SelectedPath;
            }
        }

        private void SetOutputPathButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                outputModelsFolderTextBox.Text = fbd.SelectedPath;
            }
        }

        private void StopExtractingButton_Click(object sender, EventArgs e)
        {
            stopExtracting = true;
        }
    }
}