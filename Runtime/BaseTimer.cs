namespace BrightLib.Animation.Runtime
{
	[System.Serializable]
	public abstract class BaseTimer
	{
		public bool loops;

		public virtual void Reset()
		{

		}

		public abstract bool IsComplete();
	}

}