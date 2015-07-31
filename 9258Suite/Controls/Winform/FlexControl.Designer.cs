namespace YoYoStudio.Controls.Winform
{
	partial class FlexControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlexControl));
			this.axFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
			((System.ComponentModel.ISupportInitialize)(this.axFlash)).BeginInit();
			this.SuspendLayout();
			// 
			// axFlash
			// 
			this.axFlash.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axFlash.Enabled = true;
			this.axFlash.Location = new System.Drawing.Point(0, 0);
			this.axFlash.Name = "axFlash";
			this.axFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axFlash.OcxState")));
			this.axFlash.Size = new System.Drawing.Size(0, 0);
			this.axFlash.TabIndex = 0;
			// 
			// FlexControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.axFlash);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "FlexControl";
			this.Size = new System.Drawing.Size(0, 0);
			((System.ComponentModel.ISupportInitialize)(this.axFlash)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private AxShockwaveFlashObjects.AxShockwaveFlash axFlash;
	}
}
