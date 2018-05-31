using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsCSharp
{
    /// <summary>
    /// Notify the program that consumes this class when things are broken or unbroken
    /// much like a program is notified of button clicks.    
    /// </summary>
    public class BrokenRules
    {
        public delegate void HandleBrokenRule(string fieldName);
        public delegate void HandleNoBrokenRules();

        public event HandleBrokenRule BrokenRule;
        public event HandleNoBrokenRules NoBrokenRules;

        private SortedList<string, bool> mRules;

        public BrokenRules()
        {
            mRules = new SortedList<string, bool>();
            BrokenRule += delegate(string fieldName) { };
            NoBrokenRules += delegate() { };
        }

        public void RuleBroken(string fieldName, bool isBroken)
        {
            try
            {
                if (isBroken)
                {
                    mRules.Add(fieldName, true);
                    BrokenRule(fieldName);
                }
                else
                {
                    mRules.Remove(fieldName);
                    if (mRules.Count == 0)
                        NoBrokenRules();
                }
            }
            catch (ArgumentException)
            {
                /*  An argument exception will get throw if the same rule is broken twice
                    It's not really an error so I let it go.
                    Other exceptions don't get handled
                */ 
            }
        }

        public int Count
        {
            get
            {
                return mRules.Count;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < mRules.Count; i++)
                mRules.RemoveAt(i);
        }
    }
}
