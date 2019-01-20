﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace DPA_Musicsheets.Command_Pattern.KeyHandlers
{
    public interface IKeyHandlerChain
    {
        IKeyHandlerChain Next { get; set; }

        string Handle(List<Key> keys);
    }

    public abstract class KeyHandler : IKeyHandlerChain
    {
        protected static Dictionary<string, string> Commands = new Dictionary<string, string>
        {
            { "CTRL+S", "saveAsLily" },
            { "CTRL+S+P", "saveAsPdf" },
            { "CTRL+O", "openFile" },
            { "CTRL+L", "addClef" },
            { "CTRL+M", "addTempo" },
            { "CTRL+T", "add44Time" },
            { "CTRL+T+4", "add44Time" },
            { "CTRL+T+3", "add34Time" },
            { "CTRL+T+8", "add68Time" }
        };

        public IKeyHandlerChain Next { get; set; }

        public string Handle(List<Key> keys)
        {
            string keyCombo = TryHandle(keys);

            if (Commands.ContainsKey(keyCombo))
                return Commands[keyCombo]; // Found a valid command

            string nextKey = Next?.Handle(keys);

            if (string.IsNullOrEmpty(keyCombo))
                return nextKey;

            if (string.IsNullOrEmpty(nextKey))
            {
                return keyCombo;
            }
            else
            {
                if (Commands.ContainsKey($"{keyCombo}+{nextKey}"))
                    return Commands[$"{keyCombo}+{nextKey}"];

                return $"{keyCombo}+{nextKey}";
            }
        }

        protected abstract string TryHandle(List<Key> keys);
    }
}
