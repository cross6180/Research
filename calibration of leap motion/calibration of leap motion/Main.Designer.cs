﻿namespace calibration_of_leap_motion
{
    partial class Main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.picboxLeapLeft = new System.Windows.Forms.PictureBox();
            this.labLeapLeft = new System.Windows.Forms.Label();
            this.labLeapRight = new System.Windows.Forms.Label();
            this.picboxLeapRight = new System.Windows.Forms.PictureBox();
            this.txtLeapFPS = new System.Windows.Forms.TextBox();
            this.labLeapFPS = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeapLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeapRight)).BeginInit();
            this.SuspendLayout();
            // 
            // picboxLeapLeft
            // 
            this.picboxLeapLeft.Location = new System.Drawing.Point(12, 29);
            this.picboxLeapLeft.Name = "picboxLeapLeft";
            this.picboxLeapLeft.Size = new System.Drawing.Size(673, 243);
            this.picboxLeapLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picboxLeapLeft.TabIndex = 0;
            this.picboxLeapLeft.TabStop = false;
            // 
            // labLeapLeft
            // 
            this.labLeapLeft.AutoSize = true;
            this.labLeapLeft.Location = new System.Drawing.Point(12, 11);
            this.labLeapLeft.Name = "labLeapLeft";
            this.labLeapLeft.Size = new System.Drawing.Size(89, 12);
            this.labLeapLeft.TabIndex = 1;
            this.labLeapLeft.Text = "Leap Left Camera";
            // 
            // labLeapRight
            // 
            this.labLeapRight.AutoSize = true;
            this.labLeapRight.Location = new System.Drawing.Point(10, 286);
            this.labLeapRight.Name = "labLeapRight";
            this.labLeapRight.Size = new System.Drawing.Size(96, 12);
            this.labLeapRight.TabIndex = 3;
            this.labLeapRight.Text = "Leap Right Camera";
            // 
            // picboxLeapRight
            // 
            this.picboxLeapRight.Location = new System.Drawing.Point(10, 304);
            this.picboxLeapRight.Name = "picboxLeapRight";
            this.picboxLeapRight.Size = new System.Drawing.Size(675, 243);
            this.picboxLeapRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picboxLeapRight.TabIndex = 2;
            this.picboxLeapRight.TabStop = false;
            // 
            // txtLeapFPS
            // 
            this.txtLeapFPS.Location = new System.Drawing.Point(106, 563);
            this.txtLeapFPS.Name = "txtLeapFPS";
            this.txtLeapFPS.Size = new System.Drawing.Size(145, 22);
            this.txtLeapFPS.TabIndex = 4;
            // 
            // labLeapFPS
            // 
            this.labLeapFPS.AutoSize = true;
            this.labLeapFPS.Location = new System.Drawing.Point(12, 566);
            this.labLeapFPS.Name = "labLeapFPS";
            this.labLeapFPS.Size = new System.Drawing.Size(88, 12);
            this.labLeapFPS.TabIndex = 5;
            this.labLeapFPS.Text = "Leap Camera FPS";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 611);
            this.Controls.Add(this.labLeapFPS);
            this.Controls.Add(this.txtLeapFPS);
            this.Controls.Add(this.labLeapRight);
            this.Controls.Add(this.picboxLeapRight);
            this.Controls.Add(this.labLeapLeft);
            this.Controls.Add(this.picboxLeapLeft);
            this.Name = "Main";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeapLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeapRight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picboxLeapLeft;
        private System.Windows.Forms.Label labLeapLeft;
        private System.Windows.Forms.Label labLeapRight;
        private System.Windows.Forms.PictureBox picboxLeapRight;
        private System.Windows.Forms.TextBox txtLeapFPS;
        private System.Windows.Forms.Label labLeapFPS;
    }
}

