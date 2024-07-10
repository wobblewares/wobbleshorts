using Cysharp.Threading.Tasks;
using GraphProcessor;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Video;

namespace WobbleShorts
{
    [Serializable, NodeMenuItem("WobbleShorts/Play Video Clip Node")]
    public class PlayClipVideoNode : DelayedNode
    {
        public override string name => "Play Clip Video";

        [Input("Video"), SerializeField]
        public VideoClip clip;
        
        [Input("Loop"), SerializeField]
        public bool loop = false;
        
        [Input("Duration"), SerializeField]
        [EnableIf("loop")]
        public float duration = 1.0f;
        
 
        
        protected override void Process()
        {
            var videoPlayer = GameObject.FindObjectOfType<VideoPlayer>();
            if (videoPlayer == null)
            {
                Debug.LogError($"Ensure there is a {nameof(VideoPlayer)} component in the scene.");
                return;
            }

            // Wait.
            UniTask.Create(async () =>
            {
                // Set the video
                videoPlayer.clip = clip;
                videoPlayer.isLooping = loop;
                
                // Play for duration, or until the video is done.
                double totalDuration = videoPlayer.isLooping ? duration : clip.length;
                float totalTime = 0.0f;
                await UniTask.Delay(TimeSpan.FromSeconds(totalDuration));
                Finish();
            });
        }
    }
}