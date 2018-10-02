using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using UnityEngine.UI;

[TrackColor(0.6600659f, 0.7592782f, 0.8584906f)]
[TrackClipType(typeof(TextColorClip))]
[TrackBindingType(typeof(Text))]
public class TextColorTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TextColorMixerBehaviour>.Create (graph, inputCount);
    }

    // Please note this assumes only one component of type Text on the same gameobject.
    public override void GatherProperties (PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        Text trackBinding = director.GetGenericBinding(this) as Text;
        if (trackBinding == null)
            return;

        // These field names are procedurally generated estimations based on the associated property names.
        // If any of the names are incorrect you will get a DrivenPropertyManager error saying it has failed to register the name.
        // In this case you will need to find the correct backing field name.
        // The suggested way of finding the field name is to:
        // 1. Make sure your scene is serialized to text.
        // 2. Search the text for the track binding component type.
        // 3. Look through the field names until you see one that looks correct.
        driver.AddFromName<Text>(trackBinding.gameObject, "m_Color");
#endif
        base.GatherProperties (director, driver);
    }
}
