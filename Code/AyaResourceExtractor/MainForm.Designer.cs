namespace AYAResourceExtractor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            outputModelsFolderTextBox = new TextBox();
            setOutputPathButton = new Button();
            label3 = new Label();
            extractAllButton = new Button();
            extractIndividualAyaFileButton = new Button();
            label1 = new Label();
            resourcesTextBox = new TextBox();
            setRootPathButton = new Button();
            openIndividualAyaFileFileDialog = new OpenFileDialog();
            label4 = new Label();
            createBinaryFbxCheckBox = new CheckBox();
            createAsciiFbxCheckBox = new CheckBox();
            logTextBox = new TextBox();
            clearLogButton = new Button();
            stopExtractingButton = new Button();
            versionLabel = new Label();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 62);
            label2.Name = "label2";
            label2.Size = new Size(156, 15);
            label2.TabIndex = 6;
            label2.Text = "Output FBX models location";
            // 
            // outputModelsFolderTextBox
            // 
            outputModelsFolderTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            outputModelsFolderTextBox.Location = new Point(12, 80);
            outputModelsFolderTextBox.Name = "outputModelsFolderTextBox";
            outputModelsFolderTextBox.Size = new Size(468, 23);
            outputModelsFolderTextBox.TabIndex = 5;
            outputModelsFolderTextBox.Text = "Not set";
            // 
            // setOutputPathButton
            // 
            setOutputPathButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            setOutputPathButton.Location = new Point(486, 79);
            setOutputPathButton.Name = "setOutputPathButton";
            setOutputPathButton.Size = new Size(34, 23);
            setOutputPathButton.TabIndex = 4;
            setOutputPathButton.Text = "...";
            setOutputPathButton.UseVisualStyleBackColor = true;
            setOutputPathButton.Click += SetOutputPathButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(415, 30);
            label3.TabIndex = 7;
            label3.Text = "Battle Engine Aquila resource extractor tool";
            // 
            // extractAllButton
            // 
            extractAllButton.Location = new Point(12, 216);
            extractAllButton.Name = "extractAllButton";
            extractAllButton.Size = new Size(85, 23);
            extractAllButton.TabIndex = 8;
            extractAllButton.Text = "Extract all";
            extractAllButton.UseVisualStyleBackColor = true;
            extractAllButton.Click += ExtractAllButton_Click;
            // 
            // extractIndividualAyaFileButton
            // 
            extractIndividualAyaFileButton.Location = new Point(12, 277);
            extractIndividualAyaFileButton.Name = "extractIndividualAyaFileButton";
            extractIndividualAyaFileButton.Size = new Size(216, 23);
            extractIndividualAyaFileButton.TabIndex = 9;
            extractIndividualAyaFileButton.Text = "Extract individual model from aya file";
            extractIndividualAyaFileButton.UseVisualStyleBackColor = true;
            extractIndividualAyaFileButton.Click += ExtractIndividualAyaFileButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 169);
            label1.Name = "label1";
            label1.Size = new Size(199, 15);
            label1.TabIndex = 12;
            label1.Text = "'Battle Engine Aquila' folder location";
            // 
            // resourcesTextBox
            // 
            resourcesTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            resourcesTextBox.Location = new Point(12, 187);
            resourcesTextBox.Name = "resourcesTextBox";
            resourcesTextBox.Size = new Size(468, 23);
            resourcesTextBox.TabIndex = 11;
            resourcesTextBox.Text = "Not set";
            // 
            // setRootPathButton
            // 
            setRootPathButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            setRootPathButton.Location = new Point(486, 186);
            setRootPathButton.Name = "setRootPathButton";
            setRootPathButton.Size = new Size(34, 23);
            setRootPathButton.TabIndex = 10;
            setRootPathButton.Text = "...";
            setRootPathButton.UseVisualStyleBackColor = true;
            setRootPathButton.Click += SetRootPathButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 126);
            label4.Name = "label4";
            label4.Size = new Size(94, 15);
            label4.TabIndex = 13;
            label4.Text = "Output FBX type";
            // 
            // createBinaryFbxCheckBox
            // 
            createBinaryFbxCheckBox.AutoSize = true;
            createBinaryFbxCheckBox.Checked = true;
            createBinaryFbxCheckBox.CheckState = CheckState.Checked;
            createBinaryFbxCheckBox.Location = new Point(112, 125);
            createBinaryFbxCheckBox.Name = "createBinaryFbxCheckBox";
            createBinaryFbxCheckBox.Size = new Size(59, 19);
            createBinaryFbxCheckBox.TabIndex = 14;
            createBinaryFbxCheckBox.Text = "Binary";
            createBinaryFbxCheckBox.UseVisualStyleBackColor = true;
            // 
            // createAsciiFbxCheckBox
            // 
            createAsciiFbxCheckBox.AutoSize = true;
            createAsciiFbxCheckBox.Checked = true;
            createAsciiFbxCheckBox.CheckState = CheckState.Checked;
            createAsciiFbxCheckBox.Location = new Point(177, 125);
            createAsciiFbxCheckBox.Name = "createAsciiFbxCheckBox";
            createAsciiFbxCheckBox.Size = new Size(51, 19);
            createAsciiFbxCheckBox.TabIndex = 15;
            createAsciiFbxCheckBox.Text = "Ascii";
            createAsciiFbxCheckBox.UseVisualStyleBackColor = true;
            // 
            // logTextBox
            // 
            logTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logTextBox.Location = new Point(12, 337);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.ScrollBars = ScrollBars.Vertical;
            logTextBox.Size = new Size(508, 179);
            logTextBox.TabIndex = 16;
            // 
            // clearLogButton
            // 
            clearLogButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            clearLogButton.Location = new Point(12, 534);
            clearLogButton.Name = "clearLogButton";
            clearLogButton.Size = new Size(75, 23);
            clearLogButton.TabIndex = 17;
            clearLogButton.Text = "Clear log";
            clearLogButton.UseVisualStyleBackColor = true;
            clearLogButton.Click += ClearLogButton_Click;
            // 
            // stopExtractingButton
            // 
            stopExtractingButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            stopExtractingButton.Enabled = false;
            stopExtractingButton.Location = new Point(93, 534);
            stopExtractingButton.Name = "stopExtractingButton";
            stopExtractingButton.Size = new Size(122, 23);
            stopExtractingButton.TabIndex = 18;
            stopExtractingButton.Text = "Stop extracting";
            stopExtractingButton.UseVisualStyleBackColor = true;
            stopExtractingButton.Click += StopExtractingButton_Click;
            // 
            // versionLabel
            // 
            versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            versionLabel.AutoSize = true;
            versionLabel.ImageAlign = ContentAlignment.TopRight;
            versionLabel.Location = new Point(474, 539);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(29, 15);
            versionLabel.TabIndex = 19;
            versionLabel.Text = "V1.0";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 579);
            Controls.Add(versionLabel);
            Controls.Add(stopExtractingButton);
            Controls.Add(clearLogButton);
            Controls.Add(logTextBox);
            Controls.Add(createAsciiFbxCheckBox);
            Controls.Add(createBinaryFbxCheckBox);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(resourcesTextBox);
            Controls.Add(setRootPathButton);
            Controls.Add(extractIndividualAyaFileButton);
            Controls.Add(extractAllButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(outputModelsFolderTextBox);
            Controls.Add(setOutputPathButton);
            Name = "MainForm";
            Text = "BAE resource extractor tool";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private TextBox outputModelsFolderTextBox;
        private Button setOutputPathButton;
        private Label label3;
        private Button extractAllButton;
        private Button extractIndividualAyaFileButton;
        private Label label1;
        private TextBox resourcesTextBox;
        private Button setRootPathButton;
        private OpenFileDialog openIndividualAyaFileFileDialog;
        private Label label4;
        private CheckBox createBinaryFbxCheckBox;
        private CheckBox createAsciiFbxCheckBox;
        private TextBox logTextBox;
        private Button clearLogButton;
        private Button stopExtractingButton;
        private Label versionLabel;
    }
}