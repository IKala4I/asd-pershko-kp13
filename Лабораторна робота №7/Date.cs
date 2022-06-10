namespace ASD_Lab7__2
{
    internal class Date
    {
        public int year;
        public string month;
        public int day;
        public Date(string[] parts)
        {
            year = Convert.ToInt32(parts[2]);
            month = parts[1];
            day = Convert.ToInt32(parts[0]);
        }
        public override string ToString() => $"{day}.{month}.{year}";
    }
}
