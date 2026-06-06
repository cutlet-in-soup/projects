namespace Wafanda_calculator
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.distance_label = new System.Windows.Forms.Label();
            this.distance_map = new System.Windows.Forms.NumericUpDown();
            this.LPix = new System.Windows.Forms.Label();
            this.angleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distance_map)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 56);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1200, 1200);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // distance_label
            // 
            this.distance_label.AutoSize = true;
            this.distance_label.Location = new System.Drawing.Point(9, 20);
            this.distance_label.Name = "distance_label";
            this.distance_label.Size = new System.Drawing.Size(38, 13);
            this.distance_label.TabIndex = 1;
            this.distance_label.Text = "meters";
            // 
            // distance_map
            // 
            this.distance_map.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.distance_map.Location = new System.Drawing.Point(174, 20);
            this.distance_map.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.distance_map.Name = "distance_map";
            this.distance_map.Size = new System.Drawing.Size(120, 20);
            this.distance_map.TabIndex = 2;
            // 
            // LPix
            // 
            this.LPix.AutoSize = true;
            this.LPix.Location = new System.Drawing.Point(300, 27);
            this.LPix.Name = "LPix";
            this.LPix.Size = new System.Drawing.Size(18, 13);
            this.LPix.TabIndex = 3;
            this.LPix.Text = "px";
            // 
            // angleLabel
            // 
            this.angleLabel.AutoSize = true;
            this.angleLabel.Location = new System.Drawing.Point(12, 40);
            this.angleLabel.Name = "angleLabel";
            this.angleLabel.Size = new System.Drawing.Size(33, 13);
            this.angleLabel.TabIndex = 4;
            this.angleLabel.Text = "angle";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 1061);
            this.Controls.Add(this.angleLabel);
            this.Controls.Add(this.LPix);
            this.Controls.Add(this.distance_map);
            this.Controls.Add(this.distance_label);
            this.Controls.Add(this.pictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Wafanda Calculator v2.2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distance_map)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label distance_label;
        private System.Windows.Forms.NumericUpDown distance_map;
        private System.Windows.Forms.Label LPix;
        private System.Windows.Forms.Label angleLabel;
    }
}

