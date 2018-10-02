using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TextColorClip : PlayableAsset, ITimelineClipAsset
{
    public TextColorBehaviour template = new TextColorBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextColorBehaviour>.Create (graph, template);
        return playable;
    }
}
