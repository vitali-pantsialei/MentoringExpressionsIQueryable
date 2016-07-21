using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sample03.E3SClient
{
	public class FTSRequestGenerator
	{
		private readonly UriTemplate FTSSearchTemplate = new UriTemplate(@"data/searchFts?metaType={metaType}&query={query}&fields={fields}");
		private readonly Uri BaseAddress;

		public FTSRequestGenerator(string baseAddres) : this(new Uri(baseAddres))
		{
		}

		public FTSRequestGenerator(Uri baseAddress)
		{
			BaseAddress = baseAddress;
		}

		public Uri GenerateRequestUrl<T>(string query = "*", int start = 0, int limit = 10)
		{
			return GenerateRequestUrl(typeof(T), query, start, limit);
		}

		public Uri GenerateRequestUrl(Type type, string query = "*", int start = 0, int limit = 10)
		{
			string metaTypeName = GetMetaTypeName(type);

            query = this.CheckQuery(query);

			var ftsQueryRequest = new FTSQueryRequest
			{
				Statements = new List<Statement>
				{
					new Statement {
						Query = query
					}
				},
				Start = start,
				Limit = limit
			};

			var ftsQueryRequestString = JsonConvert.SerializeObject(ftsQueryRequest);

			var uri = FTSSearchTemplate.BindByName(BaseAddress,
				new Dictionary<string, string>()
				{
					{ "metaType", metaTypeName },
					{ "query", ftsQueryRequestString }
				});

			return uri;
		}

		private string GetMetaTypeName(Type type)
		{
			var attributes = type.GetCustomAttributes(typeof(E3SMetaTypeAttribute), false);

			if (attributes.Length == 0)
				throw new Exception(string.Format("Entity {0} do not have attribute E3SMetaType", type.FullName));

			return ((E3SMetaTypeAttribute)attributes[0]).Name;
		}

        private string CheckQuery(string query)
        {
            StringBuilder resultQuery = new StringBuilder();
            string pattern = @"(\(.*\)):(.*)";
            MatchCollection matches = Regex.Matches(query, pattern);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    resultQuery.Append(match.Groups[2].Value + ":" + match.Groups[1].Value);
                }
                return resultQuery.ToString();
            }
            else
                return query;
        }
	}
}
