namespace ASD_Lab7__2
{
    internal class Address
    {
        public string street;
        public int houseNumber;
        public int appartmentNumber;

        public Address(string[] partsofstreet)
        {
            street = partsofstreet[0];
            houseNumber = Convert.ToInt32(partsofstreet[1]);
            appartmentNumber = Convert.ToInt32(partsofstreet[2]);
        }
        public override string ToString() => $"{street},{houseNumber},{appartmentNumber}";
    }
}
