namespace CameraCapture
{
    partial class CameraCapture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraCapture));
            this.CamImageBoxLeft = new Emgu.CV.UI.ImageBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.CamImageBoxRight = new Emgu.CV.UI.ImageBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.VerticalLine2 = new System.Windows.Forms.PictureBox();
            this.VerticalLine1 = new System.Windows.Forms.PictureBox();
            this.HorizontalLine1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CamImageBoxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CamImageBoxRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalLine2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalLine1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalLine1)).BeginInit();
            this.SuspendLayout();
            // 
            // CamImageBoxLeft
            // 
            this.CamImageBoxLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CamImageBoxLeft.BackColor = System.Drawing.Color.Transparent;
            this.CamImageBoxLeft.BackgroundImage = global::CameraCapture.Properties.Resources.Logo;
            this.CamImageBoxLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CamImageBoxLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CamImageBoxLeft.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.CamImageBoxLeft.Enabled = false;
            this.CamImageBoxLeft.ErrorImage = global::CameraCapture.Properties.Resources.Logo;
            this.CamImageBoxLeft.InitialImage = global::CameraCapture.Properties.Resources.Logo;
            this.CamImageBoxLeft.Location = new System.Drawing.Point(33, 210);
            this.CamImageBoxLeft.Name = "CamImageBoxLeft";
            this.CamImageBoxLeft.Size = new System.Drawing.Size(320, 220);
            this.CamImageBoxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CamImageBoxLeft.TabIndex = 2;
            this.CamImageBoxLeft.TabStop = false;
            this.CamImageBoxLeft.WaitOnLoad = true;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.AutoSize = true;
            this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStart.Location = new System.Drawing.Point(639, 106);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(60, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "StartCam";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // CamImageBoxRight
            // 
            this.CamImageBoxRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CamImageBoxRight.BackColor = System.Drawing.Color.Transparent;
            this.CamImageBoxRight.BackgroundImage = global::CameraCapture.Properties.Resources.Logo;
            this.CamImageBoxRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CamImageBoxRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CamImageBoxRight.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.CamImageBoxRight.Enabled = false;
            this.CamImageBoxRight.ErrorImage = global::CameraCapture.Properties.Resources.Logo;
            this.CamImageBoxRight.InitialImage = global::CameraCapture.Properties.Resources.Logo;
            this.CamImageBoxRight.Location = new System.Drawing.Point(375, 210);
            this.CamImageBoxRight.Name = "CamImageBoxRight";
            this.CamImageBoxRight.Size = new System.Drawing.Size(320, 220);
            this.CamImageBoxRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CamImageBoxRight.TabIndex = 4;
            this.CamImageBoxRight.TabStop = false;
            this.CamImageBoxRight.WaitOnLoad = true;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Size = new System.Drawing.Size(724, 474);
            this.shapeContainer1.TabIndex = 5;
            this.shapeContainer1.TabStop = false;
            // 
            // VerticalLine2
            // 
            this.VerticalLine2.BackColor = System.Drawing.Color.White;
            this.VerticalLine2.Location = new System.Drawing.Point(530, 195);
            this.VerticalLine2.Margin = new System.Windows.Forms.Padding(1);
            this.VerticalLine2.Name = "VerticalLine2";
            this.VerticalLine2.Size = new System.Drawing.Size(1, 250);
            this.VerticalLine2.TabIndex = 7;
            this.VerticalLine2.TabStop = false;
            // 
            // VerticalLine1
            // 
            this.VerticalLine1.BackColor = System.Drawing.Color.White;
            this.VerticalLine1.Location = new System.Drawing.Point(182, 195);
            this.VerticalLine1.Margin = new System.Windows.Forms.Padding(1);
            this.VerticalLine1.Name = "VerticalLine1";
            this.VerticalLine1.Size = new System.Drawing.Size(1, 250);
            this.VerticalLine1.TabIndex = 8;
            this.VerticalLine1.TabStop = false;
            // 
            // HorizontalLine1
            // 
            this.HorizontalLine1.BackColor = System.Drawing.Color.White;
            this.HorizontalLine1.Location = new System.Drawing.Point(10, 326);
            this.HorizontalLine1.Margin = new System.Windows.Forms.Padding(1);
            this.HorizontalLine1.Name = "HorizontalLine1";
            this.HorizontalLine1.Size = new System.Drawing.Size(701, 1);
            this.HorizontalLine1.TabIndex = 9;
            this.HorizontalLine1.TabStop = false;
            // 
            // CameraCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(724, 474);
            this.Controls.Add(this.HorizontalLine1);
            this.Controls.Add(this.VerticalLine1);
            this.Controls.Add(this.VerticalLine2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.CamImageBoxRight);
            this.Controls.Add(this.CamImageBoxLeft);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CameraCapture";
            this.Text = "CameraCapture";
            this.Load += new System.EventHandler(this.CameraCapture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CamImageBoxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CamImageBoxRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalLine2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalLine1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalLine1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox CamImageBoxLeft;
        private System.Windows.Forms.Button btnStart;
        private Emgu.CV.UI.ImageBox CamImageBoxRight;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        //private Microsoft.VisualBasic.PowerPacks.LineShape lineShapeH;
        //private Microsoft.VisualBasic.PowerPacks.LineShape lineShapeV1;
        //private Microsoft.VisualBasic.PowerPacks.LineShape lineShapeV2;
        private System.Windows.Forms.PictureBox VerticalLine2;
        private System.Windows.Forms.PictureBox VerticalLine1;
        private System.Windows.Forms.PictureBox HorizontalLine1;
    }
}

