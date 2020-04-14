using UnityEngine;

namespace BrightLib.Animation.Runtime
{
	public class PlayAudioClip : StateMachineBehaviour
	{
		public bool useMultiple;
		public AudioClip clip;
		public AudioClip[] clips;

		public PlayCondition condition;
		public Delayer delayer;
		public Timer frequencyTimer;

		private AudioSource _source;

		private int _clipIndex;
		private bool _valid;

		private void OnEnable()
		{
			delayer.OnComplete += Execute;
			frequencyTimer.onComplete += Execute;
		}

		private void OnDisable()
		{
			delayer.OnComplete -= Execute;
			frequencyTimer.onComplete -= Execute;
		}

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			Validate(animator, stateInfo);

			delayer.Reset();
			frequencyTimer.Reset();
			frequencyTimer.loops = true;
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (condition == PlayCondition.OnEnter)
			{
				delayer.Update();
			}
			else if (condition == PlayCondition.OnUpdate)
			{
				frequencyTimer.Update();
			}			
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (condition != PlayCondition.OnExit) return;
			Execute();
		}

		private void Execute()
		{
			if (!_valid) return;

			var currClip = !useMultiple ? clip : clips[_clipIndex++ % clips.Length];
			_source.PlayOneShot(currClip);
		}

		private void Validate(Animator animator, AnimatorStateInfo stateInfo)
		{
			_valid = false;
			if (_source == null)
			{
				_source = animator.GetComponent<AudioSource>();
				if (_source == null)
				{
					Debug.LogWarning($"{animator.name}.{nameof(PlayAudioClip)}: No {nameof(AudioSource)} found on {animator.name}.");
					return;
				}
			}

			if (!useMultiple)
			{
				if (clip == null)
				{
					Debug.LogWarning($"{animator.name}.{nameof(PlayAudioClip)}: No {nameof(AudioClip)} added.");
					return;
				}
			}
			else
			{
				if (useMultiple && clips == null || clips.Length == 0)
				{
					Debug.LogWarning($"{animator.name}.{nameof(PlayAudioClip)}: No {nameof(AudioClip)}s added.");
					return;
				}	
			}
			_valid = true;
		}

	}

}