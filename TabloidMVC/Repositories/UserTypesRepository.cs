using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class UserTypeRepository :  BaseRepository, IUserTypeRepository 
    { 
        public UserTypeRepository(IConfiguration config) : base(config) { }

        public List<UserType> GetAllUserTypes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id, Name
                        FROM UserType
                      
                       ";


                    List<UserType> userTypes = new List<UserType>();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userTypes.Add(new UserType()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),

                        });
                    }

                    reader.Close();

                    return userTypes;
                }
            }
        }
    }
}
