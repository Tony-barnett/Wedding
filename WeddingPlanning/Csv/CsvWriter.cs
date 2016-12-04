using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Csv
{
    public class CsvWriter: StreamWriter
    {
        public CsvWriter(string filenameIn, bool append = true)
            : base(filenameIn, append: append)
        {
        }

        ~CsvWriter()
        {
            this.Flush();
            this.Close();
            this.Dispose();
        }

        public void ParseRecord(string record)
        {
            if (record.Contains('"'))
            {
                record.Replace("\"", "\"\"");
                record = "\"" + record + "\"";
            }

            else if(record.Contains(','))
            {
                record = "\"" + record + "\"";
            }

            this.Write(record);
        }

        public void ParseLine(List<string> line)
        {
            foreach (string record in line)
            {
                ParseRecord(record);
            }
            this.Write(Environment.NewLine);
        }
    }
}
