using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Form1 : Form
{
	private bool FullScreen = false;
	
	public Form1()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		this.Text = "Particle Simulator";
		this.Size = new Size(300, 300);
		this.DoubleBuffered = true;
		this.Load += Form1_Load;

		this.BackColor = Color.Black;
		this.ForeColor = Color.White;

		if (this.FullScreen == true)
		{
			this.FormBorderStyle = FormBorderStyle.None;
			this.Bounds = Screen.PrimaryScreen.Bounds;
		}
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		Scene.Run(this, () =>
		{
			SampleParticleSystem system = new SampleParticleSystem(Scene.Instance, 500);

			Scene.Instance.ParticleSystems.Add(system);
		});
	}
}