using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

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

        private void SQLToBase( string sql, ref bool hasRow, ref List<object> list, int countCell )
        {
            MySqlCommand mySqlCommand = new MySqlCommand( sql, MySqlConnection );
            mySqlCommand.CommandTimeout = 60;
            try
            {
                MySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                hasRow = mySqlDataReader.HasRows;

                if( mySqlDataReader.HasRows )
                {
                    Debuger.PrintLog( "Print your result" );
                    mySqlDataReader.Read();
                    object[] objs = new object[countCell];
                    int quant = mySqlDataReader.GetValues( objs );
                    for( int i = 0; i < quant; i++ )
                    {
                        list.Add( objs[i] );
                    }
                    mySqlDataReader.Close();
                }
                else
                {
                    Debuger.PrintLog( "Successful sql!", ETypeLog.Succes );
                }
            }
            catch( Exception e )
            {
                Debuger.PrintLog( string.Format( "Error sql: {0}", e.Message ) );
            }
            MySqlConnection.Close();
        }

        private void SQLToBase( string sql )
        {
            MySqlCommand mySqlCommand = new MySqlCommand( sql, MySqlConnection );
            mySqlCommand.CommandTimeout = 60;
            try
            {
                MySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                if( mySqlDataReader.HasRows )
                {
                    Debuger.PrintLog( "[DB] Print your result" );
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
            List<object> list = new List<object>();
            SQLToBase( sql, ref hasRow,ref list, 0 );
            if( !hasRow )
            {
                string insertSQL = string.Format( "INSERT INTO `userteacher`(`username`,`firstMarkDate`) VALUES ('{0}','{1}')", username, DateTime.Now );
                SQLToBase( insertSQL );
            }
        }

        private string GetTeacherUserID( string username )
        {
            string sql = string.Format( "SELECT `usersID` FROM `userteacher` WHERE username = '{0}'", username );
            bool hasRow = false;
            List<object> list = new List<object>();
            SQLToBase( sql, ref hasRow, ref list, 1 );
            if (hasRow)
            {
                return list[0].ToString() ;
            }
            return string.Empty;
        }

        public void InsertQuestion( string username, string question )
        {
            string usernameID = GetTeacherUserID( username );
            string insertSQL = string.Format( "INSERT INTO `questions`(`usersID`, `question`) VALUES ('{0}','{1}')", usernameID, question );
            SQLToBase( insertSQL );
        }

        #endregion
    }
}
