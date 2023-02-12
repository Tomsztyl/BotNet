using MySql.Data.MySqlClient;

namespace BotAssistant_Net.Code.Core.MySQL
{
    public class DataManager
    {
        public static MySqlConnection MySqlConnection = new MySqlConnection();

        public void Initialize()
        {
            GetMySqlConnection();
        }

        public void UnInitialize()
        {

        }

        private MySqlConnection GetMySqlConnection()
        {
            MysqlProperties mysqlProperties = BotController.BotPropertiesData.MysqlProperties;
            MySqlConnection = new MySqlConnection
                (
                    $"server={mysqlProperties.Server} " +
                    $"database={mysqlProperties.Database} " +
                    $"Uid={mysqlProperties.Uid} " +
                    $"password={mysqlProperties.Password} "
                );
            return MySqlConnection;
        }

        private void SQLToBase( string sql, ref bool hasRow )
        {
            MySqlCommand mySqlCommand = new MySqlCommand( sql, MySqlConnection );
            mySqlCommand.CommandTimeout = 60;
            try
            {
                MySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                if( mySqlDataReader.HasRows )
                {
                    Debuger.PrintLog( "Print your result" );
                }
                else
                {
                    Debuger.PrintLog( "Succesful sql!", ETypeLog.Succes );
                }
                hasRow = mySqlDataReader.HasRows;
                MySqlConnection.Close();
            }
            catch( Exception e )
            {
                Debuger.PrintLog( string.Format( "Error sql: {0}", e.Message ) );
            }
            MySqlConnection.Close();
        }

        private void SQLToBase( string sql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand( sql, MySqlConnection );
            mySqlCommand.CommandTimeout = 60;
            try
            {
                MySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                if( mySqlDataReader.HasRows )
                {
                    Debuger.PrintLog( "Print your result" );
                }
                else
                {
                    Debuger.PrintLog( "Succesful sql!", ETypeLog.Succes );
                }
                MySqlConnection.Close();
            }
            catch( Exception e )
            {
                Debuger.PrintLog( string.Format( "Error sql: {0}", e.Message ) );
            }
            MySqlConnection.Close();
        }

        #region USER DATA

        public void SetTeacherUser( string username )
        {
            string sql = string.Format( "SELECT `usersID`, `username` FROM `userteacher` WHERE username = '{0}'", username );
            bool hasRow = false;
            SQLToBase( sql,ref hasRow );
            if( !hasRow )
            {
                string insertSQL = string.Format( "INSERT INTO `userteacher`(`username`,`firstMarkDate`) VALUES ('{0}','{1}')", username,DateTime.Now );
                SQLToBase( insertSQL );
            }
        }

        #endregion
    }
}
