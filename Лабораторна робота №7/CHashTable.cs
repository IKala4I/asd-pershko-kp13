using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD_Lab7__2
{
    internal class CHashTable
    {
        private string[] couriers;
        private List<int>[] delivers;
        private int size;
        public CHashTable()
        {
            couriers = new string[2];
            delivers = new List<int>[2];

            for (int i = 0; i < delivers.Length; i++)
                delivers[i] = new List<int>();

            size = 0;
        }
        public void InsertEntry(string courier, int deliverID)
        {
           
            if ((double)size / delivers.Length >= 0.6)
                Rehashing(ref delivers, ref couriers);
            
            int hash = GetHash(couriers, delivers, ConvertString(courier), 0, "insert");

            delivers[hash].Add(deliverID);

            couriers[hash] = courier;
        }
        public void RemoveEntry(int deliverID)
        {
            foreach (List<int> i in delivers)
                i.Remove(deliverID);
        }
        private void Rehashing(ref List<int>[] delivers, ref string[] couriers)
        {
            List<int>[] newtable = new List<int>[delivers.Length * 2];

            for (int i = 0; i < newtable.Length; i++)
                newtable[i] = new List<int>();
           
            string[] newcouriers = new string[couriers.Length * 2];
            
            int count = 0;

            foreach (string i in couriers)
            {
                if (i != default)
                {
                    int hash = GetHash(newcouriers, newtable, ConvertString(i), 0, "rehashing");

                    newcouriers[hash] = i;

                    newtable[hash] = delivers[count];
                }
                count++;
            }

            delivers = newtable;
            couriers = newcouriers;
        }
        private int GetHash(string[] keytable, List<int>[] table, ulong key, ulong i, string type)
        {

            int hash = (int)((key + i) % Convert.ToUInt64(keytable.Length));

            if (type == "insert")
            {
                if (keytable[hash] == default)
                {
                    size++;
                    return hash;
                }

                else if (key == ConvertString(keytable[hash]))
                    return hash;
            }
            else if (type == "rehashing")
            {
                if (keytable[hash] == default)
                    return hash;

                else if (key == ConvertString(keytable[hash]))
                    return hash;
            }
            return GetHash(keytable, table, key, i + 1, type);
        }
        private ulong ConvertString(string courier)
        {
            if (courier != null)
            {
                string val = courier.ToLower();

                ulong s = 0;

                char[] a = val.ToCharArray();

                for (int i = 0; i < a.Length; i++)
                    s += (ulong)(a[i] * Math.Pow(27, a.Length - i - 1));
                
                return s;
            }
            return 0;
        }
        public int[] GetCourierOrders(string courier)
        {
            int[] orders = Array.Empty<int>();

            int count = 0;

            if (delivers.Length == 0)
                return null;
            
            foreach (int i in delivers[GetHash(couriers, delivers, ConvertString(courier), 0, "insert")])
            {
                Array.Resize(ref orders, orders.Length + 1);
                orders[count] = i;
                count++;
            }
            return orders;
        }
        public bool FindCourier(string courier)
        {
            foreach (string i in couriers)
                if (i == courier)
                    return true;

            return false;
        }
        public string[] GetCouriersArray() => couriers;
    }
}
