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
                    Name = "�ҵ��ղ�",
                    Type = "01",
                    Items = Enumerable.Range(1, 12).Select(i => new MenuItem {Name = $"�˵��� - {i:00}"})
                },
                new MenuItem
                {
                    Code = "020000",
                    Name = "���۵�����",
                    Type = "01",
                    Items = new []
                    {
                        new MenuItem { Name = "���۵�", Code = "010001", Items = new MenuItem [0], Type = "02"},
                        new MenuItem { Name = "���۵���ѯ", Code = "010002", Items = new MenuItem [0], Type = "03"},
                        new MenuItem { Name = "���۵���ϸ��ѯ", Code = "010003", Items = new MenuItem [0], Type = "03"}
                    } 
                },
                new MenuItem
                {
                    Code = "030000",
                    Name = "�������",
                    Type = "01",
                    Items = Enumerable.Range(1, 16).Select(i => new MenuItem {Name = $"�˵��� - {i:00}"})
                },
                new MenuItem
                {
                    Code = "040000",
                    Name = "��Ʊ����",
                    Type = "01",
                    Items = Enumerable.Range(1, 22).Select(i => new MenuItem {Name = $"�˵��� - {i:00}"})
                },
                new MenuItem
                {
                    Code = "050000",
                    Name = "�������",
                    Type = "01",
                    Items = Enumerable.Range(1, 11).Select(i => new MenuItem {Name = $"�˵��� - {i:00}"})
                },
                new MenuItem
                {
                    Code = "060000",
                    Name = "��Ʒ����",
                    Type = "01",
                    Items = Enumerable.Range(1, 10).Select(i => new MenuItem {Name = $"�˵��� - {i:00}"})
                },
                new MenuItem
                {
                    Code = "070000",
                    Name = "�ͻ�����",
                    Type = "01",
                    Items = Enumerable.Range(1, 5).Select(i => new MenuItem {Name = $"�˵��� - {i:00}"})
                },
                new MenuItem
                {
                    Code = "080000",
                    Name = "ϵͳ����",
                    Type = "01",
                    Items = Enumerable.Range(1, 7).Select(i => new MenuItem {Name = $"�˵��� - {i:00}"})
                }
            };
        }
    }
}