﻿namespace Lightning
{
    partial class LightningForm
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
            this.createLightBulbButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createLightBulbButton
            // 
            this.createLightBulbButton.Location = new System.Drawing.Point(12, 12);
            this.createLightBulbButton.Name = "createLightBulbButton";
            this.createLightBulbButton.Size = new System.Drawing.Size(140, 51);
            this.createLightBulbButton.TabIndex = 0;
            this.createLightBulbButton.Text = "Create Light Bulb";
            this.createLightBulbButton.UseVisualStyleBackColor = true;
            this.createLightBulbButton.Click += new System.EventHandler(this.createLightBulbButton_Click);
            // 
            // LightningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 149);
            this.Controls.Add(this.createLightBulbButton);
            this.Name = "LightningForm";
            this.Text = "LightningForm";
            this.Load += new System.EventHandler(this.LightningForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createLightBulbButton;
    }
}

