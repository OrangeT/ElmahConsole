using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;
using elmahconsole.Controllers;
using elmahconsole.lib.Models;

namespace elmahconsole.lib.Sql
{
    public class ElmahSql : IDisposable
    {
        public string SEARCH_FIELDS = "errorId, application, host, type, source, message, [user], statusCode, timeUtc, allXml";

        public const int PAGE_SIZE = 20;

        private SqlConnection _sqlConnection;

        public ElmahSql(string connectionstring)
        {
            _sqlConnection = new SqlConnection(connectionstring);
        }

        public int TotalErrors()
        {
            using (var command = new SqlCommand(
                $"SELECT COUNT(*) FROM Elmah_Error", _sqlConnection
            ))
            {
                _sqlConnection.Open();

                var result = (int)command.ExecuteScalar(); // long?

                _sqlConnection.Close();

                return result;
            }
        }

        /* Return a paginated list of ELMAH Exceptions */
        public List<ElmahError> ErrorList(int page)
        {
            var list = new List<ElmahError>();

            var orderField = "TimeUtc";
            var orderDir = "DESC";

            using (var command =
                new SqlCommand(
                    $"SELECT {SEARCH_FIELDS} FROM ELMAH_Error ORDER BY {orderField} {orderDir} " +
                    $"OFFSET {page * PAGE_SIZE} ROWS FETCH NEXT {PAGE_SIZE} ROWS ONLY",
                    _sqlConnection
            ))
            {
                _sqlConnection.Open();

                //Set up serializer
                var serializer = new XmlSerializer(typeof(ElmahErrorXml));

                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        list.Add(new ElmahError
                        {
                            ErrorId = reader.GetGuid(reader.GetOrdinal("errorId")),
                            Application = reader.GetString(reader.GetOrdinal("application")),
                            Host = reader.GetString(reader.GetOrdinal("host")),
                            Type = reader.GetString(reader.GetOrdinal("type")),
                            Source = reader.GetString(reader.GetOrdinal("source")),
                            Message = reader.GetString(reader.GetOrdinal("message")),
                            User = reader.GetString(reader.GetOrdinal("user")),
                            StatusCode = reader.GetInt32(reader.GetOrdinal("statusCode")),
                            TimeUtc = reader.GetDateTime(reader.GetOrdinal("timeUtc")),
                            AllXml = reader.GetString(reader.GetOrdinal("allXml")),
                            DecodedXml = (ElmahErrorXml)serializer.Deserialize(
                                new StringReader(reader.GetString(reader.GetOrdinal("allXml")))) // Deserialize XML
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }

                _sqlConnection.Close();
            }

            return list;

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (_sqlConnection != null)
                    {
                        _sqlConnection.Dispose();
                        _sqlConnection = null;
                    }
                }
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ElmahSql() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}