using Sber_Kvit.BLL.BaseFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.Barcode
{
    interface IBaseKindergardenHandler
    {
        List<KindergardenDataSet> Go();
    }
}
