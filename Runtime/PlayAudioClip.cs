using UnityEngine;
using System;

namespace BrightLib.Animation.Runtime
{
	public class PlayAudioClip : StateMachineBehaviour
	{ 
		public float delay;
		public PlayCondition condition;
		public float frequency;

		public bool useMultiple;
		public AudioClip clip;
		public AudioClip[] clips;

		private AudioSource _source;

		private float _lastUpdateTime;
		private int _clipIndex;
		private bool _valid;
		private bool _executed;

		

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			Validate(animator, stateInfo);

			_lastUpdateTime = Time.time;
			_executed = false;
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (condition == PlayCondition.OnEnter)
			{
				if (Time.time - _lastUpdateTime < delay) return;
				if (_executed) return;

				Execute();
			}
			else if (condition == PlayCondition.OnUpdate)
			{
				if (Time.time - _lastUpdateTime < frequency) return;
				_lastUpdateTime = Time.time;

				Execute();
			}			
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if(condition != PlayCondition.OnExit) return;
			Execute();
		}

		private void Execute()
		{
			if (!_valid) return;

			var currClip = !useMultiple ? clip : clips[_clipIndex++ % clips.Length];
			_source.PlayOneShot(currClip);
			_executed = true;
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