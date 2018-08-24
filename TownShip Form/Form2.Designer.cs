namespace TownShip_Form
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EmptyHaveAlready = new System.Windows.Forms.Button();
            this.updateAlreadyHave = new System.Windows.Forms.Button();
            this.calcButton = new System.Windows.Forms.Button();
            this.LoadZakazButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EmptyHaveAlready
            // 
            this.EmptyHaveAlready.Location = new System.Drawing.Point(12, 12);
            this.EmptyHaveAlready.Name = "EmptyHaveAlready";
            this.EmptyHaveAlready.Size = new System.Drawing.Size(127, 23);
            this.EmptyHaveAlready.TabIndex = 0;
            this.EmptyHaveAlready.Text = "EmptyHaveAlready";
            this.EmptyHaveAlready.UseVisualStyleBackColor = true;
            this.EmptyHaveAlready.Click += new System.EventHandler(this.EmptyHaveAlready_Click);
            // 
            // updateAlreadyHave
            // 
            this.updateAlreadyHave.Location = new System.Drawing.Point(12, 41);
            this.updateAlreadyHave.Name = "updateAlreadyHave";
            this.updateAlreadyHave.Size = new System.Drawing.Size(127, 23);
            this.updateAlreadyHave.TabIndex = 1;
            this.updateAlreadyHave.Text = "UpdateAlreadyHave";
            this.updateAlreadyHave.UseVisualStyleBackColor = true;
            this.updateAlreadyHave.Click += new System.EventHandler(this.updateAlreadyHave_Click);
            // 
            // calcButton
            // 
            this.calcButton.Location = new System.Drawing.Point(12, 103);
            this.calcButton.Name = "calcButton";
            this.calcButton.Size = new System.Drawing.Size(126, 23);
            this.calcButton.TabIndex = 2;
            this.calcButton.Text = "Calc";
            this.calcButton.UseVisualStyleBackColor = true;
            this.calcButton.Click += new System.EventHandler(this.calcButton_Click);
            // 
            // LoadZakazButton
            // 
            this.LoadZakazButton.Location = new System.Drawing.Point(13, 71);
            this.LoadZakazButton.Name = "LoadZakazButton";
            this.LoadZakazButton.Size = new System.Drawing.Size(126, 23);
            this.LoadZakazButton.TabIndex = 3;
            this.LoadZakazButton.Text = "LoadZakaz";
            this.LoadZakazButton.UseVisualStyleBackColor = true;
            this.LoadZakazButton.Click += new System.EventHandler(this.LoadZakazButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(186, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LoadZakazButton);
            this.Controls.Add(this.calcButton);
            this.Controls.Add(this.updateAlreadyHave);
            this.Controls.Add(this.EmptyHaveAlready);
            this.Name = "Form2";
            this.Text = "Form2";
            this.DoubleClick += new System.EventHandler(this.Form2_DoubleClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EmptyHaveAlready;
        private System.Windows.Forms.Button updateAlreadyHave;
        private System.Windows.Forms.Button calcButton;
        private System.Windows.Forms.Button LoadZakazButton;
        private System.Windows.Forms.Button button1;
    }
}