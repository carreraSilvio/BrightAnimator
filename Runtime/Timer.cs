using UnityEngine;

namespace BrightLib.Animation.Runtime
{
    [System.Serializable]
	public class Timer : BaseTimer
	{
		public float time;
		private float _lastTimeExecuted;

		public override bool IsComplete()
		{
			var result = Time.time - _lastTimeExecuted >= time;

			if (loops) Reset();
			return result;
		}

		public override void Reset()
		{
			_lastTimeExecuted = Time.time;
		}
	}

}