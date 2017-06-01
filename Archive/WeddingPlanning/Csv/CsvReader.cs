using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Csv
{
    public class CsvReader: StreamReader

    {

        private char _RecordDelimitter;
        private char _QuoteChar;
        private string _LineDelimitter;

        public CsvReader(string pathnameIn)
            : base(pathnameIn)
        {
         _RecordDelimitter = ',';
         _QuoteChar = '"';
         _LineDelimitter = Environment.NewLine;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathnameIn"></param>
        /// <param name="recordDelimitter"></param>
        /// <param name="quoteChar"></param>
        /// <param name="lineDelimitter">line delimiter must be at most two characters</param>
        public CsvReader(string pathnameIn, char recordDelimitter, char quoteChar, string lineDelimitter)
            : base(pathnameIn)
        {
         _RecordDelimitter = recordDelimitter;
         _QuoteChar = quoteChar;
            if(lineDelimitter.Length > 2)
            {
                throw new ArgumentException("line delimiter can be at most 2 characters in length");
            }
         _LineDelimitter = lineDelimitter;
        }

        ~CsvReader()
        {
            this.Close();
            this.Dispose();
        }

        public List<string> GetLine()
        {
            List<string> output = new List<string>();
            char c = new char();
            StringBuilder sb = new StringBuilder();

            while (!IsEndOfLine(this, out c) && !EndOfStream)
            {
                if (c == _RecordDelimitter)
                {
                    output.Add(sb.ToString());
                    sb = new StringBuilder();
                }

                else if (c == _QuoteChar)
                {
                    c = (char)this.Read();

                    if (this.Peek() == _QuoteChar)
                    {
                        sb.Append(c);
                        c = (char)this.Read();
                    }

                    while  (!(c == _QuoteChar && this.Peek() != _QuoteChar))
                        //(c != _QuoteChar && this.Peek() != _QuoteChar)
                    {
                        if (c == _QuoteChar && this.Peek() == _QuoteChar)
                        {
                            c = (char)this.Read();
                        }

                        sb.Append(c);
                        c = (char)this.Read();
                    }
                }

                else
                {
                    sb.Append(c);
                }
            }
            output.Add(sb.ToString());
            // If for example it's a windows line ending consume the \r as well as the \n
            if(_LineDelimitter.Length == 2)
            {
                this.Read();
            }

            return output;
        }

        private bool IsEndOfLine(CsvReader input, out char nextChar)
        {
            nextChar = (char)input.Read();

            return nextChar == _LineDelimitter[0]
                && (_LineDelimitter.Length == 1 || (char)input.Peek() == _LineDelimitter[1]);
        }
    }
}
