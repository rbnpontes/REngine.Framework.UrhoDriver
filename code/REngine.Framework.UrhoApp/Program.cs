namespace REngine.Framework.UrhoApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Application.Run(new UrhoDriver.RootDriver() ,new App());
		}
	}
}
