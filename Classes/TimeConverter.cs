using BerlinClock.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            StringBuilder result = new StringBuilder();
            string[] timeParts = aTime.Split(':');

            if (timeParts.Length != 3)
                throw new ArgumentException($"{aTime} is not a valid argument, the format should look like: HH:mm:ss");

            int hour = int.Parse(timeParts[0]);
            int min = int.Parse(timeParts[1]);
            int sec = int.Parse(timeParts[2]);

            generateFirstRow(result, sec);
            generateSecondRow(result, hour);
            generateThirdRow(result, hour);
            generateFourthRow(result, min);
            generateFifthRow(result, min);

            return result.ToString();
        }

        private void generateFourthRow(StringBuilder result, int min)
        {
            int activeLamps = min / 5;
            for (int i = 0; i < activeLamps / 3; i++)
                result.Append("YYR");
            for (int i = 0; i < activeLamps % 3; i++)
                result.Append("Y");

            result.AppendLine(new string(Constants.Blank, 11 - activeLamps));
        }

        private void generateFifthRow(StringBuilder result, int min)
        {
            result.Append(new string(Constants.Yellow, min % 5));
            result.Append(new string(Constants.Blank, 4 - (min % 5)));
        }

        private void generateThirdRow(StringBuilder result, int hour)
        {
            result.Append(new string(Constants.Red, hour % 5));
            result.AppendLine(new string(Constants.Blank, 4 - (hour % 5)));
        }

        private void generateSecondRow(StringBuilder result, int hour)
        {
            result.Append(new string(Constants.Red, hour / 5));
            result.AppendLine(new string(Constants.Blank, 4 - (hour / 5)));
        }

        private void generateFirstRow(StringBuilder result, int sec)
        {
            if (sec % 2 == 1)
                result.AppendLine(Constants.Blank.ToString());
            else
                result.AppendLine(Constants.Yellow.ToString());
        }
    }
}
