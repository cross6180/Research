namespace Disparity_Map_for_Leap
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.picboxLeft = new System.Windows.Forms.PictureBox();
            this.picboxDisp = new System.Windows.Forms.PictureBox();
            this.picbocRight = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TBminDisparity = new System.Windows.Forms.TrackBar();
            this.labminDisparity = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labnumDisparities = new System.Windows.Forms.Label();
            this.TBnumDisparities = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.labSADWindowSize = new System.Windows.Forms.Label();
            this.TBSADWindowSize = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.labp1 = new System.Windows.Forms.Label();
            this.TBp1 = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.labuniquenessRatio = new System.Windows.Forms.Label();
            this.TBuniquenessRatio = new System.Windows.Forms.TrackBar();
            this.label13 = new System.Windows.Forms.Label();
            this.labpreFilterCap = new System.Windows.Forms.Label();
            this.TBpreFilterCap = new System.Windows.Forms.TrackBar();
            this.label15 = new System.Windows.Forms.Label();
            this.labdisp12MaxDiff = new System.Windows.Forms.Label();
            this.TBdisp12MaxDiff = new System.Windows.Forms.TrackBar();
            this.label17 = new System.Windows.Forms.Label();
            this.labp2 = new System.Windows.Forms.Label();
            this.TBp2 = new System.Windows.Forms.TrackBar();
            this.label19 = new System.Windows.Forms.Label();
            this.TBspeckleRange = new System.Windows.Forms.TrackBar();
            this.label20 = new System.Windows.Forms.Label();
            this.TBspeckleWindowSize = new System.Windows.Forms.TrackBar();
            this.labspeckleRange = new System.Windows.Forms.Label();
            this.labspeckleWindowSize = new System.Windows.Forms.Label();
            this.btnBlend = new System.Windows.Forms.Button();
            this.btnBlendRGB = new System.Windows.Forms.Button();
            this.picboxRGB = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxDisp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbocRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBminDisparity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBnumDisparities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBSADWindowSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBp1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBuniquenessRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBpreFilterCap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBdisp12MaxDiff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBp2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBspeckleRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBspeckleWindowSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRGB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // picboxLeft
            // 
            this.picboxLeft.Location = new System.Drawing.Point(12, 24);
            this.picboxLeft.Name = "picboxLeft";
            this.picboxLeft.Size = new System.Drawing.Size(445, 169);
            this.picboxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxLeft.TabIndex = 0;
            this.picboxLeft.TabStop = false;
            // 
            // picboxDisp
            // 
            this.picboxDisp.Location = new System.Drawing.Point(11, 411);
            this.picboxDisp.Name = "picboxDisp";
            this.picboxDisp.Size = new System.Drawing.Size(445, 166);
            this.picboxDisp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxDisp.TabIndex = 1;
            this.picboxDisp.TabStop = false;
            // 
            // picbocRight
            // 
            this.picbocRight.Location = new System.Drawing.Point(11, 221);
            this.picbocRight.Name = "picbocRight";
            this.picbocRight.Size = new System.Drawing.Size(446, 163);
            this.picbocRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbocRight.TabIndex = 2;
            this.picbocRight.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "left";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "right";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 396);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "disp";
            // 
            // TBminDisparity
            // 
            this.TBminDisparity.Location = new System.Drawing.Point(488, 34);
            this.TBminDisparity.Maximum = 0;
            this.TBminDisparity.Minimum = -100;
            this.TBminDisparity.Name = "TBminDisparity";
            this.TBminDisparity.Size = new System.Drawing.Size(516, 45);
            this.TBminDisparity.TabIndex = 6;
            this.TBminDisparity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBminDisparity.Visible = false;
            this.TBminDisparity.Scroll += new System.EventHandler(this.TBminDisparity_Scroll);
            // 
            // labminDisparity
            // 
            this.labminDisparity.AutoSize = true;
            this.labminDisparity.Location = new System.Drawing.Point(1010, 38);
            this.labminDisparity.Name = "labminDisparity";
            this.labminDisparity.Size = new System.Drawing.Size(11, 12);
            this.labminDisparity.TabIndex = 7;
            this.labminDisparity.Text = "0";
            this.labminDisparity.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(494, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "minDisparity";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(494, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "numDisparities";
            this.label5.Visible = false;
            // 
            // labnumDisparities
            // 
            this.labnumDisparities.AutoSize = true;
            this.labnumDisparities.Location = new System.Drawing.Point(1010, 90);
            this.labnumDisparities.Name = "labnumDisparities";
            this.labnumDisparities.Size = new System.Drawing.Size(23, 12);
            this.labnumDisparities.TabIndex = 10;
            this.labnumDisparities.Text = "112";
            this.labnumDisparities.Visible = false;
            // 
            // TBnumDisparities
            // 
            this.TBnumDisparities.LargeChange = 16;
            this.TBnumDisparities.Location = new System.Drawing.Point(488, 85);
            this.TBnumDisparities.Maximum = 1600;
            this.TBnumDisparities.Name = "TBnumDisparities";
            this.TBnumDisparities.Size = new System.Drawing.Size(516, 45);
            this.TBnumDisparities.SmallChange = 16;
            this.TBnumDisparities.TabIndex = 9;
            this.TBnumDisparities.TickFrequency = 16;
            this.TBnumDisparities.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBnumDisparities.Value = 112;
            this.TBnumDisparities.Visible = false;
            this.TBnumDisparities.Scroll += new System.EventHandler(this.TBnumDisparities_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(494, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "SADWindowSize";
            this.label7.Visible = false;
            // 
            // labSADWindowSize
            // 
            this.labSADWindowSize.AutoSize = true;
            this.labSADWindowSize.Location = new System.Drawing.Point(1010, 141);
            this.labSADWindowSize.Name = "labSADWindowSize";
            this.labSADWindowSize.Size = new System.Drawing.Size(11, 12);
            this.labSADWindowSize.TabIndex = 13;
            this.labSADWindowSize.Text = "3";
            this.labSADWindowSize.Visible = false;
            // 
            // TBSADWindowSize
            // 
            this.TBSADWindowSize.Location = new System.Drawing.Point(488, 136);
            this.TBSADWindowSize.Maximum = 11;
            this.TBSADWindowSize.Minimum = 3;
            this.TBSADWindowSize.Name = "TBSADWindowSize";
            this.TBSADWindowSize.Size = new System.Drawing.Size(516, 45);
            this.TBSADWindowSize.SmallChange = 2;
            this.TBSADWindowSize.TabIndex = 12;
            this.TBSADWindowSize.TickFrequency = 2;
            this.TBSADWindowSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBSADWindowSize.Value = 3;
            this.TBSADWindowSize.Visible = false;
            this.TBSADWindowSize.Scroll += new System.EventHandler(this.TBSADWindowSize_Scroll);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(494, 172);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "p1";
            this.label9.Visible = false;
            // 
            // labp1
            // 
            this.labp1.AutoSize = true;
            this.labp1.Location = new System.Drawing.Point(1010, 192);
            this.labp1.Name = "labp1";
            this.labp1.Size = new System.Drawing.Size(11, 12);
            this.labp1.TabIndex = 16;
            this.labp1.Text = "0";
            this.labp1.Visible = false;
            // 
            // TBp1
            // 
            this.TBp1.Location = new System.Drawing.Point(488, 187);
            this.TBp1.Maximum = 1000;
            this.TBp1.Name = "TBp1";
            this.TBp1.Size = new System.Drawing.Size(516, 45);
            this.TBp1.TabIndex = 15;
            this.TBp1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBp1.Visible = false;
            this.TBp1.Scroll += new System.EventHandler(this.TBp1_Scroll);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(494, 376);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "uniquenessRatio";
            this.label11.Visible = false;
            // 
            // labuniquenessRatio
            // 
            this.labuniquenessRatio.AutoSize = true;
            this.labuniquenessRatio.Location = new System.Drawing.Point(1010, 396);
            this.labuniquenessRatio.Name = "labuniquenessRatio";
            this.labuniquenessRatio.Size = new System.Drawing.Size(17, 12);
            this.labuniquenessRatio.TabIndex = 28;
            this.labuniquenessRatio.Text = "12";
            this.labuniquenessRatio.Visible = false;
            // 
            // TBuniquenessRatio
            // 
            this.TBuniquenessRatio.Location = new System.Drawing.Point(488, 391);
            this.TBuniquenessRatio.Maximum = 15;
            this.TBuniquenessRatio.Minimum = 5;
            this.TBuniquenessRatio.Name = "TBuniquenessRatio";
            this.TBuniquenessRatio.Size = new System.Drawing.Size(516, 45);
            this.TBuniquenessRatio.TabIndex = 27;
            this.TBuniquenessRatio.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBuniquenessRatio.Value = 12;
            this.TBuniquenessRatio.Visible = false;
            this.TBuniquenessRatio.Scroll += new System.EventHandler(this.TBuniquenessRatio_Scroll);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(494, 325);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 12);
            this.label13.TabIndex = 26;
            this.label13.Text = "preFilterCap";
            this.label13.Visible = false;
            // 
            // labpreFilterCap
            // 
            this.labpreFilterCap.AutoSize = true;
            this.labpreFilterCap.Location = new System.Drawing.Point(1010, 345);
            this.labpreFilterCap.Name = "labpreFilterCap";
            this.labpreFilterCap.Size = new System.Drawing.Size(17, 12);
            this.labpreFilterCap.TabIndex = 25;
            this.labpreFilterCap.Text = "20";
            this.labpreFilterCap.Visible = false;
            // 
            // TBpreFilterCap
            // 
            this.TBpreFilterCap.Location = new System.Drawing.Point(488, 340);
            this.TBpreFilterCap.Maximum = 100;
            this.TBpreFilterCap.Name = "TBpreFilterCap";
            this.TBpreFilterCap.Size = new System.Drawing.Size(516, 45);
            this.TBpreFilterCap.TabIndex = 24;
            this.TBpreFilterCap.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBpreFilterCap.Value = 20;
            this.TBpreFilterCap.Visible = false;
            this.TBpreFilterCap.Scroll += new System.EventHandler(this.TBpreFilterCap_Scroll);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(494, 274);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 12);
            this.label15.TabIndex = 23;
            this.label15.Text = "disp12MaxDiff";
            this.label15.Visible = false;
            // 
            // labdisp12MaxDiff
            // 
            this.labdisp12MaxDiff.AutoSize = true;
            this.labdisp12MaxDiff.Location = new System.Drawing.Point(1010, 294);
            this.labdisp12MaxDiff.Name = "labdisp12MaxDiff";
            this.labdisp12MaxDiff.Size = new System.Drawing.Size(11, 12);
            this.labdisp12MaxDiff.TabIndex = 22;
            this.labdisp12MaxDiff.Text = "8";
            this.labdisp12MaxDiff.Visible = false;
            // 
            // TBdisp12MaxDiff
            // 
            this.TBdisp12MaxDiff.Location = new System.Drawing.Point(488, 289);
            this.TBdisp12MaxDiff.Maximum = 100;
            this.TBdisp12MaxDiff.Name = "TBdisp12MaxDiff";
            this.TBdisp12MaxDiff.Size = new System.Drawing.Size(516, 45);
            this.TBdisp12MaxDiff.TabIndex = 21;
            this.TBdisp12MaxDiff.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBdisp12MaxDiff.Value = 8;
            this.TBdisp12MaxDiff.Visible = false;
            this.TBdisp12MaxDiff.Scroll += new System.EventHandler(this.TBdisp12MaxDiff_Scroll);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(494, 223);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 12);
            this.label17.TabIndex = 20;
            this.label17.Text = "p2";
            this.label17.Visible = false;
            // 
            // labp2
            // 
            this.labp2.AutoSize = true;
            this.labp2.Location = new System.Drawing.Point(1010, 243);
            this.labp2.Name = "labp2";
            this.labp2.Size = new System.Drawing.Size(11, 12);
            this.labp2.TabIndex = 19;
            this.labp2.Text = "0";
            this.labp2.Visible = false;
            // 
            // TBp2
            // 
            this.TBp2.Location = new System.Drawing.Point(488, 238);
            this.TBp2.Maximum = 4000;
            this.TBp2.Name = "TBp2";
            this.TBp2.Size = new System.Drawing.Size(516, 45);
            this.TBp2.TabIndex = 18;
            this.TBp2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBp2.Visible = false;
            this.TBp2.Scroll += new System.EventHandler(this.TBp2_Scroll);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(494, 478);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 12);
            this.label19.TabIndex = 33;
            this.label19.Text = "speckleRange";
            this.label19.Visible = false;
            // 
            // TBspeckleRange
            // 
            this.TBspeckleRange.Location = new System.Drawing.Point(488, 493);
            this.TBspeckleRange.Minimum = 1;
            this.TBspeckleRange.Name = "TBspeckleRange";
            this.TBspeckleRange.Size = new System.Drawing.Size(516, 45);
            this.TBspeckleRange.TabIndex = 32;
            this.TBspeckleRange.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBspeckleRange.Value = 3;
            this.TBspeckleRange.Visible = false;
            this.TBspeckleRange.Scroll += new System.EventHandler(this.TBspeckleRange_Scroll);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(494, 427);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 12);
            this.label20.TabIndex = 31;
            this.label20.Text = "speckleWindowSize";
            this.label20.Visible = false;
            // 
            // TBspeckleWindowSize
            // 
            this.TBspeckleWindowSize.Location = new System.Drawing.Point(488, 442);
            this.TBspeckleWindowSize.Maximum = 200;
            this.TBspeckleWindowSize.Name = "TBspeckleWindowSize";
            this.TBspeckleWindowSize.Size = new System.Drawing.Size(516, 45);
            this.TBspeckleWindowSize.TabIndex = 30;
            this.TBspeckleWindowSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBspeckleWindowSize.Value = 65;
            this.TBspeckleWindowSize.Visible = false;
            this.TBspeckleWindowSize.Scroll += new System.EventHandler(this.TBspeckleWindowSize_Scroll);
            // 
            // labspeckleRange
            // 
            this.labspeckleRange.AutoSize = true;
            this.labspeckleRange.Location = new System.Drawing.Point(1010, 497);
            this.labspeckleRange.Name = "labspeckleRange";
            this.labspeckleRange.Size = new System.Drawing.Size(11, 12);
            this.labspeckleRange.TabIndex = 35;
            this.labspeckleRange.Text = "3";
            this.labspeckleRange.Visible = false;
            // 
            // labspeckleWindowSize
            // 
            this.labspeckleWindowSize.AutoSize = true;
            this.labspeckleWindowSize.Location = new System.Drawing.Point(1010, 446);
            this.labspeckleWindowSize.Name = "labspeckleWindowSize";
            this.labspeckleWindowSize.Size = new System.Drawing.Size(17, 12);
            this.labspeckleWindowSize.TabIndex = 34;
            this.labspeckleWindowSize.Text = "65";
            this.labspeckleWindowSize.Visible = false;
            // 
            // btnBlend
            // 
            this.btnBlend.Location = new System.Drawing.Point(496, 535);
            this.btnBlend.Name = "btnBlend";
            this.btnBlend.Size = new System.Drawing.Size(123, 23);
            this.btnBlend.TabIndex = 36;
            this.btnBlend.Text = "save and blend";
            this.btnBlend.UseVisualStyleBackColor = true;
            this.btnBlend.Click += new System.EventHandler(this.btnBlend_Click);
            // 
            // btnBlendRGB
            // 
            this.btnBlendRGB.Location = new System.Drawing.Point(638, 535);
            this.btnBlendRGB.Name = "btnBlendRGB";
            this.btnBlendRGB.Size = new System.Drawing.Size(123, 23);
            this.btnBlendRGB.TabIndex = 37;
            this.btnBlendRGB.Text = "blend RGB and";
            this.btnBlendRGB.UseVisualStyleBackColor = true;
            this.btnBlendRGB.Click += new System.EventHandler(this.btnBlendRGB_Click);
            // 
            // picboxRGB
            // 
            this.picboxRGB.Location = new System.Drawing.Point(496, 24);
            this.picboxRGB.Name = "picboxRGB";
            this.picboxRGB.Size = new System.Drawing.Size(445, 169);
            this.picboxRGB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxRGB.TabIndex = 38;
            this.picboxRGB.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(496, 221);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(445, 169);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 39;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 593);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.picboxRGB);
            this.Controls.Add(this.btnBlendRGB);
            this.Controls.Add(this.btnBlend);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picbocRight);
            this.Controls.Add(this.picboxDisp);
            this.Controls.Add(this.picboxLeft);
            this.Controls.Add(this.labspeckleRange);
            this.Controls.Add(this.labspeckleWindowSize);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.TBspeckleRange);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.TBspeckleWindowSize);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.labuniquenessRatio);
            this.Controls.Add(this.TBuniquenessRatio);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.labpreFilterCap);
            this.Controls.Add(this.TBpreFilterCap);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.labdisp12MaxDiff);
            this.Controls.Add(this.TBdisp12MaxDiff);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.labp2);
            this.Controls.Add(this.TBp2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labp1);
            this.Controls.Add(this.TBp1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labSADWindowSize);
            this.Controls.Add(this.TBSADWindowSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labnumDisparities);
            this.Controls.Add(this.TBnumDisparities);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labminDisparity);
            this.Controls.Add(this.TBminDisparity);
            this.Name = "MainForm";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.picboxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxDisp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbocRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBminDisparity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBnumDisparities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBSADWindowSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBp1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBuniquenessRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBpreFilterCap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBdisp12MaxDiff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBp2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBspeckleRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBspeckleWindowSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRGB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picboxLeft;
        private System.Windows.Forms.PictureBox picboxDisp;
        private System.Windows.Forms.PictureBox picbocRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar TBminDisparity;
        private System.Windows.Forms.Label labminDisparity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labnumDisparities;
        private System.Windows.Forms.TrackBar TBnumDisparities;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labSADWindowSize;
        private System.Windows.Forms.TrackBar TBSADWindowSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labp1;
        private System.Windows.Forms.TrackBar TBp1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labuniquenessRatio;
        private System.Windows.Forms.TrackBar TBuniquenessRatio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labpreFilterCap;
        private System.Windows.Forms.TrackBar TBpreFilterCap;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labdisp12MaxDiff;
        private System.Windows.Forms.TrackBar TBdisp12MaxDiff;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label labp2;
        private System.Windows.Forms.TrackBar TBp2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TrackBar TBspeckleRange;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TrackBar TBspeckleWindowSize;
        private System.Windows.Forms.Label labspeckleRange;
        private System.Windows.Forms.Label labspeckleWindowSize;
        private System.Windows.Forms.Button btnBlend;
        private System.Windows.Forms.Button btnBlendRGB;
        private System.Windows.Forms.PictureBox picboxRGB;
        private System.Windows.Forms.PictureBox pictureBox1;


    }
}

