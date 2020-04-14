using UnityEngine;

namespace BrightLib.Animation.Runtime
{
    [System.Serializable]
	public class Timer : BaseTimer
	{
		public float time;
		private float _lastTimeExecuted;

		public override void Reset()
		{
			_lastTimeExecuted = Time.time;
			_completed = false;
		}

		public override void Update()
		{
			if (_completed) return;

			_completed = Time.time - _lastTimeExecuted >= time;

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