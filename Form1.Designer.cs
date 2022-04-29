namespace WinFormsApp1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AmountTextBox = new System.Windows.Forms.TextBox();
            this.amountLabel = new System.Windows.Forms.Label();
            this.ReadFromFileButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.X1 = new System.Windows.Forms.Label();
            this.Y1 = new System.Windows.Forms.Label();
            this.X2 = new System.Windows.Forms.Label();
            this.Y2 = new System.Windows.Forms.Label();
            this.EraseButton = new System.Windows.Forms.Button();
            this.labelA = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.labelC = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // label7
            // 
            resources.ApplyResources(this.scaleLabel, "label7");
            this.scaleLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.scaleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scaleLabel.Name = "label7";
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.trackBar1, "trackBar1");
            this.trackBar1.Maximum = 240;
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.TickFrequency = 5;
            this.trackBar1.Value = 50;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Name = "panel1";
            // 
            // AmountTextBox
            // 
            this.AmountTextBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(this.AmountTextBox, "AmountTextBox");
            this.AmountTextBox.Name = "AmountTextBox";
            this.AmountTextBox.TextChanged += new System.EventHandler(this.AmountTextBox_TextChanged);
            // 
            // amountLabel
            // 
            resources.ApplyResources(this.amountLabel, "amountLabel");
            this.amountLabel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.amountLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.amountLabel.Name = "amountLabel";
            // 
            // ReadFromFileButton
            // 
            this.ReadFromFileButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            resources.ApplyResources(this.ReadFromFileButton, "ReadFromFileButton");
            this.ReadFromFileButton.Name = "ReadFromFileButton";
            this.ReadFromFileButton.UseVisualStyleBackColor = false;
            this.ReadFromFileButton.Click += new System.EventHandler(this.ReadFromFileButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "example.txt";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            this.openFileDialog1.InitialDirectory = "C:\\Users\\Lenovo\\Desktop";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // X1
            // 
            this.X1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.X1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.X1, "X1");
            this.X1.Name = "X1";
            // 
            // Y1
            // 
            this.Y1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Y1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.Y1, "Y1");
            this.Y1.Name = "Y1";
            // 
            // X2
            // 
            this.X2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.X2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.X2, "X2");
            this.X2.Name = "X2";
            // 
            // Y2
            // 
            this.Y2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Y2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.Y2, "Y2");
            this.Y2.Name = "Y2";
            // 
            // EraseButton
            // 
            this.EraseButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            resources.ApplyResources(this.EraseButton, "EraseButton");
            this.EraseButton.Name = "EraseButton";
            this.EraseButton.UseVisualStyleBackColor = false;
            this.EraseButton.Click += new System.EventHandler(this.EraseButton_Click);
            // 
            // labelA
            // 
            this.labelA.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.labelA, "labelA");
            this.labelA.Name = "labelA";
            // 
            // labelB
            // 
            this.labelB.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.labelB, "labelB");
            this.labelB.Name = "labelB";
            // 
            // labelC
            // 
            this.labelC.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.labelC, "labelC");
            this.labelC.Name = "labelC";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Controls.Add(this.labelA);
            this.Controls.Add(this.labelB);
            this.Controls.Add(this.labelC);
            this.Controls.Add(this.EraseButton);
            this.Controls.Add(this.Y2);
            this.Controls.Add(this.X2);
            this.Controls.Add(this.Y1);
            this.Controls.Add(this.X1);
            this.Controls.Add(this.ReadFromFileButton);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.AmountTextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.scaleLabel);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        //private Button button1;
        private PictureBox pictureBox1;
        private Label scaleLabel;
        private TrackBar trackBar1;
        private Panel panel1;
        private TextBox AmountTextBox;
        private Label amountLabel;
        private Button ReadFromFileButton;
        private OpenFileDialog openFileDialog1;
        private Label X1;
        private Label Y1;
        private Label X2;
        private Label Y2;
        private Button EraseButton;
        private Label labelA;
        private Label labelB;
        private Label labelC;
    }
}