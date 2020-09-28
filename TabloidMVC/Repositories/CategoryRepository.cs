using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
//Maintained by Brett Stoudt
namespace TabloidMVC.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration config) : base(config) { }

        //List Category
        public List<Category> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                   // get id and name from category table or return it based on name spelling in descending order
                    cmd.CommandText = "SELECT id, [name] FROM Category ORDER BY name";
                    
                    //execute sql command, builds sql data reader and retruns a reader object 
                    var reader = cmd.ExecuteReader();

                    var categories = new List<Category>();

                    //while the sql data reader returns results, we obtain those values in the form we need them in (string, int, etc.)
                    // and add each converted object (now category type) to the empty list created above
                    while (reader.Read())
                    {
                        categories.Add(new Category()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    //Close the reader when there is no responses to loop through
                    reader.Close();

                    //return the list of categories as the result of our GetAll method
                    return categories;
                }
            }
        }

        //Create Category
        public void Add(Category category)
        {
            using (var conn = Connection)
            {
                conn.Open();
                //SQL String that says we plan to insert into the category table with a new row that contains data for the Name Column
                // Then the SQL String says we are going to need an Output (repsonse) from the SQL Server containing the id of the row the insert will be placed
                // then we declare the location of the value we would like to add using the "@" to symbolize the paramater name
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Category ([Name])
                        OUTPUT INSERTED.ID
                        VALUES (@Name)";
                    //After the SQL String is declared we place the expected values in a comand that adds values to paramaters by passing through the SQL @Values
                    cmd.Parameters.AddWithValue("@Name", category.Name);

                    category.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        //Detail View for EDIT
        public Category GetCategoryById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM Category WHERE id = @id";


                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    Category category = null;

                    if (reader.Read())
                    {
                        category = NewCategoryFromReader(reader);
                    }

                    reader.Close();

                    return category;
                }
            }
        }

        //EDIT CATEGORY
        public void UpdateCategory(Category category)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Category
                            SET [Name] = @name
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", category.Id);
                    cmd.Parameters.AddWithValue("@name", category.Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //DELETE CATEGORY


        public void DeleteCategory(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Post
                            SET CategoryId = @categoryId
                            WHERE CategoryId = @id
                        ";
                    cmd.Parameters.AddWithValue("@categoryId", 1);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Category
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //public void DeleteCategory(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                    UPDATE Category
        //                    SET 
        //                    IsDeleted = @IsDeleted
        //                    WHERE Id = @id
        //                ";
                    
        //            cmd.Parameters.AddWithValue("@IsDeleted", 1);
        //            cmd.Parameters.AddWithValue("@id", id);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //Declare Category Properties
        private Category NewCategoryFromReader(SqlDataReader reader)
        {
            return new Category()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
            };
        }
    }
}
