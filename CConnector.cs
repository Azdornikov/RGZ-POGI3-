using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGZ_POGI3_
{
    public abstract class CConnector
    {
        string path;

        public CConnector()
        {
        }

        public CConnector(string path)
        {
            this.path = path;
        }

        public void SetPath(string path)
        {
            this.path = path;
        }

        public string GetPath()
        {
            return path;
        }

        public abstract bool SaveData(CDataBase db);
        public abstract CDataBase LoadData();
    }
}
