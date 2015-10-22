namespace calibration_of_leap_motion
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
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.picboxUndistortedR = new System.Windows.Forms.PictureBox();
            this.picboxUndistortedL = new System.Windows.Forms.PictureBox();
            this.labLeapUndistortedLeft = new System.Windows.Forms.Label();
            this.labLeapUndistortedRight = new System.Windows.Forms.Label();
            this.picboxRGB = new System.Windows.Forms.PictureBox();
            this.labRGBCamera = new System.Windows.Forms.Label();
            this.picboxRGBCapture = new System.Windows.Forms.PictureBox();
            this.labRGBCapture = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeapLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeapRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxUndistortedR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxUndistortedL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRGB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRGBCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // picboxLeapLeft
            // 
            this.picboxLeapLeft.Location = new System.Drawing.Point(12, 29);
            this.picboxLeapLeft.Name = "picboxLeapLeft";
            this.picboxLeapLeft.Size = new System.Drawing.Size(673, 243);
            this.picboxLeapLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxLeapLeft.TabIndex = 0;
            this.picboxLeapLeft.TabStop = false;
            // 
            // labLeapLeft
            // 
            this.labLeapLeft.AutoSize = true;
            this.labLeapLeft.Location = new System.Drawing.Point(12, 13);
            this.labLeapLeft.Name = "labLeapLeft";
            this.labLeapLeft.Size = new System.Drawing.Size(89, 12);
            this.labLeapLeft.TabIndex = 1;
            this.labLeapLeft.Text = "Leap Left Camera";
            // 
            // labLeapRight
            // 
            this.labLeapRight.AutoSize = true;
            this.labLeapRight.Location = new System.Drawing.Point(704, 13);
            this.labLeapRight.Name = "labLeapRight";
            this.labLeapRight.Size = new System.Drawing.Size(96, 12);
            this.labLeapRight.TabIndex = 3;
            this.labLeapRight.Text = "Leap Right Camera";
            // 
            // picboxLeapRight
            // 
            this.picboxLeapRight.Location = new System.Drawing.Point(704, 29);
            this.picboxLeapRight.Name = "picboxLeapRight";
            this.picboxLeapRight.Size = new System.Drawing.Size(675, 243);
            this.picboxLeapRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxLeapRight.TabIndex = 2;
            this.picboxLeapRight.TabStop = false;
            // 
            // txtLeapFPS
            // 
            this.txtLeapFPS.Location = new System.Drawing.Point(800, 523);
            this.txtLeapFPS.Name = "txtLeapFPS";
            this.txtLeapFPS.Size = new System.Drawing.Size(145, 22);
            this.txtLeapFPS.TabIndex = 4;
            // 
            // labLeapFPS
            // 
            this.labLeapFPS.AutoSize = true;
            this.labLeapFPS.Location = new System.Drawing.Point(706, 526);
            this.labLeapFPS.Name = "labLeapFPS";
            this.labLeapFPS.Size = new System.Drawing.Size(88, 12);
            this.labLeapFPS.TabIndex = 5;
            this.labLeapFPS.Text = "Leap Camera FPS";
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(706, 567);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(75, 23);
            this.btnSaveImage.TabIndex = 6;
            this.btnSaveImage.Text = "Save";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSavaImage_Click);
            // 
            // picboxUndistortedR
            // 
            this.picboxUndistortedR.Location = new System.Drawing.Point(1048, 460);
            this.picboxUndistortedR.Name = "picboxUndistortedR";
            this.picboxUndistortedR.Size = new System.Drawing.Size(331, 130);
            this.picboxUndistortedR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxUndistortedR.TabIndex = 7;
            this.picboxUndistortedR.TabStop = false;
            // 
            // picboxUndistortedL
            // 
            this.picboxUndistortedL.Location = new System.Drawing.Point(1048, 302);
            this.picboxUndistortedL.Name = "picboxUndistortedL";
            this.picboxUndistortedL.Size = new System.Drawing.Size(331, 130);
            this.picboxUndistortedL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxUndistortedL.TabIndex = 8;
            this.picboxUndistortedL.TabStop = false;
            // 
            // labLeapUndistortedLeft
            // 
            this.labLeapUndistortedLeft.AutoSize = true;
            this.labLeapUndistortedLeft.Location = new System.Drawing.Point(1050, 287);
            this.labLeapUndistortedLeft.Name = "labLeapUndistortedLeft";
            this.labLeapUndistortedLeft.Size = new System.Drawing.Size(139, 12);
            this.labLeapUndistortedLeft.TabIndex = 9;
            this.labLeapUndistortedLeft.Text = "Leap Left Undistorted Image";
            // 
            // labLeapUndistortedRight
            // 
            this.labLeapUndistortedRight.AutoSize = true;
            this.labLeapUndistortedRight.Location = new System.Drawing.Point(1046, 445);
            this.labLeapUndistortedRight.Name = "labLeapUndistortedRight";
            this.labLeapUndistortedRight.Size = new System.Drawing.Size(146, 12);
            this.labLeapUndistortedRight.TabIndex = 10;
            this.labLeapUndistortedRight.Text = "Leap Right Undistorted Image";
            // 
            // picboxRGB
            // 
            this.picboxRGB.Location = new System.Drawing.Point(12, 302);
            this.picboxRGB.Name = "picboxRGB";
            this.picboxRGB.Size = new System.Drawing.Size(673, 375);
            this.picboxRGB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxRGB.TabIndex = 11;
            this.picboxRGB.TabStop = false;
            // 
            // labRGBCamera
            // 
            this.labRGBCamera.AutoSize = true;
            this.labRGBCamera.Location = new System.Drawing.Point(10, 287);
            this.labRGBCamera.Name = "labRGBCamera";
            this.labRGBCamera.Size = new System.Drawing.Size(68, 12);
            this.labRGBCamera.TabIndex = 12;
            this.labRGBCamera.Text = "RGB Camera";
            // 
            // picboxRGBCapture
            // 
            this.picboxRGBCapture.Location = new System.Drawing.Point(706, 302);
            this.picboxRGBCapture.Name = "picboxRGBCapture";
            this.picboxRGBCapture.Size = new System.Drawing.Size(318, 186);
            this.picboxRGBCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxRGBCapture.TabIndex = 14;
            this.picboxRGBCapture.TabStop = false;
            // 
            // labRGBCapture
            // 
            this.labRGBCapture.AutoSize = true;
            this.labRGBCapture.Location = new System.Drawing.Point(704, 287);
            this.labRGBCapture.Name = "labRGBCapture";
            this.labRGBCapture.Size = new System.Drawing.Size(101, 12);
            this.labRGBCapture.TabIndex = 15;
            this.labRGBCapture.Text = "RGB Capture Image";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 696);
            this.Controls.Add(this.labRGBCapture);
            this.Controls.Add(this.picboxRGBCapture);
            this.Controls.Add(this.labRGBCamera);
            this.Controls.Add(this.picboxRGB);
            this.Controls.Add(this.labLeapUndistortedRight);
            this.Controls.Add(this.labLeapUndistortedLeft);
            this.Controls.Add(this.picboxUndistortedL);
            this.Controls.Add(this.picboxUndistortedR);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.labLeapFPS);
            this.Controls.Add(this.txtLeapFPS);
            this.Controls.Add(this.labLeapRight);
            this.Controls.Add(this.picboxLeapRight);
            this.Controls.Add(this.labLeapLeft);
            this.Controls.Add(this.picboxLeapLeft);
            this.Name = "Main";
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeapLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeapRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxUndistortedR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxUndistortedL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRGB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRGBCapture)).EndInit();
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
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.PictureBox picboxUndistortedR;
        private System.Windows.Forms.PictureBox picboxUndistortedL;
        private System.Windows.Forms.Label labLeapUndistortedLeft;
        private System.Windows.Forms.Label labLeapUndistortedRight;
        private System.Windows.Forms.PictureBox picboxRGB;
        private System.Windows.Forms.Label labRGBCamera;
        private System.Windows.Forms.PictureBox picboxRGBCapture;
        private System.Windows.Forms.Label labRGBCapture;
    }
}

