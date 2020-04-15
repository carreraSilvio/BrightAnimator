using System;

namespace BrightLib.Animation.Runtime
{
	public enum DelayType {Time, Frame }

	[System.Serializable]
	public class Delayer
	{
		public DelayType delayType;

		public Timer timer;
		public FrameTimer frameTimer;

		public void Reset()
		{
			timer.Reset();
			frameTimer.Reset();
		}

		public void Update()
		{
			if (delayType == DelayType.Time) timer.Update();
			else frameTimer.Update();
		}

		public Action OnComplete
		{
			get
			{
				return timer.onComplete;
			}
			set
			{
				timer.onComplete = value;
				frameTimer.onComplete = value;
			}
		}
	}

}