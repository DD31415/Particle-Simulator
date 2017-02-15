using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

public class ParticleSystem
{
	private Scene _Parent;
	private List<Particle> _Particles = new List<Particle>();

	public virtual Scene Parent
	{
		get
		{
			return _Parent;
		}
		set
		{
			_Parent = value;
		}
	}

	public virtual List<Particle> Particles
	{
		get
		{
			return _Particles;
		}
	}

	public ParticleSystem(Scene parent)
	{
		this.Parent = parent;
	}

	public ParticleSystem(Scene parent, int ParticleCount) : this(parent)
	{
		this.Parent = parent;
	}

	public virtual void CreateParticles(int count)
	{
	}

	public virtual Particle CreateParticle()
	{
		return null;
	}

	public virtual void Update(Graphics g)
	{
		Parallel.ForEach<Particle>(this.Particles,
		(Particle p) =>
		{
			p.Update(g);
		});

		//foreach (Particle p in this.Particles)
		//{
			//p.Update(g);
		//}
	}

	public virtual void Draw(Graphics g)
	{
		foreach (Particle p in this.Particles)
		{
			p.Draw(g);
		}
	}

	public virtual void Dispose()
	{
		foreach (Particle p in this.Particles.ToArray())
		{
			p.Dispose();
		}
	}
}