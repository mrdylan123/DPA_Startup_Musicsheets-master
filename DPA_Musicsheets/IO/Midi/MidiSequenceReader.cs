﻿using System;
using System.Text;
using DPA_Musicsheets.Entities;
using DPA_Musicsheets.Managers;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.IO.Midi
{
    public class MidiSequenceReader : SequenceReader
    {
        public MidiSequenceReader(string fileName)
        {
            Sequence midiSequence = new Sequence();
            midiSequence.Load(fileName);

            int division = midiSequence.Division;
            INote previousNote = null;
            int previousNoteAbsoluteTicks = 0;
            double percentageOfBarReached = 0;
            bool startedNoteIsClosed = true;
            TimeSignature previousTimeSignature = null;

            Sequence.Symbols.Add(new Clef(ClefType.GClef));

            foreach (Track track in midiSequence)
            {
                foreach (MidiEvent midiEvent in track.Iterator())
                {
                    IMidiMessage midiMessage = midiEvent.MidiMessage;
                    switch (midiMessage.MessageType)
                    {
                        case MessageType.Meta:
                            MetaMessage metaMessage = midiMessage as MetaMessage;
                            switch (metaMessage.MetaType)
                            {
                                case MetaType.TimeSignature:
                                    ITimeSignatureEvent timeSignatureEvent = new MidiTimeSignatureEvent(metaMessage);
                                    Sequence.Symbols.Add(timeSignatureEvent.TimeSignature);
                                    previousTimeSignature = timeSignatureEvent.TimeSignature;
                                    break;
                                case MetaType.Tempo:
                                    ITempoEvent tempoEvent = new MidiTempoEvent(metaMessage);
                                    Sequence.BeatsPerMinute = tempoEvent.BeatsPerMinute;
                                    break;
                                case MetaType.EndOfTrack:
                                    break;
                            }
                            break;
                        case MessageType.Channel:
                            ChannelMessage channelMessage = midiEvent.MidiMessage as ChannelMessage;
                            if (channelMessage.Command == ChannelCommand.NoteOn)
                            {
                                INoteEvent noteEvent = new MidiNoteEvent(channelMessage);
                                INote note = noteEvent.Note;

                                if (note != null)
                                {
                                    Sequence.Symbols.Add(note);

                                    previousNote = note;
                                    startedNoteIsClosed = false;
                                }
                                else
                                {
                                    // Previous note must be closed
                                    IBarLineEvent barLineEvent = new MidiBarLineEvent(
                                        midiEvent, previousNote, ref startedNoteIsClosed,
                                        ref previousNoteAbsoluteTicks, division, previousTimeSignature,
                                        ref percentageOfBarReached);

                                    if (barLineEvent.BarLine != null) // End of bar has been reached, start a new one
                                    {
                                        Sequence.Symbols.Add(barLineEvent.BarLine);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
