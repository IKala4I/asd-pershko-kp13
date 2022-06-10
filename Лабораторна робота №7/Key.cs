namespace ASD_Lab7__2
{
    internal class Key
    {
        public int key;
        public Key(string Value)
        {
            key = Convert.ToInt32(Value);
        }
        public override string ToString() => $"{key}";
    }
}
