
namespace ImageDownsizer
{
    partial class ImageDownsizer : Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SizeF AutoScaleDimensions { get; private set; }

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
            LoadImageBtn = new Button();
            SaveImageBtn = new Button();
            originalPictureBox = new PictureBox();
            downscaledPictureBox = new PictureBox();
            downscaleF = new TextBox();
            label2 = new Label();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            parallelCheck = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)downscaledPictureBox).BeginInit();
            SuspendLayout();
            // 
            // LoadImageBtn
            // 
            LoadImageBtn.BackColor = Color.Plum;
            LoadImageBtn.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LoadImageBtn.Location = new Point(28, 25);
            LoadImageBtn.Name = "LoadImageBtn";
            LoadImageBtn.Size = new Size(176, 29);
            LoadImageBtn.TabIndex = 0;
            LoadImageBtn.Text = "Open Image";
            LoadImageBtn.UseVisualStyleBackColor = false;
            LoadImageBtn.Click += openImageButton_Click;
            // 
            // SaveImageBtn
            // 
            SaveImageBtn.BackColor = Color.Plum;
            SaveImageBtn.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            SaveImageBtn.Location = new Point(28, 118);
            SaveImageBtn.Name = "SaveImageBtn";
            SaveImageBtn.Size = new Size(176, 29);
            SaveImageBtn.TabIndex = 1;
            SaveImageBtn.Text = "Save ";
            SaveImageBtn.UseVisualStyleBackColor = false;
            SaveImageBtn.Click += saveImageButton_Click;
            // 
            // originalPictureBox
            // 
            originalPictureBox.Location = new Point(242, 25);
            originalPictureBox.Name = "originalPictureBox";
            originalPictureBox.Size = new Size(395, 256);
            originalPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            originalPictureBox.TabIndex = 3;
            originalPictureBox.TabStop = false;
            // 
            // downscaledPictureBox
            // 
            downscaledPictureBox.Location = new Point(670, 25);
            downscaledPictureBox.Name = "downscaledPictureBox";
            downscaledPictureBox.Size = new Size(430, 256);
            downscaledPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            downscaledPictureBox.TabIndex = 4;
            downscaledPictureBox.TabStop = false;
            // 
            // downscaleF
            // 
            downscaleF.Location = new Point(125, 55);
            downscaleF.Name = "downscaleF";
            downscaleF.Size = new Size(79, 27);
            downscaleF.TabIndex = 5;
            downscaleF.TextChanged += textBox2_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.ForeColor = Color.Violet;
            label2.Location = new Point(242, 284);
            label2.Name = "label2";
            label2.Size = new Size(55, 16);
            label2.TabIndex = 7;
            label2.Text = "Original";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label4.ForeColor = Color.Plum;
            label4.Location = new Point(670, 284);
            label4.Name = "label4";
            label4.Size = new Size(100, 16);
            label4.TabIndex = 9;
            label4.Text = "Reduced scale";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(177, 57);
            label3.Name = "label3";
            label3.Size = new Size(27, 25);
            label3.TabIndex = 10;
            label3.Text = "%";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label5.ForeColor = Color.Orchid;
            label5.Location = new Point(28, 59);
            label5.Name = "label5";
            label5.Size = new Size(89, 18);
            label5.TabIndex = 11;
            label5.Text = "Downscale";
            // 
            // parallelCheck
            // 
            parallelCheck.AutoSize = true;
            parallelCheck.Location = new Point(123, 88);
            parallelCheck.Name = "parallelCheck";
            parallelCheck.Size = new Size(81, 24);
            parallelCheck.TabIndex = 12;
            parallelCheck.Text = "parallel";
            parallelCheck.UseVisualStyleBackColor = true;
            parallelCheck.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // ImageDownsizer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.GhostWhite;
            ClientSize = new Size(1129, 301);
            Controls.Add(parallelCheck);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(downscaleF);
            Controls.Add(downscaledPictureBox);
            Controls.Add(originalPictureBox);
            Controls.Add(SaveImageBtn);
            Controls.Add(LoadImageBtn);
            Name = "ImageDownsizer";
            Text = "ImageDownsizer";
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)downscaledPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private Button LoadImageBtn;
        private Button SaveImageBtn;
        private PictureBox originalPictureBox;
        private PictureBox downscaledPictureBox;
        private TextBox downscaleF;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label label5;
        private CheckBox parallelCheck;
      
    }
}
