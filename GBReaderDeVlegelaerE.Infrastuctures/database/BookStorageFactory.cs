using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using GBReaderDeVlegelaerE.Domains;
using MySql.Data.MySqlClient;

namespace GBReaderDeVlegelaerE.Infrastuctures.database
{
    public class BookStorageFactory
    {
        private DbProviderFactory _factory;
        private string _connectionString;
        
        public BookStorageFactory(string providerName, string connectionString)
        {
            try
            {
                //_factory = DbProviderFactories.GetFactory(providerName);
                DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
                _factory = DbProviderFactories.GetFactory(providerName);
                _connectionString = connectionString;
            }
            catch (ArgumentException ex)
            {
                throw new ProviderNotFoundException($"Unable to load provider {providerName}", ex);
            }
        }
        
        public BookStorage NewStorage(LibraryBook libraryBook)
        {
            try
            {
                IDbConnection con = _factory.CreateConnection()!;
                con.ConnectionString = _connectionString;
                // if (con == null)
                // {
                //     // throw new UnableToConnectException("adadad");
                // }
                con.Open();
                return new BookStorage(con, libraryBook);
                //Si j'ai mis mon using ici, ça aurait fermé la connexion à la BD
            }
            catch (ArgumentException ex)
            {
                throw new InvalidConnectionStringException(ex);
            }
            catch (SqlException ex)
            {
                throw new UnableToConnectException(ex);
            }
            catch (DbException ex)
            {
                throw new UnableToConnectException(ex);
            }
        }
    }

    public class UnableToConnectException : Exception
    {
        public UnableToConnectException(Exception sqlException)
            : base("Unable to establish connection to db", sqlException)
        { }
    }

    public class ProviderNotFoundException : Exception
    {
        public ProviderNotFoundException(string s, Exception argumentException)
            :base(s, argumentException)
        {
        }
    }

    public class InvalidConnectionStringException : Exception
    {
        public InvalidConnectionStringException(Exception argumentException)
            : base("Unable to use this connection string", argumentException)
        {
        }
    }
    
}