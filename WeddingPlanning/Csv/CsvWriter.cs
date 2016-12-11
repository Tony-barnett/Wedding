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

        public string ParseRecord(string record)
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

            return record;
        }

        public string ParseLine(List<string> line)
        {
            for(int i = 0; i < line.Count; i++)
            {
                line[i] = ParseRecord(line[i]);
            }
            return string.Join(",", line);
        }

        public void WriteLine(List<string> line)
        {
            var parsedLine = ParseLine(line);

            this.WriteLine(parsedLine);
        }
    }
}
