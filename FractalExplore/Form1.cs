using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;


namespace FractalExplore
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel FractalPanel;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuAbout;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Drawing.Bitmap fractal;
		private System.Drawing.Color[] palette;
        private IContainer components;
        private const int MAX_ITERATIONS = 255;

		private double r0 = -2, i0 = -2, r1 = 2, i1 = 2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox Zoom;
		private System.Windows.Forms.Button RecenterButton;
		private System.Windows.Forms.RichTextBox Instructions;
		private System.Windows.Forms.GroupBox groupBox1;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Initialize();

			// Setup event handlers
			menuAbout.Click += new System.EventHandler(aboutMenuItem_Click);
			menuExit.Click += new System.EventHandler(exitMenuItem_Click);
			RecenterButton.Click += new System.EventHandler(recenter_Click);
			FractalPanel.Paint += new System.Windows.Forms.PaintEventHandler(paint);
			FractalPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(mouseUp);

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.FractalPanel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuAbout = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.Zoom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RecenterButton = new System.Windows.Forms.Button();
            this.Instructions = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FractalPanel
            // 
            this.FractalPanel.Location = new System.Drawing.Point(0, 0);
            this.FractalPanel.Name = "FractalPanel";
            this.FractalPanel.Size = new System.Drawing.Size(304, 304);
            this.FractalPanel.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(312, 296);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(144, 16);
            this.progressBar1.TabIndex = 1;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuAbout,
            this.menuExit});
            this.menuItem1.Text = "File";
            // 
            // menuAbout
            // 
            this.menuAbout.Index = 0;
            this.menuAbout.Text = "About";
            // 
            // menuExit
            // 
            this.menuExit.Index = 1;
            this.menuExit.Text = "Exit";
            // 
            // Zoom
            // 
            this.Zoom.Items.AddRange(new object[] {
            "1.5x",
            "2x",
            "3x",
            "4x",
            "5x",
            "10x"});
            this.Zoom.Location = new System.Drawing.Point(16, 40);
            this.Zoom.Name = "Zoom";
            this.Zoom.Size = new System.Drawing.Size(88, 21);
            this.Zoom.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(40, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Level";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Zoom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(320, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 112);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zoom";
            // 
            // RecenterButton
            // 
            this.RecenterButton.Location = new System.Drawing.Point(344, 80);
            this.RecenterButton.Name = "RecenterButton";
            this.RecenterButton.Size = new System.Drawing.Size(75, 23);
            this.RecenterButton.TabIndex = 5;
            this.RecenterButton.Text = "Re-Center";
            // 
            // Instructions
            // 
            this.Instructions.Location = new System.Drawing.Point(312, 136);
            this.Instructions.Name = "Instructions";
            this.Instructions.ReadOnly = true;
            this.Instructions.Size = new System.Drawing.Size(136, 144);
            this.Instructions.TabIndex = 6;
            this.Instructions.Text = "Instructions";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(456, 272);
            this.Controls.Add(this.Instructions);
            this.Controls.Add(this.RecenterButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.FractalPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Fractal Explorer";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		
		private void Initialize()
		{
			fractal = new Bitmap(304, 304);
			palette = new Color[256];
			for(int i = 0; i < 16; i++)
			{
				palette[i] = Color.FromArgb(255 - 16 * i, 16*i, 0);
				palette[i+16] = Color.FromArgb(0, 255 - 16*i, 16*i);
				palette[i+32] = Color.FromArgb(0, 16*i, 255);
				palette[i+48] = Color.FromArgb(16*i, 255, 255);
			}
			for (int i = 0; i < 64; i++)
			{
				palette[i+64] = Color.FromArgb(255-4*i, 255, 255);
				palette[i+128] = Color.FromArgb(0, 255-4*i, 255);
				palette[i+192] = Color.FromArgb(0, 0, 255-4*i);
			}
			DrawFractal();
			Invalidate(true);

			Zoom.SelectedIndex = 0;
			Instructions.Text = "Zoom in by selecting a zoom level and then clicking on the fractal using the left mouse button.\n\nZoom back out using the right mouse button.  Click \"Re-Center\" to return to original location and zoom level.";
		}

		private void DrawFractal()
		{
			int x0 = 0;
			int y0 = 0;
			int x1 = 304;
			int y1 = 304;
			double dr = (r1-r0) / (double)(x1-x0);
			double di = (i1-i0) / (double)(y1-y0);
			int color;
			for(int x=x0; x < x1; x++)
			{
				for(int y=y0; y < y1; y++)
				{
					color = CalcPoint(r0 + dr*x, i0 + dr*y);
					fractal.SetPixel(x, y, palette[color]);
				}
			}
		}
		
		private int CalcPoint(double real, double imaginary)
		{
			int count = 0;
			double r,i, nr;
			r = 0;
			i = 0;
			while (r*r + i*i < 4 && (count < MAX_ITERATIONS))
			{
				nr = r*r - i*i + real;
				i = 2*r*i + imaginary;
				r = nr;
				count++;
			}
			return count;
		}

		private double getZoom()
		{
			switch ((string) Zoom.Items[Zoom.SelectedIndex])
			{
				case "1.5x":
					return 1.5;
				case "2x":
					return 2;
				case "3x":
					return 3;
				case "4x":
					return 4;
				case "5x":
					return 5;
				case "10x":
					return 10;
				default:
					return 1.5;
			}
		}

		private void zoom(double r, double i, double zoomer)
		{
			double change = (r1-r0)*zoomer / 2;
			r0 = r - change;
			r1 = r + change;
			i0 = i - change;
			i1 = i + change;

			DrawFractal();
			Invalidate(true);
		}

		#region Event handlers...
		private void paint(object o, System.Windows.Forms.PaintEventArgs pea)
		{
			Graphics g = pea.Graphics;

			g.DrawImage(fractal, 0, 0, fractal.Width, fractal.Height);
		}

		private void aboutMenuItem_Click(object o, System.EventArgs e)
		{
			About aboutBox = new About();
			aboutBox.ShowDialog();
		}

		private void exitMenuItem_Click(object o, System.EventArgs e)
		{
			Dispose();
		}

		private void recenter_Click(object o, System.EventArgs e)
		{
			r0 = -2;
			i0 = -2;
			r1 = 2;
			i1 = 2;
			DrawFractal();
			Invalidate(true);
		}

		private void mouseUp(object o, System.Windows.Forms.MouseEventArgs mea)
		{
			double dr = (r1-r0) / (double)304;
			double di = (i1-i0) / (double)304;
			if (mea.Button == MouseButtons.Left)
			{
				zoom(r0 + dr * mea.X, i0 + di*mea.Y, (1 / getZoom()));
			}
			else if(mea.Button == MouseButtons.Right)
			{
				zoom(r0 + dr * mea.X, i0 + di*mea.Y, getZoom());
			}
		}

		#endregion
	}
}
