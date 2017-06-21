using System.Collections.Generic;
using System.Linq;

namespace boss.client.win
{
    internal class RemoteApplicationService : IApplicationService
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            // TODO: implement it
            return new MenuItem[]
            {
                new MenuItem
                {
                    Code = "010000",
                    Name = "我的收藏",
                    Type = "01",
                    Items = Enumerable.Range(1, 12).Select(i => new MenuItem {Name = $"菜单项 - {i:00}"})
                },
                new MenuItem
                {
                    Code = "020000",
                    Name = "销售单管理",
                    Type = "01",
                    Items = new []
                    {
                        new MenuItem { Name = "销售单", Code = "010001", Items = new MenuItem [0], Type = "02"},
                        new MenuItem { Name = "销售单查询", Code = "010002", Items = new MenuItem [0], Type = "03"},
                        new MenuItem { Name = "销售单明细查询", Code = "010003", Items = new MenuItem [0], Type = "03"}
                    } 
                },
                new MenuItem
                {
                    Code = "030000",
                    Name = "款项管理",
                    Type = "01",
                    Items = Enumerable.Range(1, 16).Select(i => new MenuItem {Name = $"菜单项 - {i:00}"})
                },
                new MenuItem
                {
                    Code = "040000",
                    Name = "发票管理",
                    Type = "01",
                    Items = Enumerable.Range(1, 22).Select(i => new MenuItem {Name = $"菜单项 - {i:00}"})
                },
                new MenuItem
                {
                    Code = "050000",
                    Name = "服务管理",
                    Type = "01",
                    Items = Enumerable.Range(1, 11).Select(i => new MenuItem {Name = $"菜单项 - {i:00}"})
                },
                new MenuItem
                {
                    Code = "060000",
                    Name = "产品管理",
                    Type = "01",
                    Items = Enumerable.Range(1, 10).Select(i => new MenuItem {Name = $"菜单项 - {i:00}"})
                },
                new MenuItem
                {
                    Code = "070000",
                    Name = "客户管理",
                    Type = "01",
                    Items = Enumerable.Range(1, 5).Select(i => new MenuItem {Name = $"菜单项 - {i:00}"})
                },
                new MenuItem
                {
                    Code = "080000",
                    Name = "系统管理",
                    Type = "01",
                    Items = Enumerable.Range(1, 7).Select(i => new MenuItem {Name = $"菜单项 - {i:00}"})
                }
            };
        }
    }
}