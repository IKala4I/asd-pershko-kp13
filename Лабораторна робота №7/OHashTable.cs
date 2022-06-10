using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD_Lab7__2
{
    internal class OHashTable
    {
        private Value[] table;
        private int[] keys;
        private int size;
        public OHashTable()
        {
            size = 0;
            table = new Value[3];
            keys = new int[3];
        }
        public string Loadness
        {
            get { return $"Loadness: [{(double)size / table.Length * 100}%] [{size}/{table.Length}]"; }
        }
        public void InsertEntry(Entry entry)
        {
            Key key = entry.key;
            Value value = entry.value;
            
            if ((double)size / table.Length >= 0.6)
                Rehashing(ref table, ref keys, 2);
            

            int hash = GetHash(keys, table, key.key, 0, "insert");

            table[hash] = value;

            keys[hash] = key.key;

            size++;

            Console.WriteLine($"Added: deliveryID: {key};  Delivery: {table[hash]}");
        }
        public void RemoveEntry(Key key)
        {
            int hash = GetHash(keys, table, key.key, 0, "find");

            if (hash == -1)
                Console.WriteLine($"Cant find deliveryID: {key}");

            else
            {
                Key _key = key;
                Value _value = table[hash];

                table[hash] = default;
                keys[hash] = default;

                size--;

                Console.WriteLine($"Removed: deliveryID: {_key}; Delivery: {_value}");
            }
        }
        public string FindEntry(Key key)
        {
            int hash = GetHash(keys, table, key.key, 0, "find");

            if (hash == -1)
                return $"Cant find deliveryID: {key}";

            else
                return $"Find: deliveryID: {key}; Delivery: {table[hash]}";
        }
        private int GetHash(int[] keytable, Value[] table, int key, int i, string type)
        {
            if (i == keytable.Length)
                return -1;

            int hash = (key + i) % keytable.Length;

            if (type == "insert")
            {
                if (keytable[hash] == 0)
                    return hash;
            }
            else if (type == "find")
            {
                if (keytable[0] == key)
                    return hash;

                else if (keytable[hash] == key)
                    return hash;
            }
            else if (type == "print")
            { 
                if (keytable[hash] == key)
                    return hash;
            }
            return GetHash(keytable, table, key, i + 1, type);
        }
        private void Rehashing(ref Value[] table, ref int[] keys, int val)
        {
            if (val == 1)
                Console.WriteLine("\n____________________________________________\n     PreRehashing");

            else
                Console.WriteLine("\n____________________________________________");
            
            Console.WriteLine(Loadness);
            Console.WriteLine("      ---Rehashing---");

            Value[] newtable = new Value[table.Length * val];

            int[] newkeystable = new int[keys.Length * val];
            int count = 0;

            foreach (int i in keys)
            {
                if (i != 0)
                {
                    int hash = GetHash(newkeystable, newtable, i, 0, "insert");

                    newkeystable[hash] = i;

                    newtable[hash] = table[count];

                    Console.WriteLine($"deliveryID: {i}, Hash: {hash}");
                }
                count++;
            }

            Console.WriteLine("____________________________________________\n");

            table = newtable;

            keys = newkeystable;
        }
        public string HashCode(Key key) => $"Hash: deliveryID: {key}; Hash: {GetHash(keys, table, key.key, 0, "find")}";

        public void Print()
        {
            Rehashing(ref table, ref keys, 1);

            Console.WriteLine("---------------------TABLE---------------------");
         
            foreach (int i in keys)
            {
                int hash = GetHash(keys, table, i, 0, "print");
                if (i != default)
                    Console.WriteLine($"deliveryID: {i}; Delivery: {table[hash]}");
            }
         
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
