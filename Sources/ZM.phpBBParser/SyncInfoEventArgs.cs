using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZM.phpBBParser
{
    public class ProgressEventArgs : EventArgs
    {
        public ProgressEventArgs() : base()
        {

        }

        public virtual string Message { get; set; }

        public virtual int Value { get; set; }

        public virtual int MaximumValue { get; set; }
    }
}
