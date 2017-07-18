using System.Collections.Generic;
using System.Linq;

namespace boss.client.win
{
    [Service]
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
                    Items = new []
                    {
                        new MenuItem { Name = "�ҵĹ���̨", Code = "000000", Items = new MenuItem [0], Type = "02"},
                    }
                },
                new MenuItem
                {
                    Code = "020000",
                    Name = "���۵�����",
                    Type = "01",
                    Items = new []
                    {
                        new MenuItem { Name = "���۵�", Code = "010001", Items = new MenuItem [0], Type = "02"},
                        new MenuItem { Name = "���۵���ѯ", Code = "010001", Items = new MenuItem [0], Type = "03"},
                        new MenuItem { Name = "���۵���ϸ��ѯ", Code = "010003", Items = new MenuItem [0], Type = "03"}
                    }
                },
                new MenuItem
                {
                    Code = "030000",
                    Name = "�������",
                    Type = "01",
                    Items = new []
                    {
                        new MenuItem { Name = "�����嵥", Code = "020001", Items = new MenuItem [0], Type = "02"},
                        new MenuItem { Name = "�����嵥��ѯ", Code = "020001", Items = new MenuItem [0], Type = "03"},
                        new MenuItem { Name = "������Ϣ��ѯ", Code = "020002", Items = new MenuItem [0], Type = "03"},
                        new MenuItem { Name = "����������ѯ", Code = "020003", Items = new MenuItem [0], Type = "03"}
                    }
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
                    Items = new []
                    {
                        new MenuItem { Name = "������ƾ֤", Code = "040001", Items = new MenuItem [0], Type = "02"},
                        new MenuItem { Name = "������ƾ֤��ѯ", Code = "040001", Items = new MenuItem [0], Type = "03"},
                        new MenuItem { Name = "��������ϸ��ѯ", Code = "040003", Items = new MenuItem [0], Type = "03"},
                        new MenuItem { Name = "�ͻ������ѯ", Code = "040002", Items = new MenuItem [0], Type = "03"}
                    }
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

        public MenuItem GetWorkbenchMenuItem()
        {
            // TODO: implement it
            return new MenuItem() { Code = "000000", Name = "�ҵĹ���̨", Type = "02" };
        }
    }
}