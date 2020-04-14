using UnityEngine;

namespace BrightLib.Animation.Runtime
{
    [System.Serializable]
	public class FrameTimer : BaseTimer
	{
		public int frame;
		private int _lastFrameExecuted;

		public override void Reset()
		{
			_lastFrameExecuted = Time.frameCount;
			_completed = false;
		}

		public override void Update()
		{
			if (_completed) return;

			_completed = Time.frameCount - _lastFrameExecuted >= frame;

			if (_completed)
			{
				FireOnComplete();
				if (loops)
				{
					Reset();
				}
			}
		}
	}

}