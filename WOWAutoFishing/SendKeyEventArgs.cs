using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOWAutoFishing
{
    public class SendKeyEventArgs : EventArgs
    {
        public string Key { get; set; }

        public SendKeyEventArgs(string key) => Key = key;
    }
}
