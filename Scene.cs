using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

public class Scene
{
	public static Scene Instance = null;

	public delegate void StartupDel();

	public static void Run(Form client)
	{
		Scene.Instance = new Scene(client);
	}
	
	public static void Run(Form client, StartupDel startupCode)
	{
		Scene.Instance = new Scene(client, startupCode);

		Scene.Instance.StartupCode.Invoke();
	}
	
	private Form _Client;
	private List<ParticleSystem> _ParticleSystems = new List<ParticleSystem>();
	private Color _ClearColor;

	private StartupDel _StartupCode;

	public Form Client
	{
		get
		{
			return _Client;
		}
	}

	public List<ParticleSystem> ParticleSystems
	{
		get
		{
			return _ParticleSystems;
		}
	}

	public Color ClearColor
	{
		get
		{
			return _ClearColor;
		}
		set
		{
			_ClearColor = value;
		}
	}

	public StartupDel StartupCode
	{
		get
		{
			return _StartupCode;
		}
		set
		{
			_StartupCode = value;
		}
	}

	private Timer DrawTimer;
	
	private Scene(Form client)
	{
		_Client = client;

		this.InitScene();
	}

	private Scene(Form client, StartupDel startupCode) : this(client)
	{
		this.StartupCode = startupCode;
	}

	private void InitScene()
	{
		this.ClearColor = this.Client.BackColor;
		this.Client.Paint += Parent_Paint;
		this.Client.KeyDown += Parent_KeyDown;
		this.Client.KeyUp += Parent_KeyUp;
		this.Client.Closing += (sender, e) => this.Dispose();
		
		this.DrawTimer = new Timer();
		this.DrawTimer.Interval = 1;
		this.DrawTimer.Tick += DrawTimer_Tick;
		this.DrawTimer.Start();
	}

	private void Parent_Paint(object sender, PaintEventArgs e)
	{
		this.Update(e.Graphics);

		this.Render(e.Graphics);
	}

	private void Parent_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			this.Client.Close();
		}
	}

	private void Parent_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.R)
		{
			this.Reset();
		}
	}

	private void DrawTimer_Tick(object sender, EventArgs e)
	{
		this.Client.Invalidate();
	}

	public void Update(Graphics g)
	{

		Parallel.ForEach<ParticleSystem>(this.ParticleSystems,
		(ParticleSystem pSystem) =>
		{
			pSystem.Update(g);
		});

		//foreach (ParticleSystem pSystem in this.ParticleSystems.ToArray())
		//{
			//pSystem.Update(g);
		//}
	}

	public void Render(Graphics g)
	{
		g.Clear(this.ClearColor);
		
		foreach (ParticleSystem pSystem in this.ParticleSystems.ToArray())
		{
			pSystem.Draw(g);
		}
	}

	public void Clear()
	{
		foreach (ParticleSystem pSystem in this.ParticleSystems.ToArray())
		{
			pSystem.Dispose();
		}
		
		GC.Collect();
	}

	public void Reset()
	{
		this.Clear();
		
		this.StartupCode.Invoke();
	}

	public void Dispose()
	{
		this.Clear();
		this.DrawTimer.Stop();
		this.DrawTimer.Dispose();
	}

	public void Start()
	{
		this.DrawTimer.Start();
	}

	public void Pause()
	{
		this.DrawTimer.Stop();
	}
}