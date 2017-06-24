using System.Collections.Generic;

namespace boss.client.win
{
    public interface IApplicationService
    {
        IEnumerable<MenuItem> GetMenuItems();
        MenuItem GetWorkbenchMenuItem();
    }
}