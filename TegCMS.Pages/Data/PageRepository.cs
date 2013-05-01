using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TegCMS.Pages.Data
{
    public interface IPageRepository
    {
        PageInformation GetForRouteNameAndHostName(string routeName, string hostName);
    }
}
