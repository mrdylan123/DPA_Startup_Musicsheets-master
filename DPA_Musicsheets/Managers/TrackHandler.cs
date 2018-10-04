using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Managers
{
    public class TrackHandler
    {
        public int _beatNote, _beatsPerBar, _bpm;

        int previousMidiKey = 60; // Central C;
        int previousNoteAbsoluteTicks = 0;
        double percentageOfBarReached = 0;
        bool startedNoteIsClosed = true;

        public TrackHandler() { }

        public void Test(StringBuilder lilypondContent, int previousNoteAbsoluteTicks, MidiEvent midiEvent, int division, int _beatNote, int _beatsPerBar, double percentageOfBarReached)
        {
            // Finish the last notelength.
            double percentageOfBar;
            lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, _beatNote, _beatsPerBar, out percentageOfBar));

            // Second call
            //if() {
            //}

            lilypondContent.Append(" ");

            percentageOfBarReached += percentageOfBar;
            if (percentageOfBarReached >= 1)
            {
                lilypondContent.AppendLine("|");
                percentageOfBar = percentageOfBar - 1;
            }
        }


        // Different methods for different messagetypes, and if statement in musicloader to call these?

        public void LoadMidi(Sequence sequence, int i, StringBuilder lilypondContent)
        {
            Track track = sequence[i];
            int division = sequence.Division;

            foreach (var midiEvent in track.Iterator())
            {
                IMidiMessage midiMessage = midiEvent.MidiMessage;

                //MidiLoader midiLoader = new MidiLoader();

                //midiLoader.LoadMidi(midiEvent);

                // TODO: Split this switch statements and create separate logic.
                // We want to split this so that we can expand our functionality later with new keywords for example.
                // Hint: Command pattern? Strategies? Factory method?
                switch (midiMessage.MessageType)
                {
                    case MessageType.Meta:
                        var metaMessage = midiMessage as MetaMessage;
                        switch (metaMessage.MetaType)
                        {
                            case MetaType.TimeSignature:
                                byte[] timeSignatureBytes = metaMessage.GetBytes();
                                _beatNote = timeSignatureBytes[0];
                                _beatsPerBar = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));
                                lilypondContent.AppendLine($"\\time {_beatNote}/{_beatsPerBar}");
                                break;
                            case MetaType.Tempo:
                                byte[] tempoBytes = metaMessage.GetBytes();
                                int tempo = (tempoBytes[0] & 0xff) << 16 | (tempoBytes[1] & 0xff) << 8 | (tempoBytes[2] & 0xff);
                                _bpm = 60000000 / tempo;
                                lilypondContent.AppendLine($"\\tempo 4={_bpm}");
                                break;
                            case MetaType.EndOfTrack:
                                if (previousNoteAbsoluteTicks > 0)
                                {
                                    // There was duplicate code here so call this method to do that instead.
                                    HandleNoteLength(false, lilypondContent, previousNoteAbsoluteTicks, midiEvent, division, percentageOfBarReached, startedNoteIsClosed);
                                }
                                break;
                            default: break;
                        }
                        break;
                    case MessageType.Channel:
                        var channelMessage = midiEvent.MidiMessage as ChannelMessage;
                        if (channelMessage.Command == ChannelCommand.NoteOn)
                        {
                            if (channelMessage.Data2 > 0) // Data2 = loudness
                            {
                                // Append the new note.
                                lilypondContent.Append(MidiToLilyHelper.GetLilyNoteName(previousMidiKey, channelMessage.Data1));

                                previousMidiKey = channelMessage.Data1;
                                startedNoteIsClosed = false;
                            }
                            else if (!startedNoteIsClosed)
                            {
                                // There was duplicate code here so call this method to do that instead.
                                HandleNoteLength(true, lilypondContent, previousNoteAbsoluteTicks, midiEvent, division, percentageOfBarReached, startedNoteIsClosed);
                                previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;
                                startedNoteIsClosed = true;
                            }
                            else
                            {
                                lilypondContent.Append("r");
                            }
                        }
                        break;
                }
            }
        }

        // There is duplicate code at the above method, so call this method for that instead.
        public void HandleNoteLength(bool lastNote, StringBuilder lilypondContent, int previousNoteAbsoluteTicks, MidiEvent midiEvent, int division, double percentageOfBarReached, bool startedNoteIsClosed)
        {
            double percentageOfBar;
            lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, _beatNote, _beatsPerBar, out percentageOfBar));

            lilypondContent.Append(" ");

            percentageOfBarReached += percentageOfBar;
            if (percentageOfBarReached >= 1)
            {
                lilypondContent.AppendLine("|");
                if (lastNote == true)
                {
                    percentageOfBarReached -= 1;
                }
                else
                {
                    percentageOfBar = percentageOfBar - 1;
                }
            }
        }
    }
}
