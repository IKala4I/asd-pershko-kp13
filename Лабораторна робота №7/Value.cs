namespace ASD_Lab7__2
{
    internal class Value
    {
        public string objectType;
        public string courierName;
        public Address addressOfDelivery;
        public Date dateOfDelivery;

        public Value(string type, string name, Address address, Date date)
        {
            objectType = type;
            courierName = name;
            addressOfDelivery = address;
            dateOfDelivery = date;
        }
        public override string ToString() => $"[{objectType}],[{courierName}],[{addressOfDelivery}],[{dateOfDelivery}]";
    }
}
