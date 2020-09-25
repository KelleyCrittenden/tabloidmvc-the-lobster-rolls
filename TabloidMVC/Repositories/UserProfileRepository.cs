using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration config) : base(config) { }

        public UserProfile GetByEmail(string email)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                              ut.[Name] AS UserTypeName , u.IsDeactivated
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE email = @email AND u.IsDeactivated = 0";
                    cmd.Parameters.AddWithValue("@email", email);

                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                            IsDeactivated = reader.GetInt32(reader.GetOrdinal("IsDeactivated")),
                        };
                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }

        public List<UserProfile> GetAllUserProfiles()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                              ut.[Name] AS UserTypeName, u.IsDeactivated
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE u.IsDeactivated = 0
                        ORDER BY u.DisplayName;
                      
                       ";


                    List<UserProfile> userProfiles = new List<UserProfile>();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userProfiles.Add(new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                            IsDeactivated = reader.GetInt32(reader.GetOrdinal("IsDeactivated")),
                        });
                    }

                    reader.Close();

                    return userProfiles;
                }
            }
        }

        public List<UserProfile> GetAllAdminUserProfiles()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                              ut.[Name] AS UserTypeName, u.IsDeactivated
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE u.IsDeactivated = 0 AND u.UserTypeId = 1
                        ORDER BY u.DisplayName;
                      
                       ";


                    List<UserProfile> userProfiles = new List<UserProfile>();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userProfiles.Add(new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                            IsDeactivated = reader.GetInt32(reader.GetOrdinal("IsDeactivated")),
                        });
                    }

                    reader.Close();

                    return userProfiles;
                }
            }
        }


        public void Register(UserProfile profile)

        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO UserProfile (FirstName, LastName, DisplayName, Email, ImageLocation, UserTypeId, CreateDateTime)
                    OUTPUT INSERTED.ID
                    VALUES (@firstName, @lastName, @displayName, @email, @imageLocation, @userTypeId, @createDateTime);
                ";

                    cmd.Parameters.AddWithValue("@firstName", profile.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", profile.LastName);
                    cmd.Parameters.AddWithValue("@displayName", profile.DisplayName);
                    cmd.Parameters.AddWithValue("@email", profile.Email);
                    cmd.Parameters.AddWithValue("@ImageLocation", DbUtils.ValueOrDBNull(profile.ImageLocation));
                    cmd.Parameters.AddWithValue("@userTypeId", profile.UserTypeId);
                    cmd.Parameters.AddWithValue("@createDateTime", profile.CreateDateTime);


                    int id = (int)cmd.ExecuteScalar();

                    profile.Id = id;
                }
            }
        }
        public List<UserProfile> GetAllDeactivatedUserProfiles()
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                        SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                              ut.[Name] AS UserTypeName, u.IsDeactivated
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE u.IsDeactivated = 1
                        ORDER BY u.DisplayName;
                        
                       ";


                    List<UserProfile> userProfiles = new List<UserProfile>();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userProfiles.Add(new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                            IsDeactivated = reader.GetInt32(reader.GetOrdinal("IsDeactivated")),
                        });
                    }

                    reader.Close();

                    return userProfiles;
                }
            }
        }

        public UserProfile GetUserProfileById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                              ut.[Name] AS UserTypeName, u.IsDeactivated
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE u.id = @id
                        ORDER BY u.DisplayName;
                        
                       ";

                    cmd.Parameters.AddWithValue("@id", id);
                    UserProfile userProfile = new UserProfile();
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                            IsDeactivated = reader.GetInt32(reader.GetOrdinal("IsDeactivated")),
                        };
                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }

        public void DeactivateProfile(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                        SET
                        IsDeactivated = @isDeactivated
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@isDeactivated", 1);
                    cmd.Parameters.AddWithValue("@id", id);


                    cmd.ExecuteNonQuery();

                }
            }

        }
        public void ReactivateProfile(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                        SET
                        IsDeactivated = @isDeactivated
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@isDeactivated", 0);
                    cmd.Parameters.AddWithValue("@id", id);


                    cmd.ExecuteNonQuery();

                }
            }

        }
        public void UpdateUserType(int id, int userTypeId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                        SET
                        UserTypeId = @userTypeId
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@userTypeId", userTypeId);
                    cmd.Parameters.AddWithValue("@id", id);


                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
