namespace boss.client.win
{
    [Service]
    public class RemoteQueryService : IQueryService
    {
        public QueryMeta GetQueryMeta(string code)
        {
            // TODO: implement it
            return new QueryMeta()
            {
                code = code,
                name = "客户服务查询",
                parameters = new[]
                {
                    new QueryParameter() { description = "客户编码", name = "customerCode", type = "02"},
                    new QueryParameter() { description = "客户", name = "customer", subQueryCode = "090101", type = "01" },
                    new QueryParameter() { description = "产品", name = "product", subQueryCode = "909201", type = "01" },
                    new QueryParameter() { description = "起始结束时间", name = "minEndDate", type = "04"},
                    new QueryParameter() { description = "截止结束时间", name = "maxEndDate", type = "04"}
                },
                fields = new[]
                {
                    new Field() { description = "序号", name = "rowNumber", type = "01" },
                    new Field() { description = "客户编码", name = "customerCode", type = "01" },
                    new Field() { description = "客户名称", name = "customerName", type = "01" },
                    new Field() { description = "产品编码", name = "productCode", type = "01" },
                    new Field() { description = "产品名称", name = "productName", type = "01" },
                    new Field() { description = "产品内部名称", name = "productInternalName", type = "01" },
                    new Field() { description = "服务开始时间", name = "serviceStartDate", type = "03" },
                    new Field() { description = "服务结束时间", name = "serviceEndDate", type = "03" },
                    new Field() { description = "组织编码", name = "organizationCode", type = "01" },
                    new Field() { description = "组织名称", name = "organizationName", type = "01" }
                }
            };
        }
    }
}