using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace StopWastingMyTime.Models.Data
{
    public class BigBallOfSql
    {
        #region Fields

        private List<FileInfo> _sqlFiles = null;
        private bool _suppressMissingFileErrors = false;
        private List<object> _parameters = null;

        #endregion

        #region Properties

        public bool SuppressMissingFileErrors
        {
            get { return _suppressMissingFileErrors; }
            set { _suppressMissingFileErrors = value; }
        }

        public List<object> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        #endregion

        #region Constructors

        private BigBallOfSql()
        {
            _parameters = new List<object>();
        }

        public BigBallOfSql(IEnumerable<string> sqlFilenames) : this()
        {
            foreach (string sqlFilename in sqlFilenames)
                _sqlFiles.Add(new FileInfo(sqlFilename));
        }

        public BigBallOfSql(IEnumerable<FileInfo> sqlFiles) : this()
        {
            _sqlFiles = new List<FileInfo>(sqlFiles);
        }

        #endregion

        #region Methods

        public void Run()
        {
            SqlCommand command = null;
            foreach (FileInfo sqlFile in _sqlFiles)
            {
                if (sqlFile.Exists)
                {
                    StreamReader reader = sqlFile.OpenText();
                    string sql = reader.ReadToEnd();
                    reader.Close();

                    for (int i = 0; i < _parameters.Count; i++)
                        sql = sql.Replace("{" + i.ToString() + "}", _parameters[i].ToString());

                    string[] scripts = sql.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string script in scripts)
                    {
                        command = new SqlCommand(script);
                        DataUtils.RunAdhocNonQuery(command);
                    }
                }
                else if (!SuppressMissingFileErrors)
                {
                    throw new FileNotFoundException("SQL file batch runner unable to find a supplied file", sqlFile.FullName);
                }
            }
        }

        #endregion
    }
}
