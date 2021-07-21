using System;
using System.Collections.Generic;

namespace Movie_Profanity_Remover_2._0
{
    public class Subtitle
    {
        public TimeSpan Start;
        public TimeSpan End;

        public bool RemoveFlag = false;
        public List<KeyValuePair<TimeSpan, TimeSpan>> Remove = new List<KeyValuePair<TimeSpan, TimeSpan>>();

        public string Text = "";
    }
}
