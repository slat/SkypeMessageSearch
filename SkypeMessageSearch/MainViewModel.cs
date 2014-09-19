using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace SkypeMessageSearch
{
    public class MainViewModel
    {
        // todo: Convert HTML to RTF or similar to improve presentation of variable messages. ie. quotes, hyperlinks.
        // https://www.google.co.nz/search?q=C%23+html+to+rich+text
        // http://www.modeltext.com/html/sample-output.aspx
        // todo: Sorting. Currently defaulted to Timestamp desc.
        // todo: Refresh Button - or poll message count and refresh. (ie. When new messages come in push to top of stack if ordered by Timestamp desc.)

        // todo: Probably not these as may not be achievable.
        // todo: Filtering highlight text in cell where filter matches. (Although probably complex with Any/And.)
        // https://www.google.co.nz/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=datagridview%20textbox%20column%20highlight
        // http://social.msdn.microsoft.com/Forums/vstudio/en-US/43f6b81f-4cb7-4e8e-bd29-e3645f200734/highlight-text-inside-a-datagridview-cell?forum=vbgeneral
        // todo: Alternate color for each convo_id. (May be a pain to implement as virtual scrolling is enabled - keeping track of unique groups is a pain)

        private string _connectionString;
        private MainView _view;
        public readonly Dictionary<int, object[]> Cache = new Dictionary<int, object[]>();
        private ICollection<SkypeContact> _contacts;
        private ICollection<SkypeParticipant> _participants;
        private long _ticks1970;
        private string _profileName;

        public string FilterMessage { get; set; }
        public bool FilterMessageAnd { get; set; }
        public DateTime? FilterFrom { get; set; }
        public DateTime? FilterTo { get; set; }
        public string FilterContact { get; set; }

        public void Ready(MainView view)
        {
            _view = view;
            FilterContact = "All";
            FilterMessage = "";
            FilterMessageAnd = true;
        }

        public void Load(string profileName, string profileDatabasePath)
        {
            _profileName = profileName;
            _connectionString = string.Format("Data Source={0};Version=3;Pooling=True;Max Pool Size=100;Read Only=True;FailIfMissing=True;Cache Size=30000000;", profileDatabasePath);
            _contacts = ContactsQuery();
            _participants = ParticipantsQuery();
            _view.LoadContacts(_contacts);
            Reload();
        }

        public void Reload()
        {
            if (_connectionString == null)
                return;

            var rowCount = QueryCount();
            _view.RowCount = rowCount;
            Cache.Clear();
            _view.DisplayFooter(rowCount);
        }

        public void LoadPage()
        {
            var records = Query();
            var index = _view.Offset;

            Cache.Clear();

            foreach (var x in records)
            {
                Cache.Add(index++, new object[]
                    {
                        x.Id,
                        x.Conversation,
                        x.Timestamp,
                        x.Author,
                        x.Text,
                    });
            }
        }

        public ICollection<SkypeParticipant> ParticipantsQuery()
        {
            using (var conn = GetConnection())
            {
                return conn.Query<SkypeParticipant>("SELECT convo_id AS ConvoId, identity AS Identity FROM Participants")
                    .ToArray();
            }
        }

        public ICollection<SkypeContact> ContactsQuery()
        {
            const string sql = @"
                SELECT C.skypename AS SkypeName, C.displayname AS Name
                FROM Messages M
                INNER JOIN Participants P ON P.convo_id = M.convo_id
                INNER JOIN Contacts C ON C.skypename = P.identity
                WHERE (M.type = 61 OR M.type = 68)
                AND C.skypename <> @ProfileName
                GROUP BY P.identity
                ORDER BY C.displayname
            ";

            using (var conn = GetConnection())
            {
                return conn.Query<SkypeContact>(sql, new
                {
                    ProfileName = _profileName
                }).ToArray();
            }
        }

        private class SkypeFilter
        {
            public DynamicParameters Parameters { get; private set; }
            public string Sql { get; set; }

            public SkypeFilter()
            {
                Sql = "";
                Parameters = new DynamicParameters();
            }
        }

        private string[] GetFilters()
        {
            var filters = FilterMessage.ToLower().Split(' ')
                .Where(x => !string.IsNullOrEmpty(x) && x != " ");
            return filters.ToArray();
        }

        private long Ticks1970
        {
            get
            {
                if (_ticks1970 == 0)
                    _ticks1970 = new DateTime(1970, 1, 1).Ticks;
                return _ticks1970;
            }
        }

        private SkypeFilter Filters()
        {
            var result = new SkypeFilter();

            if (!string.IsNullOrEmpty(FilterMessage))
            {
                var filters = GetFilters();
                var sql = new string[filters.Length];
                var count = 0;
                for (var i = 0; i < filters.Length; i++)
                {
                    var p = string.Format("@Body{0}", i);
                    sql[i] = string.Format("(M.body_xml LIKE {0})", p);
                    result.Parameters.Add(p, string.Format("%{0}%", filters[i]));
                    count++;
                }

                if (count > 0)
                    result.Sql = " AND " + string.Join(FilterMessageAnd ? " AND " : " OR ", sql);
            }

            if (FilterFrom.HasValue)
            {
                result.Sql += " AND M.timestamp >= @SecondsFrom";
                result.Parameters.Add("@SecondsFrom", FilterSeconds(FilterFrom.Value));
            }

            if (FilterTo.HasValue)
            {
                result.Sql += " AND M.timestamp <= @SecondsTo";
                result.Parameters.Add("@SecondsTo", FilterSeconds(FilterTo.Value, true));
            }

            if (!string.IsNullOrEmpty(FilterContact) && FilterContact != "All")
            {
                var contact = _contacts.FirstOrDefault(x => x.ToString() == FilterContact);
                if (contact != null)
                {
                    var convoIds = _participants
                        .Where(x => x.Identity == contact.SkypeName)
                        .Select(x => x.ConvoId)
                        .ToArray();

                    result.Sql += " AND M.convo_id IN @ConvoIds";
                    result.Parameters.Add("@ConvoIds", convoIds);
                }
            }

            return result;
        }

        private long FilterSeconds(DateTime dt, bool roundUp=false)
        {
            var date = dt.Date;
            if (roundUp)
                date = dt.Date.AddDays(1).AddSeconds(-1);

            var ticks = date.ToUniversalTime().Ticks - Ticks1970;
            var seconds = (long) new TimeSpan(ticks).TotalSeconds;
            return seconds;
        }

        private int QueryCount()
        {
            var filters = Filters();
            var sql = string.Format(@"
                SELECT COUNT(M.id) FROM Messages M
                WHERE (M.type = 61 OR M.type = 68)
                AND M.body_xml != ''
                {0}
            ", filters.Sql);

            using (var conn = GetConnection())
            {
                return conn.Query<int>(sql, filters.Parameters).FirstOrDefault();
            }
        }

        public ICollection<SkypeMessage> Query()
        {
            var filters = Filters();
            // Messages & Attachments only, Default to most recent first.
            var sql = string.Format(@"
                SELECT M.id AS Id, M.from_dispname AS Author, M.timestamp AS Seconds, M.body_xml AS Body, C.displayname AS Conversation
                FROM Messages M
                INNER JOIN Conversations C ON C.Id = M.convo_id
                WHERE (M.type = 61 OR M.type = 68)
                AND M.body_xml != ''
                {0}
                GROUP BY M.id
                ORDER BY M.timestamp DESC
                LIMIT @Offset, @Limit
            ", filters.Sql);

            var parameters = filters.Parameters;
            parameters.Add("@Limit", _view.Limit);
            parameters.Add("@Offset", _view.Offset);

            using (var conn = GetConnection())
            {
                return conn.Query<SkypeMessage>(sql, parameters).ToArray();
            }
        }

        private IDbConnection GetConnection(bool autoOpen=true)
        {
            var conn = new SQLiteConnection(_connectionString);
            if (autoOpen)
                conn.Open();
            return conn;
        }
    }
}
