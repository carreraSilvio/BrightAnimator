using System;

namespace BrightLib.Animation.Runtime
{
	[System.Serializable]
	public abstract class BaseTimer
	{
		public bool loops;
		public Action onComplete;

		protected bool _completed;

		public virtual void Reset()
		{

		}

		public abstract void Update();

		protected void FireOnComplete()
		{
			_completed = true;
			onComplete?.Invoke();
		}

		public bool IsComplete()
		{
			var result = _completed;
			_completed = false;
			return result;
		}
	}

}