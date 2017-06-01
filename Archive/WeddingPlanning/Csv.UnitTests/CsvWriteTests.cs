using System;
using NUnit.Framework;
using Csv;
using System.Collections.Generic;

namespace Csv.UnitTests
{
    [TestFixture]
    public class TestParseRecord
    {
        [TestCase("foo")]
        [TestCase("string with random % in it")]
        [TestCase("")]
        public void TestParseRecord_RecordIsPlainString_ReturnsStringUnchanged(string record)
        {
            var output = CsvWriter.ParseRecord(record);

            Assert.AreEqual(record, output);
        }


        [TestCase("test, case", "\"test, case\"")]
        [TestCase("test \" case", "\"test \"\" case\"")]
        [TestCase("with , and \" in", "\"with , and \"\" in\"")]
        [TestCase("\"\"", "\"\"\"\"\"\"")]
        [TestCase(",,,", "\",,,\"")]
        public void TestParseRecord_RecordContainsCommasAndDoubleQuotes_ReturnsStringSurroundedByDoubleQuotesWithDoubleQuotesEscaped(string input, string expected)
        {
            var output = CsvWriter.ParseRecord(input);

            Assert.AreEqual(expected, output);
        }

        [Test]
        public void TestParseRecord_InputIsNull_ReturnsNull()
        {
            var output = CsvWriter.ParseRecord(null);

            Assert.IsNull(output);
        }
    }

    [TestFixture]
    public class TestParseLine
    {
        [Test]
        public void TestParseLine_WithSingleRecord_ReturnsRecordValueAsString()
        {
            var recordValue = "foo";
            var records = new List<string> { recordValue };
            var output = CsvWriter.ParseLine(records);

            Assert.AreEqual(recordValue, output);
        }

        [Test]
        public void TestParseLine_WithMultipleRecords_ReturnsRecordsAsCommaSeparatedString()
        {
            var recordValue = "foo";
            var recordValue2 = "bar";
            var records = new List<string> { recordValue, recordValue2 };
            var output = CsvWriter.ParseLine(records);

            Assert.AreEqual($"{recordValue},{recordValue2}", output);
        }

        [TestCase("foo", "ba,r", "foo,\"ba,r\"")]
        [TestCase("f,oo", "ba,r", "\"f,oo\",\"ba,r\"")]
        [TestCase("f\"oo", "bar", "\"f\"\"oo\",bar")]
        [TestCase("f\"oo", "ba\"r", "\"f\"\"oo\",\"ba\"\"r\"")]
        [TestCase("f\"oo", "ba,r", "\"f\"\"oo\",\"ba,r\"")]
        [TestCase("f\"o,,o", "b\"\"a,r", "\"f\"\"o,,o\",\"b\"\"\"\"a,r\"")]
        public void TestParseLine_WithMultipleRecordsAndCommasAndDoubleQuotes_ReturnsRecordsAsEscapedCommaSeparatedString(string recordValue, string recordValue2, string expectedOutput)
        {
            var records = new List<string> { recordValue, recordValue2 };
            var output = CsvWriter.ParseLine(records);

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
