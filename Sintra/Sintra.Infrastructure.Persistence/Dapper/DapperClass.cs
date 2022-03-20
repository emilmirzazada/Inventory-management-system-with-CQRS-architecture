using Dapper;
using Microsoft.Data.SqlClient;
using Sintra.Domain.Settings;
using Sintra.Infrastructure.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Infrastructure.Persistence.Dapper
{
    public class DapperClass : IDapper
    {
        public void Dispose()
        {

        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(AppServicesHelper.Config.ConnectionString);
            return db.ExecuteScalar<int>(sp, parms, commandType: commandType);
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            SqlMapper.SetTypeMap(
                typeof(T),
                new ColumnAttributeTypeMapper<T>());
            using IDbConnection db = new SqlConnection(AppServicesHelper.Config.ConnectionString);
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            SqlMapper.SetTypeMap(
                 typeof(T),
                 new ColumnAttributeTypeMapper<T>());

            using IDbConnection db = new SqlConnection(AppServicesHelper.Config.ConnectionString);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public async Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            SqlMapper.SetTypeMap(
                 typeof(T),
                 new ColumnAttributeTypeMapper<T>());

            using IDbConnection db = new SqlConnection(AppServicesHelper.Config.ConnectionString);
            return (await db.QueryAsync<T>(sp, parms, commandType: commandType)).ToList();
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(AppServicesHelper.Config.ConnectionString);
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;

            using IDbConnection db = new SqlConnection(AppServicesHelper.Config.ConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;
            using IDbConnection db = new SqlConnection(AppServicesHelper.Config.ConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
    }
}
