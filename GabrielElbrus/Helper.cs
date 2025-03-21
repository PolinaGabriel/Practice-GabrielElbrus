using GabrielElbrus.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielElbrus
{
    public static class Helper
    {
        public static readonly PostgresContext Database = new PostgresContext();
    }
}
