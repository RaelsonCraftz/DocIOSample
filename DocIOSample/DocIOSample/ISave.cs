using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DocIOSample
{
    public interface ISave
    {

        Task Save(string filename, string contentType, MemoryStream stream);

    }
}
