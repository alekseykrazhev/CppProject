using System.ComponentModel;

namespace Events
{
    partial class shooting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.Target = new System.Windows.Forms.PictureBox();
            this.Stop_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.Target)).BeginInit();
            this.SuspendLayout();
            // 
            // Target
            // 
            this.Target.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.Target.Location = new System.Drawing.Point(12, 12);
            this.Target.Name = "Target";
            this.Target.Size = new System.Drawing.Size(587, 537);
            this.Target.TabIndex = 0;
            this.Target.TabStop = false;
            // 
            // Stop_button
            // 
            this.Stop_button.Location = new System.Drawing.Point(676, 437);
            this.Stop_button.Name = "Stop_button";
            this.Stop_button.Size = new System.Drawing.Size(196, 59);
            this.Stop_button.TabIndex = 1;
            this.Stop_button.Text = "Stop shooting";
            this.Stop_button.UseVisualStyleBackColor = true;
            this.Stop_button.Click += new System.EventHandler(this.Stop_button_Click_1);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(676, 314);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "-";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(839, 314);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "-";
            // 
            // shooting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 561);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Stop_button);
            this.Controls.Add(this.Target);
            this.Name = "shooting";
            this.Text = "shooting";
            ((System.ComponentModel.ISupportInitialize) (this.Target)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Button Stop_button;

        private System.Windows.Forms.PictureBox Target;

        #endregion
    }
}