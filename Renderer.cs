using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Renderer
{
	private Scene _Client;

	public Scene Client
	{
		get
		{
			return _Client;
		}
	}

	public Renderer(Scene client)
	{
		_Client = client;
	}

	public void Render(Graphics g)
	{
		foreach (ParticleSystem pSystem in this.Client.ParticleSystems.ToArray())
		{
			pSystem.Draw(g);
		}
	}
}