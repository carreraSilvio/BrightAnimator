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
		}

		public override bool IsComplete()
		{
			return (Time.frameCount - _lastFrameExecuted >= frame);
		}
	}

}